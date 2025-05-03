
namespace Player
{
    public class PlayerDamageEffect : DamageEffect
    {
       public static PlayerDamageEffect Instance;
       
        void Awake()=> Instance = this;

    }
}
