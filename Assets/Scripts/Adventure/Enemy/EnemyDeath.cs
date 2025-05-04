using Adventure.Enemy.Interfaces;
using UnityEngine;

namespace Enemy
{
    public class EnemyDeath: MonoBehaviour, IEnemyDeathHandler
    {
        public void HandleDeath(ParticleSystem deathVFXPrefab, Rigidbody2D rigidbody, Animator animator)
        {
            deathVFXPrefab.Play();
            rigidbody.linearVelocity = Vector2.zero;
            animator.Play("die");
        }
    }
}