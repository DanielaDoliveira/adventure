using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private PlayerAnimations _animations;
    public static PlayerControl instance;
    private Rigidbody2D _rb;
    private Vector2 _movement;
    public float speed = 10f;
    private Vector2 _lastDirection;
   [HideInInspector] public bool _attack;
    bool canAttack = true;

    void Awake()
    {
        instance = this;
    }
    
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
                _animations.SetAnimationAttacking(LAYER_TYPE.ATTACKING,_lastDirection,_attack);
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
            _animations.SetAnimationMovement(LAYER_TYPE.RUNNING,_movement);
            _lastDirection = _movement;
       
        }
       
        if (_movement == Vector2.zero && !_attack)
        {
            canAttack = true;
            _animations.SetAnimationIdle(LAYER_TYPE.IDLE,_lastDirection);
          
        }

    }

 
}
