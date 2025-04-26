
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

    public class EnemyLife: MonoBehaviour
    {
        private float _life;
        [SerializeField] private GameObject deathVFXPrefab;
   
        private Rigidbody2D _rigidbody;
        private readonly float _timerToDestroy = 1f;
        
        private void Start()
        {
            _life = 23;
            _rigidbody = GetComponent<Rigidbody2D>();
            deathVFXPrefab.GetComponent<ParticleSystem>().Stop();
        
        }

        public void TakeDamage(int damage, Action enemyDamageBehaviour)
        {
            _life -= damage;
            KillEnemy();
         
            enemyDamageBehaviour.Invoke();
      
          
        }
        public void KillEnemy()
        {
            if (_life <= 0)
            {
               
                deathVFXPrefab.GetComponent<ParticleSystem>().Play();
                GetComponent<Animator>().Play("die");
            }
        }

     
        public void DestroyEnemyAfterDead()
        {
            Destroy(gameObject,_timerToDestroy);
        }

     
    }
