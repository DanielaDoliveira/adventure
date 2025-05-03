
using System.Collections;

using Adventure.Player;

using Sirenix.OdinInspector;
using UnityEngine;

using Zenject;

namespace Player.CommandPattern
{
    public class Attack: MonoBehaviour,ICommand
    {
        [Title("ATTACK COLLIDER")] [LabelText("Player's attack collider (son of GameObject Player)")]
        public GameObject attackCollider;
        
        private IPlayerAnimations _playerAnimations;
        private PlayerState _playerState;

        [Inject]
        public void Construct(IPlayerAnimations playerAnimations, PlayerState playerState)
        {
            _playerAnimations = playerAnimations;
            _playerState = playerState;
        }
        
        public void Execute(Vector2 lastDirection)
        {
            if (!_playerState.IsAttacking && _playerState.CanAttack)
            {
                _playerState.IsAttacking = true;
                _playerAnimations.PlayAttackAnimation(
                    LAYER_TYPE.ATTACKING,
                    lastDirection,
                    _playerState.IsAttacking,
                    attackCollider
                );
            }
            _playerState.CanAttack = false;
            StartCoroutine(CanAttack());
          
        }

        private IEnumerator CanAttack()
        {
            yield return new WaitForSeconds(0.5f);
            _playerState.CanAttack = true;
        }

        public void Finish()
        {
            
            if (_playerState.IsAttacking)
            {
                _playerAnimations.FinishAttackAnimation(attackCollider);
                _playerState.IsAttacking = false;
            }
          
            if (_playerState.Movement != Vector2.zero)   _playerAnimations.PlayMovementAnimation(LAYER_TYPE.RUNNING, _playerState.Movement);
            
            else   _playerAnimations.PlayIdleAnimation(LAYER_TYPE.IDLE, _playerState.LastDirection);
            
            
        }

     



    }
}
