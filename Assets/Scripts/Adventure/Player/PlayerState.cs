using UnityEngine;

namespace Adventure.Player
{
    public class PlayerState
    {
        public bool IsAttacking { get; set; }
        public bool CanAttack { get; set; } = true;
        public Vector2 Movement { get; set; }
        public Vector2 LastDirection { get; set; }
        public Rigidbody2D Rigidbody { get; set; }
        public Animator Animator { get; set; }
    }
}