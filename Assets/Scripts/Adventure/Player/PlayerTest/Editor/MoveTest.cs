using NUnit.Framework;
using UnityEngine;
using Player.CommandPattern;
using Adventure.Player;
using System.Reflection;

namespace Adventure.Player.PlayerTest.Editor
{
    public class MoveTest
    {
        private MockPlayerAnimations _animations;
        private GameObject _playerObject;
        private Move _move;
        private PlayerState _state;

        [SetUp]
        public void Setup()
        {
            _animations = new MockPlayerAnimations();
            _playerObject = new GameObject();
            _playerObject.AddComponent<Rigidbody2D>();
            _state = new PlayerState
            {
                Rigidbody = _playerObject.GetComponent<Rigidbody2D>(),
                CanAttack = true
            };

            _move = _playerObject.AddComponent<Move>();
            _move.Construct(_animations, _state);
        }
        
        [Test]
        public void Execute_SetsMovementInState()
        {
            var direction = new Vector2(1, 0);
            _move.Execute(direction);

            Assert.AreEqual(direction, _state.Movement);
        }
        [Test]
        public void MovementAnimationValidator_WithMovement_PlaysMovementAnimation()
        {
            _state.Movement = new Vector2(1, 0);
            _state.IsAttacking = false;

           
            typeof(Move)
                .GetMethod("MovementAnimationValidator", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.Invoke(_move, null);

            Assert.IsTrue(_animations.PlayedMovement);
            Assert.AreEqual(_state.Movement, _animations.MovementDirection);
            Assert.IsFalse(_animations.PlayedIdle);
            Assert.AreEqual(_state.Movement, _state.LastDirection);
        }
        [Test]
        public void MovementAnimationValidator_WithNoMovement_PlaysIdleAnimation()
        {
            _state.Movement = Vector2.zero;
            _state.LastDirection = new Vector2(0, -1);
            _state.IsAttacking = false;

            typeof(Move)
                .GetMethod("MovementAnimationValidator", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.Invoke(_move, null);

            Assert.IsTrue(_animations.PlayedIdle);
            Assert.AreEqual(_state.LastDirection, _animations.LastDirection);
            Assert.IsFalse(_animations.PlayedMovement);
        }

        [Test]
        public void FixedUpdate_SetsVelocityCorrectly()
        {
            // Arrange
            var gameObject = new GameObject();
            var rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
            _state.Rigidbody = rigidbody2D;
            _state.Movement = new Vector2(1, 0); 
            
            var move = gameObject.AddComponent<Move>();
            move.Construct(new MockPlayerAnimations(),_state);
            
            // Act
            move.FixedUpdate();
                // Assert
            Vector2 expectedVelocity = new Vector2(5f, 0f); // _speed = 5f por padrão
            Assert.AreEqual(expectedVelocity, rigidbody2D.linearVelocity);
        }
    }
}