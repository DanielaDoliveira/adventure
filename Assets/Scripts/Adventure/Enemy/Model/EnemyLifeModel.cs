using UnityEngine;

namespace Enemy.Model
{
    public class EnemyLifeModel
    {
        public float life { get; private set; }
        public bool IsDead => life <= 0;
        public EnemyLifeModel(float initialLife)=> life = Mathf.Max(0, initialLife);

        public bool TakeDamage(float damage)
        {
            if (IsDead) return false;
            life -= damage;
            return IsDead;
        }
            
        
    }
}