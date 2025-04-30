
using Player;
using UnityEngine;
using Zenject;

    public class DepedencyInjectionInstaller: MonoInstaller
    {
        [SerializeField] private PlayerAnimations playerAnimations; 
        public override void InstallBindings()
        {
            Debug.Log("Install");
            Container.Bind<IPlayerAnimations>().FromInstance(playerAnimations).AsSingle();
            Container.Bind<PlayerControl>().FromComponentInHierarchy().AsSingle();
        }
    }
