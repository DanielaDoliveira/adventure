using Enemy;
using UnityEngine;

namespace Adventure.Enemy.EnemyTests.MOCKS
{
    public class FakeEnemyBehaviour : MonoBehaviour, IEnemyBehaviour
    {
        public bool StoppedPursuit { get; private set; } = false;

        public void PirsuitPlayer()=>throw new System.NotImplementedException();
        

        public void StopPirsuitPlayer()=> StoppedPursuit = true;
        
    }
}