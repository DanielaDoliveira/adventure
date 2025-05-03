


    using UnityEngine;

    namespace Player
    {
        public interface IPlayerAnimations
        {
           
            void PlayMovementAnimation(LAYER_TYPE layer, Vector2 direction);
            
            void PlayIdleAnimation(LAYER_TYPE layer, Vector2 direction);
            
             void PlayAttackAnimation(LAYER_TYPE layer, Vector2 direction, bool isAttacking, GameObject attackCollider);

            void FinishAttackAnimation(GameObject attackCollider);
            
            void CheckLayers(LAYER_TYPE layer);
            
            void ResetAllLayers();
        }
    }
