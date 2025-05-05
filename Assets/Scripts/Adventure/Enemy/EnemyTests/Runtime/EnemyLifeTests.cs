using System.Reflection;
using Adventure.Enemy.EnemyTests.MOCKS;
using Enemy;
using Enemy.Model;
using NUnit.Framework;
using UnityEngine;

namespace Adventure.Enemy.EnemyTests.Runtime
{
    public class EnemyLifeTests
    {
        private GameObject _enemyGO;
        private EnemyLife _enemyLife;
        private EnemyLifeModel _lifeModel;
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private ParticleSystem _vfx;
        private Collider2D _collider;
        private FakeEnemyBehaviour _fakeBehaviour;


        [SetUp]
        public void Setup()
        {
            _enemyGO = new GameObject("Enemy");

            _rigidbody = _enemyGO.AddComponent<Rigidbody2D>();
            _animator = _enemyGO.AddComponent<Animator>();
            _collider = _enemyGO.AddComponent<BoxCollider2D>();
          
    
          SettingParticleSystemGO();
 
            _fakeBehaviour = _enemyGO.AddComponent<FakeEnemyBehaviour>();
            
            _enemyLife = _enemyGO.AddComponent<TestableEnemyLife>();
            _enemyLife.GetType().GetField("deathVFXPrefab", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(_enemyLife, _vfx);
           _enemyLife.SendMessage("InitializeVFX"); 
       
            _lifeModel = new EnemyLifeModel(10);
            _enemyLife.Init(_lifeModel, _rigidbody, _animator, _fakeBehaviour);
            
           GetPrivateFieldEnemyColliders();
           GetPrivateFieldDeathVFXPrefab();
           
        }


        private void SettingParticleSystemGO()
        {
            GameObject vfxGO = new GameObject("VFX");
            _vfx = vfxGO.AddComponent<ParticleSystem>();
            vfxGO.transform.SetParent(_enemyGO.transform);
        }
        private void GetPrivateFieldEnemyColliders()
        {
            _enemyLife.GetType().GetField("enemyColliders", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(_enemyLife, new Collider2D[] { _collider });
        }

        private void GetPrivateFieldDeathVFXPrefab()
        {
            _enemyLife.GetType().GetField("deathVFXPrefab", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(_enemyLife, _vfx);
        }

        [Test]
        public void TakeDamage_ReducesLife()
        {
            float before = _enemyLife.CurrentLife;
            _enemyLife.TakeDamage(3);
            Assert.Less(_enemyLife.CurrentLife, before);
        }

        [Test]
        public void TakeDamage_DoesNotApplyDamage_WhenCooldownNotElapsed()
        {
            _enemyLife.TakeDamage(2);
            float afterFirst = _enemyLife.CurrentLife;
            _enemyLife.TakeDamage(2); // ainda no cooldown
            Assert.AreEqual(afterFirst, _enemyLife.CurrentLife);
        }

        [Test]
        public void TakeDamage_KillsEnemy_WhenLifeIsZero()
        {
            _enemyLife.TakeDamage(10);
            GetPrivateFieldDeathVFXPrefab();
            Assert.IsTrue(_enemyLife.IsDead);
        }

        [Test]
        public void KillEnemy_DisablesColliders_StopsMovement_PlaysVFX()
        {
            _enemyLife.TakeDamage(10);

            Assert.IsFalse(_collider.enabled);
            Assert.AreEqual(Vector2.zero, _rigidbody.linearVelocity);
            Assert.IsTrue(_vfx.isPlaying);
            Assert.IsTrue(_fakeBehaviour.StoppedPursuit);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_enemyGO);
        }
    }
}