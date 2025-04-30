
using Player;
using UnityEngine;
using Zenject;

    public class DepedencyInjectionInstaller: MonoInstaller
    {
        [SerializeField] private PlayerAnimations playerAnimations; 
        public override void InstallBindings()
        {
            Container.Bind<IPlayerAnimations>().To<PlayerAnimations>().FromComponentInHierarchy().AsSingle();
          
        }
    }
