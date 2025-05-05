using Adventure.Enemy.Interfaces;
using UnityEngine;

namespace Adventure.Enemy.EnemyTests.MOCKS
{
    public class FakeNavMeshAgent: INavMeshAgent
    {
        public bool updateRotation { get; set; }
        public bool updateUpAxis { get; set; }
        public Vector3 destination { get; set; }
        public bool pathPending { get; set; }
        public float remainingDistance { get; set; }

        public void SetDestination(Vector3 target)
        {
             destination = target;
        }
    }
}