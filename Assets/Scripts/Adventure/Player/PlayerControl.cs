using Adventure.Player;
using Player.CommandPattern;
using Player.Singletons;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player
{
    public class PlayerControl : MonoBehaviour
    {
    
        public static PlayerControl Instance;
        private Attack _attackCommand; 
        private Move _moveCommand;
        private PlayerState _playerState;
        [Inject] public void Construct(PlayerState playerState) => _playerState = playerState;

        void Awake()=>Instance = this;
        
    
        void Start()
        {

            _playerState.Rigidbody = GetComponent<Rigidbody2D>();
            _playerState.Animator = GetComponent<Animator>();
         
            _attackCommand = GetComponent<Attack>();
            
            _moveCommand = GetComponent<Move>();
         
        }
    
        public void Move(InputAction.CallbackContext context) => _moveCommand.Execute(context.ReadValue<Vector2>());
        

        public void Attack(InputAction.CallbackContext context)=> _attackCommand.Execute(_playerState.LastDirection);
        
 
  
  

 
    }
}
