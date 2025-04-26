namespace Enemy
{
    public interface IEnemyBehaviour
    {
        void BehaviourTakingDamage();
        void KnockbackBehaviour();
        void PirsuitPlayer();
        void StopPirsuitPlayer();
    }
}