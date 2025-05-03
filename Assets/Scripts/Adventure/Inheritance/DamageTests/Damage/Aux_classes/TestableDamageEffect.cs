using System.Reflection;

namespace Adventure.Player.PlayerTest.Aux_classes
{
    public class TestableDamageEffect: DamageEffect
    {
        public bool IsTakingDamage => typeof(DamageEffect)
            .GetField("_isTakingDamage", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(this) as bool? ?? false;
    }
}