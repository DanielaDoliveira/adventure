using Enemy;

namespace Adventure.Enemy.EnemyTests.Runtime
{
    public class TestableEnemyLife : EnemyLife
    {
        public float testTime { get; set; } = 0f;
        protected override float CurrentTime => testTime;
    }

}