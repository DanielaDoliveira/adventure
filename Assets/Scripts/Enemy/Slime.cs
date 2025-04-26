
using System;
using Enemy;
using UnityEngine;

public class Slime : MonoBehaviour
{
   [SerializeField] private LayerMask attackLayer;
   private float _life;
   private int _playerAttackPower;
   
   private EnemyLife _slimeLife;
   private SlimeBehaviours _slimeBehaviours;
    void Start()
    { 
        _playerAttackPower = 10;
      _slimeLife = GetComponent<EnemyLife>();
      _slimeBehaviours = GetComponent<SlimeBehaviours>();
    }

  
    public void OnAttack()
    {
      _slimeBehaviours.KnockbackBehaviour();
       _slimeLife.TakeDamage(_playerAttackPower,_slimeBehaviours.BehaviourTakingDamage);
    }


}
 