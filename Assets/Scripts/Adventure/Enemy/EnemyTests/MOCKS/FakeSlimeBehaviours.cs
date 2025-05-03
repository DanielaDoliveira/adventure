using Enemy;

namespace Adventure.Enemy.EnemyTests.MOCKS
{
   public class FakeSlimeBehaviours : SlimeBehaviours
   {
      public bool StopCalled { get; private set; }

      public override void StopPirsuitPlayer() =>StopCalled = true;
   
   }
}
