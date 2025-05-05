
using Enemy.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemy
{
        
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
    public class EnemyLife: MonoBehaviour
        {
        
           private const float DefaultLife = 23f;
            private const float timerToDestroy = 1f;
            private const float damageCooldown = 0.1f;
            
           [Title("Death VFX Prefab")] [InfoBox("This particle cannot be null", InfoMessageType.Warning)]  [SerializeReference]
           private ParticleSystem deathVFXPrefab;
           [Title("Colliders")][LabelText("Enemy enemyColliderss")]
           [SerializeField] private Collider2D[] enemyColliders;
           
           
            private Rigidbody2D _rigidbody;
            private Animator _animator;
            private IEnemyBehaviour _behaviour;
            private EnemyLifeModel _lifeModel ;
            
            private float _lastDamageTime = -999f;
            private bool _isDead = false;
            
            public bool IsDead => _isDead;
            public float CurrentLife => _lifeModel.life;
            protected virtual float CurrentTime => Time.time;
            public void Init(EnemyLifeModel lifeModel, Rigidbody2D rb, Animator animator, IEnemyBehaviour behaviour)
            {
                _lifeModel = lifeModel;
                _rigidbody = rb;
                _animator = animator;
                _behaviour = behaviour;
            }
            private void InitializeVFX()
            {
                deathVFXPrefab = GetComponentInChildren<ParticleSystem>(); 
                deathVFXPrefab.Stop();
            }
            
             private void Awake()
                {
                  
                    _rigidbody ??= GetComponent<Rigidbody2D>();
                    _animator ??= GetComponent<Animator>();
                    _behaviour ??= GetComponent<IEnemyBehaviour>();
                    enemyColliders ??= GetComponents<Collider2D>();;
                    _lifeModel ??= new EnemyLifeModel(DefaultLife);

                }

                private void Start() => InitializeVFX();          
            

            public void TakeDamage(int damage )
            {
                if (CannotTakeDamage()) return;
                _lastDamageTime = Time.time;

                if (_lifeModel.TakeDamage(damage)) HandleDeath();
                   
                
                

               
                
                
            }

            private void HandleDeath()
            {
                _isDead = true;
                KillEnemy();
            }
            
            private bool CannotTakeDamage()=> _isDead || CurrentTime - _lastDamageTime < damageCooldown;

            private void KillEnemy()
            {
                if (!_isDead) return;
            
                foreach (var col in enemyColliders)  col.enabled = false;
                _behaviour?.StopPirsuitPlayer();
                deathVFXPrefab.Play();
                _rigidbody.linearVelocity = Vector2.zero;
                _animator.Play("die");
             
                    
                
            }

         
            public void DestroyEnemyAfterDead()=>Destroy(gameObject,timerToDestroy);
            

         
        }

}