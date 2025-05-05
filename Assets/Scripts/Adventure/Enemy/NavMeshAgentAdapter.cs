using Adventure.Enemy.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class NavMeshAgentAdapter: INavMeshAgent
    {
       
      
        private NavMeshAgent _agent;
      
        public NavMeshAgentAdapter(NavMeshAgent agent)=>_agent = agent;
        
        public Vector3 destination
        {
            get => _agent.destination;
            set => _agent.destination = value;
        }
        public bool updateRotation
        {
            get => _agent.updateRotation;
            set => _agent.updateRotation = value;
        }

        public bool updateUpAxis
        {
            get => _agent.updateUpAxis;
            set => _agent.updateUpAxis = value;
        }

        public bool pathPending => _agent.pathPending;
        public float remainingDistance => _agent.remainingDistance;
        
        public void SetDestination(Vector3 target)=>_agent.SetDestination(target);
           
        
    }
}