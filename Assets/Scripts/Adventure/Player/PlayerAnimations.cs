using Adventure.Player;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerAnimations : MonoBehaviour, IPlayerAnimations
    {
        
        private PlayerState _playerState;
        private int _currentLayer = -1;
    
        [Inject]
        public void Construct( PlayerState playerState)=>_playerState = playerState;
        
        public void PlayMovementAnimation(LAYER_TYPE layer,Vector2 direction)
        {
            
            _playerState.Animator.SetFloat("x_mov",direction.x);
             _playerState.Animator.SetFloat("y_mov",direction.y);
            CheckLayers(layer); 
        }

        public void PlayIdleAnimation(LAYER_TYPE layer, Vector2 direction)
        {
            ResetAllLayers();
            CheckLayers(layer);
             _playerState.Animator.SetFloat("last_direction_x",direction.x);
             _playerState.Animator.SetFloat("last_direction_y",direction.y);
          
        }

        public void PlayAttackAnimation(LAYER_TYPE layer, Vector2 direction, bool isAttacking, GameObject attackCollider)
        {
            ResetAllLayers();
            CheckLayers(layer);
            attackCollider.SetActive(true);
          
            _playerState.Animator.SetFloat("last_direction_x", direction.x);
            _playerState.Animator.SetFloat("last_direction_y", direction.y);
            _playerState.Animator.SetBool("isAttacking", isAttacking);
            
            
        }


        public void FinishAttackAnimation( GameObject attackCollider)
        {
            attackCollider.SetActive(false);
            
            _playerState.Animator.SetBool("isAttacking", false);
            _playerState.Animator.SetLayerWeight(2,0);
        }

        public void CheckLayers(LAYER_TYPE layer)
        {
            
            int getLayer = layer.GetHashCode();
            if (_currentLayer != getLayer)
            {
                ResetAllLayers();
                _playerState.Animator.SetLayerWeight(getLayer,1);
                _currentLayer = getLayer;
            }
           
       
        }

        public void ResetAllLayers()
        {
            for (int i = 0; i < _playerState.Animator.layerCount; i++)
            {
                _playerState.Animator.SetLayerWeight(i, 0);
            }
        }
        
    }
}
