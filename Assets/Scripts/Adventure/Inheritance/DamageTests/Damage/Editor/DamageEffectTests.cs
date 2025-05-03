using System.Collections;
using Adventure.Player.PlayerTest.Aux_classes;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Reflection;
namespace Adventure.Inheritance.DamageTests.Editor
{
    public class DamageEffectTests
    {
        private GameObject _go;
        private TestableDamageEffect _damageEffect;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rb;
        
        [SetUp]
        public void SetUp()
        {
            DOTween.KillAll();
            DOTween.Init(true, true, LogBehaviour.ErrorsOnly);

            _go = new GameObject("TestObject");
            _spriteRenderer = _go.AddComponent<SpriteRenderer>();
            _rb = _go.AddComponent<Rigidbody2D>();
            _damageEffect = _go.AddComponent<TestableDamageEffect>();

           
            typeof(DamageEffect)
                .GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.Invoke(_damageEffect, null);
        }
        
        [UnityTest]
        public IEnumerator TakeDamage_SetsIsTakingDamageTrueThenFalse()
        {
            Assert.IsFalse(_damageEffect.IsTakingDamage);

            _damageEffect.TakeDamage(Vector2.left, Color.red);
            yield return new WaitForSeconds(0.05f);

            Assert.IsTrue(_damageEffect.IsTakingDamage);

            yield return new WaitForSeconds(0.3f); // tempo para o flash terminar (0.1s x 2 loops + margem)

            Assert.IsFalse(_damageEffect.IsTakingDamage);
        }
        [UnityTest]
        public IEnumerator TakeDamage_KnockbackMovesAwayFromEnemy()
        {
            _go.transform.position = Vector2.zero;
            Vector2 enemyPosition = new Vector2(-1, 0);

            _damageEffect.TakeDamage(enemyPosition, Color.red);

            yield return new WaitForSeconds(0.6f); 

            Assert.Greater(_go.transform.position.x, 0f); 
        }
        [UnityTest]
        public IEnumerator TakeDamage_SecondCallIgnoredWhileTakingDamage()
        {
            _damageEffect.TakeDamage(Vector2.left, Color.red);
            Assert.IsTrue(_damageEffect.IsTakingDamage);

            Vector3 posAfterFirstCall = _go.transform.position;

        
            _damageEffect.TakeDamage(Vector2.right, Color.blue);

            yield return new WaitForSeconds(0.6f);

          
            Vector3 finalPos = _go.transform.position;
            Vector3 direction = finalPos - posAfterFirstCall;

 
            Assert.Greater(direction.x, 0f);
        }
        [TearDown]
        public void TearDown()
        {
            DOTween.KillAll();
            Object.DestroyImmediate(_go);
        }


    }
}
