using UnityEngine;

namespace Player.Singletons
    {
        public class PlayerSingleton: MonoBehaviour
        {
            public static PlayerSingleton Instance;
         [Header("Verify if player can use attack")]   public static bool CanAttack;
         [Header("looking at player attack")]  public static bool IsAttacking;
         [Header("Get last player direction")] public static Vector2 LastDirection;
        [Header("Get Current Player Movement")] public static Vector2 Movement;
        [Header("Get Player Physics")] public static Rigidbody2D Rigidbody;
        public static Animator Animator;
        
            private void Awake()
            {
                if(Instance == null) Instance = this;
                else Destroy(gameObject);
                DontDestroyOnLoad(gameObject);
            }
            
        }
    }
