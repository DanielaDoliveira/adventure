using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private PlayerAnimations animations;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float speed = 10f;
    void Start()
    {
        animations = GetComponent<PlayerAnimations>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
      
    }

    private void FixedUpdate()
    {
    rb.linearVelocity = new Vector2(movement.x * speed, movement.y * speed);
   
        switch (movement.x)
        {
            case >= 1:
                MoveRight();
         
                break;
            case <= -1:
                MoveLeft();
          
                break;
            case 0:
                animations.SetAnimation(PLAYER_STATE.IDLE_SIDE);
                break;

         

        
        }

        
 
        switch (movement.y)
        {
            case >= 1:
                MoveUp();
                break;
            case <=-1:
                MoveDown();
                break;
        }

    }

    

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        Debug.Log(movement);
       
    
        
    }
    public void MoveUp()
    {
        animations.SetAnimation(PLAYER_STATE.WALKING_BACK);
    }

    public void MoveDown()
    {
        animations.SetAnimation(PLAYER_STATE.WALKING);
    }

    public void MoveLeft()
    {
        spriteRenderer.flipX = true;
        animations.SetAnimation(PLAYER_STATE.WALKING_SIDE);
    }

    public void MoveRight()
    {
        spriteRenderer.flipX = false;
        animations.SetAnimation(PLAYER_STATE.WALKING_SIDE);
    }

   

  
}
