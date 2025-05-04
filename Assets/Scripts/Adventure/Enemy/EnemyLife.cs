
using Adventure.Enemy.Interfaces;
using Enemy;
using Enemy.Model;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class EnemyLife: MonoBehaviour
    {
    
       [Title("Death VFX Prefab")] [InfoBox("This particle cannot be null", InfoMessageType.Warning)]  [SerializeReference,ReadOnly]
       private ParticleSystem deathVFXPrefab;

       private readonly float _life = 23;
        private Rigidbody2D _rigidbody;
        private readonly float _timerToDestroy = 1f;
        private Animator _animator;
        private IEnemyBehaviour _behaviour;
        
        private float _damageCooldown = 0.1f;
        private float _lastDamageTime = -999f;
        
        [Title("Collider")][LabelText("Enemy colliders")]
        [SerializeField] private Collider2D[] collider;
        private bool _isDead = false;
 
        private EnemyLifeModel _lifeModel ;

        
        public void Init(EnemyLifeModel lifeModel, Rigidbody2D rb, Animator animator, IEnemyBehaviour behaviour)
        {
            _lifeModel = lifeModel;
            _rigidbody = rb;
            _animator = animator;
            _behaviour = behaviour;
        }
        public bool IsDead => _isDead;
        public float CurrentLife => _lifeModel.life;
        protected virtual float CurrentTime => Time.time;
        private void InitializeVFX()
        {
            deathVFXPrefab = GetComponentInChildren<ParticleSystem>();
            deathVFXPrefab.Stop();
        }
        
         private void Awake()
            {
              
                if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody2D>();
                if (_animator == null) _animator = GetComponent<Animator>();
                if (_behaviour == null) _behaviour = GetComponent<IEnemyBehaviour>();
                if (collider == null || collider.Length == 0) collider = GetComponents<Collider2D>();
                if (_lifeModel == null) _lifeModel = new EnemyLifeModel(_life);

            }

            private void Start() => InitializeVFX();          
        

        public void TakeDamage(int damage )
        {
            if (_isDead || CurrentTime - _lastDamageTime < _damageCooldown) return;
            _lastDamageTime = Time.time;

            if (_lifeModel.TakeDamage(damage))
            {
                _isDead = true;
                KillEnemy();
            }
            

           
            
            
        }
        public void KillEnemy()
        {
            if (!_isDead) return;
        
            foreach (var col in collider)  col.enabled = false;
            _behaviour?.StopPirsuitPlayer();
            deathVFXPrefab.Play();
            _rigidbody.linearVelocity = Vector2.zero;
            _animator.Play("die");
         
                
            
        }

     
        public void DestroyEnemyAfterDead()=>Destroy(gameObject,_timerToDestroy);
        

     
    }
