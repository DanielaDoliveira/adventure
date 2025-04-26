using System;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{ 

     private PlayerControl _playerControl;
     [SerializeField]private Transform point;
     [SerializeField] private float Radius;
     [SerializeField] private LayerMask enemyLayer;
     void Start()
     {
         _playerControl = GetComponentInParent<PlayerControl>();
     }

     void OnAttack()
     {
         if (_playerControl._attack)
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
