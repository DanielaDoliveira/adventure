
using Enemy;

namespace Player
{
    public class PlayerDamageEffect : DamageEffect, IDamageEffect
    {
       public static PlayerDamageEffect Instance;
       
        void Awake()=> Instance = this;

    }
}
