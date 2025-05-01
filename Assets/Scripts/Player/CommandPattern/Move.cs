using Player.Singletons;
using UnityEngine;
using Zenject;

namespace Player.CommandPattern
{
    public class Move: MonoBehaviour, ICommand
    {
        private readonly float _speed = 5f;

        private IPlayerAnimations _playerAnimations;
        [Inject]  public void  Construct (IPlayerAnimations playerAnimations) => _playerAnimations = playerAnimations;
        public void Execute(Vector2 lastDirection) => PlayerSingleton.Movement = lastDirection;
        private void Update() => MovementAnimationValidator();
        private void FixedUpdate() => PlayerSingleton.Rigidbody.linearVelocity = new Vector2(PlayerSingleton.Movement.x * _speed, PlayerSingleton.Movement.y * _speed);
        
        private void MovementAnimationValidator()
        {
            if (PlayerSingleton.Movement != Vector2.zero && !(PlayerSingleton.IsAttacking))
            {
                PlayerSingleton.CanAttack = false;
              _playerAnimations.SetAnimationMovement(LAYER_TYPE.RUNNING,PlayerSingleton.Movement);
                PlayerSingleton.LastDirection = PlayerSingleton.Movement;
        
            }
            if (PlayerSingleton.Movement == Vector2.zero && !(PlayerSingleton.IsAttacking))
            {
                PlayerSingleton.CanAttack = true;
                _playerAnimations.SetAnimationIdle(LAYER_TYPE.IDLE,PlayerSingleton.LastDirection);
            }
        }
    }
}