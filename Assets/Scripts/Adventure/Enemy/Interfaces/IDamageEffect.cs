using UnityEngine;

namespace Enemy
{
    public interface IDamageEffect
    {
        void TakeDamage(Vector2 pos, Color damageColor);
    }
}