using UnityEngine;

public class PlayerAnimations : MonoBehaviour, IPlayerAnimations
{
    
    

    public void SetAnimationMovement(PLAYER_STATE state,Vector2 direction)
    {
        GetComponent<Animator>().SetFloat("x_mov",direction.x);
        GetComponent<Animator>().SetFloat("y_mov",direction.y);
        
            GetComponent<Animator>().SetLayerWeight(2,1);
            GetComponent<Animator>().SetLayerWeight(1,0);
            GetComponent<Animator>().SetLayerWeight(3,0);
        
    }

    public void SetAnimationIdle(PLAYER_STATE state, Vector2 direction)
    {
        GetComponent<Animator>().SetFloat("last_direction_x",direction.x);
        GetComponent<Animator>().SetFloat("last_direction_y",direction.y);
        
        GetComponent<Animator>().SetLayerWeight(2,0);
        GetComponent<Animator>().SetLayerWeight(1,1);
        GetComponent<Animator>().SetLayerWeight(3,0);
    }

    public void SetAnimationAttacking(PLAYER_STATE state, Vector2 direction, bool isAttacking)
    {

            GetComponent<Animator>().SetFloat("last_direction_x",direction.x);
            GetComponent<Animator>().SetFloat("last_direction_y",direction.y);
            GetComponent<Animator>().SetBool("isAttacking",isAttacking);
            GetComponent<Animator>().SetLayerWeight(2,0);
            GetComponent<Animator>().SetLayerWeight(1,0);
            GetComponent<Animator>().SetLayerWeight(3,1);
        
  

    }

  
    public void FinishAttack( )
    {
        
        GetComponent<Animator>().SetBool("isAttacking",false);
        GetComponent<Animator>().SetLayerWeight(2,1);
        GetComponent<Animator>().SetLayerWeight(3,0);

     
    }

  

    
}
