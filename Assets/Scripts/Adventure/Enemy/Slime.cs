
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
   private SlimeDamageEffect _damageEffect;
   public Vector2 Position;
   public Vector2 PlayerPosition;

   void Start()
    { 
        Position = transform.position;
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        _playerAttackPower = 10;
       _slimeLife = GetComponent<EnemyLife>();
       _slimeBehaviours = GetComponent<SlimeBehaviours>();
       _damageEffect = GetComponent<SlimeDamageEffect>();


    }

   

    public void OnAttack()
    {
            _damageEffect.TakeDamage(PlayerPosition, Color.blue);
            _slimeLife.TakeDamage(_playerAttackPower);
    }


}
 