using System.Collections;
using Adventure.Enemy.Interfaces;
using Player;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;
namespace Enemy
{
    public class SlimeBehaviours: MonoBehaviour,IEnemyBehaviour
    {
     
        private INavMeshAgent _agent;
        
  
      [BoxGroup("Pathfinding")]
       [Title("Player")]
      [LabelText("Player target")]
        public Transform target; 
        
      
        [BoxGroup("Pathfinding")]
        [Title("Waypoints")]
        [LabelText("Waypoints when enemy can't detect player")]
        public Transform[] waypoints;
        
        [BoxGroup("Pathfinding")]
        [LabelText("Detect radius of enemy")]
        [MinValue((2f))]
        public float detectionRadius = 2f;
        
         
        private bool _isChasing = false;
        private int _currentWaypointIndex = 0;
        
        public SlimeBehaviours(INavMeshAgent agent)=>_agent = agent;

        private bool IsPlayerInRange()
        {
            if (target == null) return false;
           return Vector2.Distance(transform.position, target.position) <= detectionRadius;
        }
        
        void Start()
        {

            if (_agent == null)
            {
                var realAgent = GetComponent<NavMeshAgent>();
                Initialize(new NavMeshAgentAdapter(realAgent));

            }
            if (target == null)
            {
                Debug.LogError("Target is not assigned. Please assign a target for the SlimeBehaviours.");
            }
            
        }
        public void Initialize(INavMeshAgent agent)
        {
            _agent = agent;
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }
        public void Update()
        {
            if (target == null)
            {
                Debug.LogWarning("Target is not assigned, skipping update.");
                return; // Ou tratar de outra forma, se necessário
            }

            if (IsPlayerInRange())   PirsuitPlayer();
            else Patrol();
        }
        void GoToNextWaypoint()
        {
            if (waypoints.Length == 0)  return;
            SetAgentDestinationToCurrentWaypoint();
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            if (_currentWaypointIndex > waypoints.Length)  _currentWaypointIndex = 0;
            
        }
        private void SetAgentDestinationToCurrentWaypoint()=>_agent.destination = waypoints[_currentWaypointIndex].position;
        
   

        public void PirsuitPlayer()
        {
            _isChasing = true;
            _agent.SetDestination(target.position);
        }

        public void Patrol()
        {
            
            if (_isChasing) _isChasing = false;
            
            if (!_agent.pathPending && _agent.remainingDistance < 0.1f)  GoToNextWaypoint();
        }

        public virtual void StopPirsuitPlayer()=>target = null;
        


        
      
    }
}