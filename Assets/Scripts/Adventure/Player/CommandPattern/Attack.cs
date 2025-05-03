using System.Collections;
using Player.Singletons;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Player.CommandPattern
{
    public class Attack: MonoBehaviour,ICommand
    {
        [Title("ATTACK COLLIDER")]
        [LabelText("Player's attack collider (son of GameObject Player)")]
        public GameObject attackCollider;
        private IPlayerAnimations _playerAnimations;
        [Inject] public void Construct(IPlayerAnimations playerAnimations) => _playerAnimations = playerAnimations;
        
        public void Execute(Vector2 lastDirection)
        {
            PlayerSingleton.IsAttacking  = true;
           _playerAnimations.SetAnimationAttacking(LAYER_TYPE.ATTACKING,lastDirection,PlayerSingleton.IsAttacking,attackCollider);
            StartCoroutine(CanAttack());
        }
    
        private  IEnumerator CanAttack()
        {
            if (PlayerSingleton.IsAttacking)
            {
                PlayerSingleton.CanAttack = false;
                yield return new WaitForSeconds(0.5f);
                PlayerSingleton.CanAttack = true;
              
            }
        }
        
        public void Finish()
        {
            if (PlayerSingleton.IsAttacking)
            {
               _playerAnimations.FinishAttackAnimation(attackCollider);
                PlayerSingleton.IsAttacking = false;
            }
        }
        


      

    }
}
