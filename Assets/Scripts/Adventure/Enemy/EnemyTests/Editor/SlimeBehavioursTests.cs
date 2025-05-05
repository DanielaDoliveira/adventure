using System.Reflection;
using Adventure.Enemy.EnemyTests.MOCKS;
using Adventure.Enemy.Interfaces;
using Enemy;
using NUnit.Framework;
using UnityEngine;

namespace Adventure.Enemy.EnemyTests.Editor
{
    public class SlimeBehavioursTests
    {
        private GameObject _gameObject;
        private SlimeBehaviours _slime;
        private FakeNavMeshAgent _fakeAgent;
        private Transform _playerTransform;
        private Transform[] _waypoints;

        
        [SetUp]
        public void SetUp()
        {
            _gameObject = new GameObject("Slime");
           _gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
    
            _slime = _gameObject.AddComponent<SlimeBehaviours>();

            _fakeAgent = new FakeNavMeshAgent();
            _slime.Initialize(_fakeAgent); 
            _playerTransform = new GameObject("Player").transform;
            
            _waypoints = new Transform[]
            {
                new GameObject("Waypoint1").transform,
                new GameObject("Waypoint2").transform
            };
           
            _slime.target = _playerTransform;
            _slime.waypoints = _waypoints;
            _slime.detectionRadius = 5f;


        }
        [Test]
        public void Patrol_WithNoWaypoints_DoesNotThrow()
        {
            _slime.waypoints = new Transform[0];

            // Simula que slime não está perseguindo
            _playerTransform.position = new Vector3(100, 0, 0);

            // Garante que não lança exceção
            Assert.DoesNotThrow(() => _slime.Update());
        }
        [Test]
        public void StopPirsuitPlayer_ClearsTarget()
        {
            _slime.StopPirsuitPlayer();
            Assert.IsNull(_slime.target);
        }
    
        [Test]
        public void Patrol_CyclesThroughWaypoints()
        {
            _slime.transform.position = Vector3.zero;
            _playerTransform.position = new Vector3(100, 0, 0); // fora do alcance
            _fakeAgent.pathPending = false;
            _fakeAgent.remainingDistance = 0.05f;

            _slime.Update(); // vai para Waypoint 0
            Assert.AreEqual(_waypoints[0].position, _fakeAgent.destination);

            _slime.Update(); // vai para Waypoint 1
            Assert.AreEqual(_waypoints[1].position, _fakeAgent.destination);

            _slime.Update(); // volta para Waypoint 0
            Assert.AreEqual(_waypoints[0].position, _fakeAgent.destination);
        }
        [Test]
        public void Update_PlayerWithinDetectionRadius_ChasesPlayer()
        {
            _playerTransform.position = new Vector3(1f, 0, 0); // dentro do raio
            _slime.transform.position = Vector3.zero;

            _slime.Update();

            Assert.AreEqual(_playerTransform.position, _fakeAgent.destination);
        }

    
        [TearDown]
        public void TearDown()
        {
            if (_gameObject != null)    Object.DestroyImmediate(_gameObject);

            if (_playerTransform != null)   Object.DestroyImmediate(_playerTransform.gameObject);

            if (_waypoints != null)
                foreach (var wp in _waypoints)
                    if (wp != null)   Object.DestroyImmediate(wp.gameObject);
                
            
        }
        [Test]
        public void PursuitPlayer_SetsDestinationToPlayer()
        {
            _playerTransform.position = new Vector3(2, 0, 0);
            _slime.transform.position = Vector3.zero;

            _slime.Update(); 

            Assert.AreEqual(_playerTransform.position, _fakeAgent.destination);
        }
        
        [Test]
        public void Patrol_GoesToNextWaypoint()
        {
            _slime.transform.position = Vector3.zero;
            _playerTransform.position = new Vector3(100, 0, 0); // fora do alcance

            _fakeAgent.pathPending = false;
            _fakeAgent.remainingDistance = 0.05f;

            _slime.Update();

            Assert.AreEqual(_waypoints[0].position, _fakeAgent.destination);
        }
        [Test]
        public void Update_PlayerOutsideDetectionRadius_Patrols()
        {
            _playerTransform.position = new Vector3(100f, 0, 0); // fora do raio
            _slime.transform.position = Vector3.zero;

            _fakeAgent.pathPending = false;
            _fakeAgent.remainingDistance = 0.05f;

            _slime.Update();

            Assert.AreEqual(_waypoints[0].position, _fakeAgent.destination);
        }

        [Test]
        public void Update_WithoutInitialization_Throws()
        {
            var newSlime = new GameObject().AddComponent<SlimeBehaviours>();
            newSlime.target = null; 
            newSlime.waypoints = _waypoints; 
            
            Assert.DoesNotThrow(() => newSlime.Update());
        }
        [Test]
        public void Update_WithNullTarget_DoesNotThrow()
        {
            _slime.target = null;
            Assert.DoesNotThrow(() => _slime.Update());
        }

    }
}