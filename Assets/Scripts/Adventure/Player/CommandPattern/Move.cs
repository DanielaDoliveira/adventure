using Adventure.Player;
using Player.Singletons;
using UnityEngine;
using Zenject;

namespace Player.CommandPattern
{
    public class Move: MonoBehaviour, ICommand
    {
        private readonly float _speed = 5f;

        private IPlayerAnimations _playerAnimations;
        private PlayerState _playerState;

        [Inject]
        public void Construct(IPlayerAnimations playerAnimations, PlayerState playerState)
        {
            _playerAnimations = playerAnimations;
            _playerState = playerState;
        }
        public void Execute(Vector2 lastDirection) => _playerState.Movement = lastDirection;
        
        private void Update() => MovementAnimationValidator();

            
        
        public void FixedUpdate() =>
            _playerState.Rigidbody.linearVelocity = new Vector2(_playerState.Movement.x * _speed,_playerState.Movement.y * _speed);
        
        private void MovementAnimationValidator()
        {
            
            if (_playerState.Movement != Vector2.zero && !(_playerState.IsAttacking))
            {
               
                _playerState.CanAttack = false;
                _playerAnimations.PlayMovementAnimation(LAYER_TYPE.RUNNING, _playerState.Movement);
                Vector2Int cleanDirection = new Vector2Int(
                    Mathf.RoundToInt(_playerState.Movement.normalized.x),
                    Mathf.RoundToInt(_playerState.Movement.normalized.y)
                );
    
                _playerState.LastDirection = cleanDirection;
        
            }
            if (_playerState.Movement == Vector2.zero && !(_playerState.IsAttacking))
            {
                _playerState.CanAttack = true;
                _playerAnimations.PlayIdleAnimation(LAYER_TYPE.IDLE,_playerState.LastDirection);
            }
        }
    }
}