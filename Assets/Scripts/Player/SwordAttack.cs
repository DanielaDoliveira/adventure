using Player.Singletons;
using UnityEngine;

namespace Player
{
    public class SwordAttack : MonoBehaviour
    { 
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("ENEMY"))
                other.GetComponent<Slime>().OnAttack();
        }
        
    }
}
