using Player.CommandPattern;
using Player.Singletons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerControl : MonoBehaviour
    {
    
        public static PlayerControl Instance;
        private Attack _attackCommand; 
        private Move _moveCommand;
        void Awake()=>Instance = this;
        
    
        void Start()
        {
            PlayerSingleton.Rigidbody = GetComponent<Rigidbody2D>();
            PlayerSingleton.Animator = GetComponent<Animator>();
            _attackCommand = GetComponent<Attack>();
            _moveCommand = GetComponent<Move>();
         
        }
    
        public void Move(InputAction.CallbackContext context) => _moveCommand.Execute(context.ReadValue<Vector2>());
        

        public void Attack(InputAction.CallbackContext context)
        {
            if (!PlayerSingleton.IsAttacking && PlayerSingleton.CanAttack )
                _attackCommand.Execute(PlayerSingleton.LastDirection);
        }
 
  
  

 
    }
}
