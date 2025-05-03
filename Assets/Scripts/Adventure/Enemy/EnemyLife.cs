
using Enemy;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;


public class EnemyLife: MonoBehaviour
    {
        [SerializeReference,ReadOnly]
        private float _life = 23f;
        [SerializeField] private GameObject deathVFXPrefab;
   
        private Rigidbody2D _rigidbody;
        private readonly float _timerToDestroy = 1f;
        private Animator _animator;
        private IEnemyBehaviour _behaviour;
        
        
        [SerializeField] private Collider2D[] collider;
        private IEnemyBehaviour _enemyBehaviour;
        [Inject] public void Constructor(IEnemyBehaviour enemyBehaviour) => _enemyBehaviour = enemyBehaviour;
        public void Init(float initialLife, Rigidbody2D rb, Animator animator, IEnemyBehaviour behaviour)
        {
            _life = initialLife;
            _rigidbody = rb;
            _animator = animator;
            _behaviour = behaviour;
        }
         private void Awake()
            {
                if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody2D>();
                if (_animator == null) _animator = GetComponent<Animator>();
                if (_behaviour == null) _behaviour = GetComponent<IEnemyBehaviour>();
                if (collider.Length == 0) collider = GetComponents<Collider2D>();
            }
        private void Start()=> deathVFXPrefab.GetComponent<ParticleSystem>().Stop();
          
        

        public void TakeDamage(int damage )
        {
            _life -= damage;
            if (_life <= 0)   KillEnemy();
           
          
      
          
        }
        public void KillEnemy()
        {
                deathVFXPrefab.GetComponent<ParticleSystem>().Play();
                foreach (var col in collider)
                {
                    col.enabled = false;
                }
             // GetComponent<SlimeBehaviours>().StopPirsuitPlayer();
             _behaviour?.StopPirsuitPlayer();
              _rigidbody.linearVelocity = Vector2.zero;
                 _animator.Play("die");
                
            
        }

     
        public void DestroyEnemyAfterDead()=>Destroy(gameObject,_timerToDestroy);
        

     
    }
