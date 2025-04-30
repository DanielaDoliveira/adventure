using Player.Singletons;
using UnityEngine;

namespace Player
{
    public class SwordAttack : MonoBehaviour
    { 

 
        [SerializeField]private Transform point;
        [SerializeField] private float Radius;
        [SerializeField] private LayerMask enemyLayer;
        void Start()
        {
       
        }

        void OnAttack()
        {
            if (PlayerSingleton.IsAttacking)
            {
                Collider2D sword_hit = Physics2D.OverlapCircle(point.position, Radius,enemyLayer);
            
 
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("ENEMY"))
            {
                other.GetComponent<Slime>().OnAttack();
            }
        }

        private void Update()
        {
            OnAttack();
        }

    }
}
