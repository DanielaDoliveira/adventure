#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gaskellgames.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [InitializeOnLoad]
    public static class HierarchyUtility
    {
        #region Variables
        
        private static GaskellgamesHubSettings_SO settings;
        private static SerializedDictionary<Type, string> hierarchyIcons;
        private static SerializedDictionary<int, HierarchyData> hierarchyObjectCache;
        
        public static event Action onCacheHierarchyIcons;
        
        #endregion
        
        //----------------------------------------------------------------------------------------------------
        
        #region Constructor [TODO]

        static HierarchyUtility()
        {
            GgGUI.onCacheGgGUIIcons -= Initialisation;
            GgGUI.onCacheGgGUIIcons += Initialisation;
        }
        
        private static void Initialisation()
        {
            // initialise references
            settings = EditorExtensions.GetAssetByType<GaskellgamesHubSettings_SO>();
            hierarchyIcons ??= new SerializedDictionary<Type, string>();
            onCacheHierarchyIcons?.Invoke();
            
            // handle initialise editor
            GgEditorCallbacks.OnSafeInitialize -= OnSafeInitialize;
            GgEditorCallbacks.OnSafeInitialize += OnSafeInitialize;
            
            // handle scene loads
            EditorSceneManager.sceneOpened -= OnSceneOpened;
            EditorSceneManager.sceneOpened += OnSceneOpened;
            
            // handle scene updates
            GgEditorCallbacks.OnSceneUpdated -= OnSceneUpdated;
            GgEditorCallbacks.OnSceneUpdated += OnSceneUpdated;
            
            // TODO - handle gameObject created (OnGameObjectCreated)
            
            // TODO - handle gameObject destroyed (OnGameObjectDestroyed)
            
            // TODO - handle prefab updated (OnPrefabInstanceUpdated)
            
            // handle component updates
            GgEditorCallbacks.OnGameObjectStructureUpdated -= OnGameObjectStructureUpdated;
            GgEditorCallbacks.OnGameObjectStructureUpdated += OnGameObjectStructureUpdated;
            
            // handle parenting changes
            GgEditorCallbacks.OnGameObjectParentUpdated -= OnGameObjectParentUpdated;
            GgEditorCallbacks.OnGameObjectParentUpdated += OnGameObjectParentUpdated;
            
            // handle drawing icons
            EditorApplication.hierarchyWindowItemOnGUI -= DrawHierarchy;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchy;
            
            // initialise cache
            hierarchyObjectCache = new SerializedDictionary<int, HierarchyData>();
            CacheAllHierarchyObjectsInAllOpenScenes();
        }

        #endregion
        
        //----------------------------------------------------------------------------------------------------
        
        #region Callbacks
        
        private static void OnSafeInitialize()
        {
            // initialise dictionary if required
            hierarchyObjectCache ??= new SerializedDictionary<int, HierarchyData>();
            
            // cache all objects in opened scene
            CacheAllHierarchyObjectsInAllOpenScenes();
        }
        
        private static void OnSceneOpened(Scene scene, OpenSceneMode mode)
        {
            // initialise dictionary if required
            hierarchyObjectCache ??= new SerializedDictionary<int, HierarchyData>();
            
            // if single scene opened, clear current cache
            if (mode == OpenSceneMode.Single)
            {
                hierarchyObjectCache.Clear();
            } 
            
            // cache all objects in opened scene
            CacheAllHierarchyObjectsInScene(scene);
        }

        private static void OnSceneUpdated(GgEventArgs_SceneData ggEventArgsSceneData)
        {
            // initialise dictionary if required
            hierarchyObjectCache ??= new SerializedDictionary<int, HierarchyData>();

            // cache all objects in updated scene
            CacheAllHierarchyObjectsInScene(ggEventArgsSceneData.sceneData.Scene);
        }

        private static void OnGameObjectStructureUpdated(GgEventArgs_GameObject ggEventArgsGameObject)
        {
            // initialise dictionary if required
            hierarchyObjectCache ??= new SerializedDictionary<int, HierarchyData>();

            // if exists, update components
            if (hierarchyObjectCache.TryGetValue(ggEventArgsGameObject.gameObject.GetInstanceID(), out HierarchyData hierarchyData))
            {
                CacheHierarchyObject(ggEventArgsGameObject.gameObject, hierarchyData.indentLevel, hierarchyData.parentIsFinalChild, hierarchyData.isFinalChild);
            }
        }

        private static void OnGameObjectParentUpdated(GgEventArgs_GameObjectChangedParent ggEventArgsGameObjectChangedParent)
        {
            // TODO - optimise using data in ggEventArgsGameObjectChangedParent
            
            // initialise dictionary if required
            hierarchyObjectCache ??= new SerializedDictionary<int, HierarchyData>();
            hierarchyObjectCache.Clear();
            
            // cache all objects in opened scene
            CacheAllHierarchyObjectsInAllOpenScenes();
        }

        private static void DrawHierarchy(int instanceID, Rect position)
        {
            if (hierarchyObjectCache.TryGetValue(instanceID, out HierarchyData hierarchyData))
            {
                DrawHierarchyBreadcrumbs(position, hierarchyData);
            }
            DrawHierarchyComponentIcons(instanceID, position);
        }

        #endregion
        
        //----------------------------------------------------------------------------------------------------
        
        #region Private Functions

        private static void CacheAllHierarchyObjectsInAllOpenScenes()
        {
            // force initialise/clear dictionary
            hierarchyObjectCache = new SerializedDictionary<int, HierarchyData>();
            hierarchyObjectCache.Clear();
            
            // cache all gameObjects in all open scenes
            List<Scene> scenes = SceneExtensions.GetAllOpenScenes();
            foreach (Scene scene in scenes)
            {
                CacheAllHierarchyObjectsInScene(scene);
            }
        }
        
        /// <summary>
        /// Cache all gameObjects in scene
        /// </summary>
        /// <param name="scene"></param>
        private static void CacheAllHierarchyObjectsInScene(Scene scene)
        {
            GameObject[] rootObjects = scene.GetRootGameObjects();
            int rootObjectsCount = rootObjects.Length;
            int count = 0;
            foreach (GameObject gameObject in rootObjects)
            {
                CacheHierarchyObjectRecursive(gameObject.transform, 0, false, count == rootObjectsCount);
                count++;
            }
        }

        private static void CacheHierarchyObjectRecursive(Transform transform, int indentLevel, bool parentIsFinalChild, bool isFinalChild)
        {
            int childCount = transform.childCount;
            CacheHierarchyObject(transform.gameObject, indentLevel, parentIsFinalChild, isFinalChild);
            
            // recursive call for children
            int count = 0;
            foreach (Transform childTransform in transform)
            {
                count++;
                CacheHierarchyObjectRecursive(childTransform, indentLevel + 1, isFinalChild, count == childCount);
            }
        }

        private static void CacheHierarchyObject(GameObject gameObject, int indentLevel, bool parentIsFinalChild, bool isFinalChild)
        {
            if (hierarchyIcons == null) { return; }
            int instanceID = gameObject.GetInstanceID();
            Component[] components = gameObject.GetComponents(typeof(Component));
            List<Type> validTypes = new List<Type>();
            foreach (Component component in components)
            {
                // cache only components for which there is an icon to draw
                if (component == null) { continue; }
                Type componentType = component.GetType();
                if (!hierarchyIcons.TryGetValue(componentType, out string outValue)) { continue; }
                validTypes.TryAdd(componentType);
            }

            HierarchyData hierarchyData = new HierarchyData(indentLevel, 0 < gameObject.transform.childCount, parentIsFinalChild, isFinalChild, validTypes);
            hierarchyObjectCache.Remove(instanceID);
            hierarchyObjectCache.TryAdd(instanceID, hierarchyData);
        }
        
        private static void DrawHierarchyComponentIcons(int instanceID, Rect position)
        {
            if (settings == null) { return; }
            if (hierarchyIcons == null) { return; }
            if (hierarchyObjectCache == null) { return; }
            
            // draw if exists
            if (!hierarchyObjectCache.TryGetValue(instanceID, out HierarchyData hierarchyData)) { return; }
            int maxIcons = 0;
            switch (settings.showHierarchyIcons)
            {
                default:
                case GaskellgamesHubSettings_SO.HierarchyIconOptions.AllIcons:
                    maxIcons = hierarchyData.componentCount;
                    break;
                
                case GaskellgamesHubSettings_SO.HierarchyIconOptions.ThreeIcons:
                    maxIcons = Mathf.Min(hierarchyData.componentCount, 3);
                    break;
                
                case GaskellgamesHubSettings_SO.HierarchyIconOptions.TwoIcons:
                    maxIcons = Mathf.Min(hierarchyData.componentCount, 2);
                    break;
                
                case GaskellgamesHubSettings_SO.HierarchyIconOptions.OneIcon:
                    maxIcons = Mathf.Min(hierarchyData.componentCount, 1);
                    break;
                
                case GaskellgamesHubSettings_SO.HierarchyIconOptions.NoIcons:
                    return;
            }
            for (int i = 0; i < maxIcons; i++)
            {
                if (!hierarchyIcons.TryGetValue(hierarchyData.components[i], out string outValue)) { continue; }
                DrawHierarchyComponent(position, GgGUI.GetIcon(outValue), i);
            }
        }

        private static void DrawHierarchyComponent(Rect position, Texture icon, int indent = 0)
        {
            // check for valid draw
            if (Event.current.type != EventType.Repaint) { return; }

            // draw icon
            if (icon == null) { return; }
            float pixels = 16;
            float offset = pixels * (indent + 1);
            EditorGUIUtility.SetIconSize(new Vector2(pixels, pixels));
            Rect iconPosition = new Rect(position.xMax - offset, position.yMin, position.width, position.height);
            GUIContent iconGUIContent = new GUIContent(icon);
            EditorGUI.LabelField(iconPosition, iconGUIContent);
        }

        private static void DrawHierarchyBreadcrumbs(Rect position, HierarchyData hierarchyData)
        {
            // check for valid draw
            if (settings.showHierarchyBreadcrumbs == GaskellgamesHubSettings_SO.HierarchyBreadcrumbOptions.Never) { return; }
            if (Event.current.type != EventType.Repaint) { return; }

            // draw icon
            float pixels = 16;
            EditorGUIUtility.SetIconSize(new Vector2(pixels, pixels));
            for (int i = 0; i <= hierarchyData.indentLevel; i++)
            {
                string breadcrumbTexture;
                if (i <= 0)
                {
                    breadcrumbTexture = hierarchyData.isFinalChild
                        ? hierarchyData.hasChild ? "Icon_Breadcrumb_D" : "Icon_Breadcrumb_E"
                        : hierarchyData.hasChild ? "Icon_Breadcrumb_B" : "Icon_Breadcrumb_C";
                }
                else if (!hierarchyData.parentIsFinalChild || i == hierarchyData.indentLevel)
                {
                    breadcrumbTexture = "Icon_Breadcrumb_A";
                }
                else
                {
                    continue;
                }
                float offset = ((pixels * 0.5f) + GgGUI.standardSpacing) + ((pixels - GgGUI.standardSpacing) * (i + 1));
                Rect iconPosition = new Rect(position.xMin - offset, position.yMin, position.width, position.height);
                EditorGUI.LabelField(iconPosition, GgGUI.IconContent(breadcrumbTexture));
            }
        }

        #endregion
        
        //----------------------------------------------------------------------------------------------------
        
        #region Public Functions
        
        /// <summary>
        /// Try to add custom icons to the HierarchyIcon_GgCore hierarchyIcons list. For best results, subscribe to
        /// the HierarchyIcon_GgCore.<see cref="onCacheHierarchyIcons"/> action using a script that implements <see cref="InitializeOnLoadAttribute"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static bool TryAddHierarchyIcon(Type type, string name, Texture icon)
        {
            if (icon == null) { return false; }
            hierarchyIcons ??= new SerializedDictionary<Type, string>();
            if (!hierarchyIcons.TryAdd(type, name)) { return false; }
            if (GgGUI.TryAddCustomIcon(name, icon)) { return true; }
            
            hierarchyIcons.Remove(type);
            return false;
        }

        #endregion
        
    } // class end
}

#endif