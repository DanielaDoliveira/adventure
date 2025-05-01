using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class SlimeBehaviours: MonoBehaviour,IEnemyBehaviour
    {
     
  
      
       
        public Transform target; 
        private NavMeshAgent _agent;
        public float detectionRadius = 2f;
        private bool _isChasing = false;
        private int _currentWaypointIndex = 0;
        public Transform[] waypoints;
        void Start()
        {
           
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        void Update()
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);
            if (distanceToPlayer <= detectionRadius)
            {
                _isChasing = true;
               PirsuitPlayer();
            }
            else
            {
                if (_isChasing)
                {
                    _isChasing = false;
                    GoToNextWaypoint();
                }
                if (!_agent.pathPending && _agent.remainingDistance < 0.1f)  GoToNextWaypoint();
                

            }

        }
        void GoToNextWaypoint()
        {
            if (waypoints.Length == 0)  return;

            _agent.destination = waypoints[_currentWaypointIndex].position;
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            
            if (_currentWaypointIndex > waypoints.Length)  _currentWaypointIndex = 0;
            
        }
        
   

        public void PirsuitPlayer()=> _agent.SetDestination(target.position);
        
        public void StopPirsuitPlayer()=>target = null;
        


        
      
    }
}