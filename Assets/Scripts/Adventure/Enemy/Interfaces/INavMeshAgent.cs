using UnityEngine;

namespace Adventure.Enemy.Interfaces
{
    public interface INavMeshAgent
    {
        bool updateRotation { get; set; }
        bool updateUpAxis { get; set; }
        Vector3 destination { get; set; }
        bool pathPending { get; }
        float remainingDistance { get; }
        void SetDestination(Vector3 target);
        
        
    }
}