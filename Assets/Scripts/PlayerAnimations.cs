using UnityEngine;

public class PlayerAnimations : MonoBehaviour, IPlayerAnimations
{
    
    public void SetAnimation(PLAYER_STATE state)
    {
        int playerState = state.GetHashCode();
        LAYER_TYPE layerName = IdentifyLayerName(state); 
   
        SetAllLayerWeights(layerName);
        
        GetComponent<Animator>().SetInteger("state",playerState);
    }

    public LAYER_TYPE IdentifyLayerName(PLAYER_STATE state)
    {
        bool isIdle = state == PLAYER_STATE.IDLE || state == PLAYER_STATE.IDLE_BACK || state == PLAYER_STATE.IDLE_SIDE;
        bool isWalking = state == PLAYER_STATE.WALKING || state == PLAYER_STATE.WALKING_BACK || state == PLAYER_STATE.WALKING_SIDE;

        if (isIdle)
            return LAYER_TYPE.IDLE;
        if (isWalking)
            return LAYER_TYPE.RUNNING;
        else
        {
            return LAYER_TYPE.DEFAULT;
        }
       

    }

    public void SetAllLayerWeights(LAYER_TYPE layerType)
    {
        float weight = 0;
        int layerIndex = layerType.GetHashCode();
        
        switch (layerType)
        {
            case LAYER_TYPE.IDLE:
                GetComponent<Animator>().SetLayerWeight(2,weight);
                
                break;
            case LAYER_TYPE.RUNNING:
                GetComponent<Animator>().SetLayerWeight(1,weight);
                break;
        }
        GetComponent<Animator>().SetLayerWeight(layerIndex,1);
      
    }
}
