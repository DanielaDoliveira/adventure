using System;
using System.Linq;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour, IPlayerAnimations
{
    
    
public GameObject attack_collider;

private void Start()
{
    attack_collider.SetActive(false);
}

public void SetAnimationMovement(LAYER_TYPE layer,Vector2 direction)
    {
        GetComponent<Animator>().SetFloat("x_mov",direction.x);
        GetComponent<Animator>().SetFloat("y_mov",direction.y);
        CheckLayers(layer); 
      
            
        
    }

    public void SetAnimationIdle(LAYER_TYPE layer, Vector2 direction)
    {
        CheckLayers(layer);
        GetComponent<Animator>().SetFloat("last_direction_x",direction.x);
        GetComponent<Animator>().SetFloat("last_direction_y",direction.y);
     
       
     
    }

    public void SetAnimationAttacking(LAYER_TYPE layer, Vector2 direction, bool isAttacking)
    {
        
        CheckLayers(layer);
        attack_collider.SetActive(true);
            GetComponent<Animator>().SetFloat("last_direction_x",direction.x);
            GetComponent<Animator>().SetFloat("last_direction_y",direction.y);
            GetComponent<Animator>().SetBool("isAttacking",isAttacking);
          
         
       
  

    }

  
    public void FinishAttack( )
    {
        attack_collider.SetActive(false);
        GetComponent<Animator>().SetBool("isAttacking",false);
        GetComponent<Animator>().SetLayerWeight(3,0);

     
    }

    public void CheckLayers(LAYER_TYPE layer)
    {
        int getLayer = layer.GetHashCode();
        SetAllLayersToDefault();
       GetComponent<Animator>().SetLayerWeight(getLayer,1);
       
    }

    public void SetAllLayersToDefault()
    {
        GetComponent<Animator>().SetLayerWeight(0,0);
        GetComponent<Animator>().SetLayerWeight(1,0);
        GetComponent<Animator>().SetLayerWeight(2,0);
        GetComponent<Animator>().SetLayerWeight(3,0);
    }

  

    
}
