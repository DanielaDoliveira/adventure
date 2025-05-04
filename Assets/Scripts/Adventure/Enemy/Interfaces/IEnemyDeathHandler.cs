using UnityEngine;

namespace Adventure.Enemy.Interfaces
{
    public interface IEnemyDeathHandler
    
    {  void HandleDeath(ParticleSystem deathVFXPrefab, Rigidbody2D rigidbody, Animator animator);
    }
}