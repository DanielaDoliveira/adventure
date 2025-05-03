using System.Collections;
using Adventure.Enemy.EnemyTests.MOCKS;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Reflection;

namespace Adventure.Enemy.EnemyTests.Editor
{
public class EnemyLifeTests
{
    private GameObject _enemy;
    private EnemyLife _enemyLife;
    private Rigidbody2D _rigidbody;
    private ParticleSystem _particleSystem;
    private Animator _animator;
    private FakeSlimeBehaviours _fakeSlime;
    private Collider2D[] _colliders;

    [SetUp]
    public void Setup()
    {
        _enemy = new GameObject("Enemy");

        // Componentes
        _enemyLife = _enemy.AddComponent<EnemyLife>();
        _rigidbody = _enemy.AddComponent<Rigidbody2D>();
        _animator = _enemy.AddComponent<Animator>();
        _fakeSlime = _enemy.AddComponent<FakeSlimeBehaviours>();
        _colliders = new Collider2D[]
        {
            _enemy.AddComponent<BoxCollider2D>(),
            _enemy.AddComponent<CircleCollider2D>()
        };

        // Configura campo private collider
        typeof(EnemyLife).GetField("collider", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(_enemyLife, _colliders);

        // deathVFXPrefab com ParticleSystem
        GameObject vfx = new GameObject("VFX");
        _particleSystem = vfx.AddComponent<ParticleSystem>();
        typeof(EnemyLife).GetField("deathVFXPrefab", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(_enemyLife, vfx);

        // Invoca Start manualmente
        _enemyLife.Invoke("Start", 0f);
    }

    [Test]
    public void TakeDamage_ReducesLife()
    {
        _enemyLife.TakeDamage(10);

        float life = (float)typeof(EnemyLife)
            .GetField("_life", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(_enemyLife);

        Assert.AreEqual(13f, life, 0.01f);
    }

    [UnityTest]
    public IEnumerator KillEnemy_WhenLifeBelowZero_DisablesCollidersAndPlaysVFX()
    {
        _enemyLife.TakeDamage(50); // Deve matar

        yield return null;

        // VFX
        Assert.IsTrue(_particleSystem.isPlaying);

        // Colliders
        foreach (var col in _colliders)
        {
            Assert.IsFalse(col.enabled);
        }

        // Rigidbody parado
        Assert.AreEqual(Vector2.zero, _rigidbody.linearVelocity);

        // Slime parou de perseguir
        Assert.IsTrue(_fakeSlime.StopCalled);
    }

    [UnityTest]
    public IEnumerator DestroyEnemyAfterDead_DestroysObjectAfterDelay()
    {
        _enemyLife.DestroyEnemyAfterDead();

        yield return new WaitForSeconds(1.1f);

        Assert.IsTrue(_enemy == null || _enemy.Equals(null)); // Unity null check
    }

    [TearDown]
    public void Teardown()
    {
        if (_enemy != null)
            Object.DestroyImmediate(_enemy);
    }
}

}