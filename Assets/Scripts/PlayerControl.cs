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
    bool canAttack = true;
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

    public void Attack(InputAction.CallbackContext context)
    {
        if (!_attack && canAttack  )
        {
                _attack = true;
                _animations.SetAnimationAttacking(PLAYER_STATE.ATTACKING,_lastDirection,_attack);
                StartCoroutine(CanAttack());
            
        }
    }

    public void FinishAttack()
    {
      
        
        if (_attack)
        {
          _animations.FinishAttack();
            _attack = false;
        }
        
    }

    IEnumerator CanAttack()
    {
      
        if (_attack)
        {
            canAttack = false;
            yield return new WaitForSeconds(0.5f);
            canAttack = true;
        }
            
    }
    
    void CheckAnimations()
    {
     
        if (_movement != Vector2.zero && !_attack)
        {
            canAttack = false;
            _animations.SetAnimationMovement(PLAYER_STATE.WALKING,_movement);
            _lastDirection = _movement;
            Debug.Log(_lastDirection);
        }
       
        if (_movement == Vector2.zero && !_attack)
        {
            canAttack = true;
            _animations.SetAnimationIdle(PLAYER_STATE.IDLE,_lastDirection);
          
        }

    }


  
}
