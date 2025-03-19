using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private PlayerAnimations _animations;

    private Rigidbody2D _rb;
    private Vector2 _movement;
    public float speed = 10f;
    private Vector2 _lastDirection;
    public bool _attack;
    void Start()
    {
        _animations = GetComponent<PlayerAnimations>();
        _rb = GetComponent<Rigidbody2D>();
        
      

    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_movement.x * speed, _movement.y * speed);
    }

    void Update()
    {
    
             CheckAnimations();
       
    }

   
    
    public void Move(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
        
    }

    
    void CheckAnimations()
    {
     
        if (_movement != Vector2.zero)
        {
            _animations.SetAnimationMovement(PLAYER_STATE.WALKING,_lastDirection);
            _lastDirection = _movement;
            Debug.Log(_lastDirection);
        }
       
        if (_movement == Vector2.zero)
        {
            _animations.SetAnimationIdle(PLAYER_STATE.IDLE,_lastDirection);
          
        }

    }


  
}
