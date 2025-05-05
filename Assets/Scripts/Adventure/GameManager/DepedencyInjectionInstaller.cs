
using Adventure.Enemy.Interfaces;
using Adventure.GameManager;
using Adventure.GameManager.Interfaces;
using Adventure.Player;
using Enemy;
using Player;
using UnityEngine;
using Zenject;

    public class DepedencyInjectionInstaller: MonoInstaller
    {
        [SerializeField] private PlayerAnimations playerAnimations; 
        public override void InstallBindings()
        {
            Container.Bind<PlayerState>().AsSingle();
            Container.Bind<IPlayerAnimations>().To<PlayerAnimations>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IDialogueSystem>().To<DialogueSystem>().AsSingle();
            // Container.Bind<IEnemyBehaviour>().To<SlimeBehaviours>().FromComponentInHierarchy().AsTransient();
          
          
        }
    }
