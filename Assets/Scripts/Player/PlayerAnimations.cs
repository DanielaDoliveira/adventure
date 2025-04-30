using Player.Singletons;
using UnityEngine;

namespace Player
{
    public class PlayerAnimations : MonoBehaviour, IPlayerAnimations
    {
        
        public void SetAnimationMovement(LAYER_TYPE layer,Vector2 direction)
        {
            PlayerSingleton.Animator.SetFloat("x_mov",direction.x);
            PlayerSingleton.Animator.SetFloat("y_mov",direction.y);
            CheckLayers(layer); 
        }

        public void SetAnimationIdle(LAYER_TYPE layer, Vector2 direction)
        {
            CheckLayers(layer);
            PlayerSingleton.Animator.SetFloat("last_direction_x",direction.x);
            PlayerSingleton.Animator.SetFloat("last_direction_y",direction.y);
        }

        public void SetAnimationAttacking(LAYER_TYPE layer, Vector2 direction, bool isAttacking, GameObject attackCollider)
        {
            CheckLayers(layer);
            attackCollider.SetActive(true);
            PlayerSingleton.Animator.SetFloat("last_direction_x", direction.x);
            PlayerSingleton.Animator.SetFloat("last_direction_y", direction.y);
            PlayerSingleton.Animator.SetBool("isAttacking", isAttacking);
        }


        public void FinishAttackAnimation( GameObject attackCollider)
        {
            attackCollider.SetActive(false);
            PlayerSingleton.Animator.SetBool("isAttacking",false);
            PlayerSingleton.Animator.SetLayerWeight(3,0);
        }

        public void CheckLayers(LAYER_TYPE layer)
        {
            int getLayer = layer.GetHashCode();
            SetAllLayersToDefault();
            PlayerSingleton.Animator.SetLayerWeight(getLayer,1);
       
        }

        public void SetAllLayersToDefault()
        {
            PlayerSingleton.Animator.SetLayerWeight(0,0);
            PlayerSingleton.Animator.SetLayerWeight(1,0);
            PlayerSingleton.Animator.SetLayerWeight(2,0);
            PlayerSingleton.Animator.SetLayerWeight(3,0);
        }
        
    }
}
