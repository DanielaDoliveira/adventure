
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using Player.CommandPattern;
using Player;
using UnityEngine.TestTools;


namespace Adventure.Player.PlayerTest.Editor
{
    
    public class AttackTest
    {
        private Attack _attack;
        private PlayerState _state;
        private IPlayerAnimations _animations;
        private GameObject _attackCollider;
        MockPlayerAnimations _playerAnimations;
      
        [SetUp]
        public void Setup()
        {
             _playerAnimations = new MockPlayerAnimations();
            
        }

        [Test]
        public void Execute_AttackAllowed_TriggersAttackAnimation()
        {
            // Arrange
           
            var playerState = new PlayerState
            {
                IsAttacking = false,
                CanAttack = true
            };

            var attackObject = new GameObject();
            var attack = attackObject.AddComponent<Attack>();
            attack.attackCollider = new GameObject();

            attack.Construct(_playerAnimations, playerState);

            Vector2 direction = Vector2.up;
 
            attack.Execute(direction);

            Assert.IsTrue(playerState.IsAttacking);
            Assert.IsFalse(playerState.CanAttack);
            Assert.IsTrue(_playerAnimations.AttackPlayed);
            Assert.AreEqual(direction, _playerAnimations.AttackDirection);
            Assert.AreEqual(LAYER_TYPE.ATTACKING, _playerAnimations.AttackLayer);
            Assert.AreEqual(attack.attackCollider, _playerAnimations.ColliderPassed);
        }

        [Test]
        public void Finish_WhenNotAttacking_PlaysIdleIfNotMoving()
        {
            // Arrange
            var playerState = new PlayerState
            {
                IsAttacking = false, 
                Movement = Vector2.zero,
                LastDirection = Vector2.down
            };

            var attackObject = new GameObject();
            var attack = attackObject.AddComponent<Attack>();
            attack.attackCollider = new GameObject();
            attack.Construct(_playerAnimations, playerState);

            // Act
            attack.Finish();

            // Assert
            Assert.IsFalse(_playerAnimations.AttackFinished); 
            Assert.IsTrue(_playerAnimations.PlayedIdle); 
            Assert.IsFalse(_playerAnimations.PlayedMovement); 
            Assert.AreEqual(Vector2.down, _playerAnimations.LastDirection);
        }
        [Test]
        public void Finish_WhenAttackingAndMoving_EndsAttackAndPlaysMovement()
        {
           
            var playerState = new PlayerState
            {
                IsAttacking = true,
                Movement = new Vector2(1, 0),
                LastDirection = new Vector2(0, -1)
            };
            var attackObject = new GameObject();
            var attack = attackObject.AddComponent<Attack>();
            var attackCollider = new GameObject();
            attack.attackCollider = attackCollider;
            attack.Construct(_playerAnimations, playerState);
            
            // Act
            attack.Finish();

            // Assert
            Assert.IsFalse(playerState.IsAttacking);
            Assert.IsTrue(_playerAnimations.AttackFinished);
            Assert.AreEqual(attackCollider, _playerAnimations.ColliderPassedToFinish);
            Assert.IsTrue(_playerAnimations.PlayedMovement);
            Assert.IsFalse(_playerAnimations.PlayedIdle);
        }

        [Test]
        public void Finish_WhenAttackingAndNotMoving_EndsAttackAndPlaysIdle()
        {
            // Arrange
           
            var playerState = new PlayerState
            {
                IsAttacking = true,
                Movement = Vector2.zero,
                LastDirection = new Vector2(0, -1)
            };
            var attackObject = new GameObject();
            var attack = attackObject.AddComponent<Attack>();
            var attackCollider = new GameObject();
            attack.attackCollider = attackCollider;
            attack.Construct(_playerAnimations, playerState);
            // Act
            attack.Finish();

            // Assert
            Assert.IsFalse(playerState.IsAttacking);
            Assert.IsTrue(_playerAnimations.AttackFinished);
            Assert.IsTrue(_playerAnimations.PlayedIdle);
            Assert.IsFalse(_playerAnimations.PlayedMovement);
            Assert.AreEqual(playerState.LastDirection, _playerAnimations.LastDirection);

        }
        
        [Test]
        public void Finish_ShouldBeIdempotent_WhenNotAttacking()
        {
            // Arrange
            var playerState = new PlayerState
            {
                IsAttacking = false,
                Movement = Vector2.zero,
                LastDirection = Vector2.down
            };

            var attackObject = new GameObject();
            var attack = attackObject.AddComponent<Attack>();
            attack.attackCollider = new GameObject();
            attack.Construct(_playerAnimations, playerState);

            // Act
            attack.Finish();

            // Assert
            Assert.IsFalse(_playerAnimations.AttackFinished); 
            Assert.IsFalse(_playerAnimations.PlayedMovement);
            Assert.IsTrue(_playerAnimations.PlayedIdle); 
        }
        
        [UnityTest]
        public IEnumerator CanAttackCoroutine_SetsCanAttackToTrue_AfterDelay()
        {
            // Arrange
            
            var playerState = new PlayerState
            {
                CanAttack = false,
                IsAttacking = false
            };
            var gameObject = new GameObject();
            var attack = gameObject.AddComponent<Attack>();
            attack.attackCollider = new GameObject();
            attack.Construct(_playerAnimations, playerState);
            // Act
            attack.Execute(Vector2.down); 

            Assert.IsFalse(playerState.CanAttack);

           
            yield return new WaitForSeconds(0.6f);

            // Assert
            Assert.IsTrue(playerState.CanAttack);
            
        }
        [Test]
        public void Execute_ShouldNotAttack_IfCanAttackIsFalse()
        {
            // Arrange
            var playerState = new PlayerState
            {
                IsAttacking = false,
                CanAttack = false
            };

            var attackObject = new GameObject();
            var attack = attackObject.AddComponent<Attack>();
            attack.attackCollider = new GameObject();
            attack.Construct(_playerAnimations, playerState);

            Vector2 direction = Vector2.right;

            // Act
            attack.Execute(direction);

            // Assert
            Assert.IsFalse(playerState.IsAttacking);
            Assert.IsFalse(_playerAnimations.AttackPlayed); 
        }
        [Test]
        public void Execute_ShouldNotAttack_IfAlreadyAttacking()
        {
            // Arrange
            var playerState = new PlayerState
            {
                IsAttacking = true,  
                CanAttack = true
            };

            var attackObject = new GameObject();
            var attack = attackObject.AddComponent<Attack>();
            attack.attackCollider = new GameObject();
            attack.Construct(_playerAnimations, playerState);

            Vector2 direction = Vector2.left;

            // Act
            attack.Execute(direction);

            // Assert
            Assert.IsTrue(playerState.IsAttacking); 
            Assert.IsFalse(_playerAnimations.AttackPlayed); 
        }

        
        
    }
}