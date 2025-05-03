


    using UnityEngine;

    namespace Player
    {
        public interface IPlayerAnimations
        {
           
            void SetAnimationMovement(LAYER_TYPE layer, Vector2 direction);
            
            void SetAnimationIdle(LAYER_TYPE layer, Vector2 direction);
            
             void SetAnimationAttacking(LAYER_TYPE layer, Vector2 direction, bool isAttacking, GameObject attackCollider);

            void FinishAttackAnimation(GameObject attackCollider);
            
            void CheckLayers(LAYER_TYPE layer);
            
            void SetAllLayersToDefault();
        }
    }
