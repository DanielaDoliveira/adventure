using Player;
using UnityEngine;
using NUnit.Framework;
using Player.CommandPattern;
public class MockPlayerAnimations : IPlayerAnimations

{   
    
    public bool IsAttacking;
    public bool AttackPlayed = false;
    public bool AttackFinished = false;
    public LAYER_TYPE AttackLayer;
    public Vector2 AttackDirection;
    
    public bool PlayedMovement = false;
    public Vector2 LastDirection;
    public Vector2 MovementDirection;
    public LAYER_TYPE MovementLayer;
    public bool LayersChecked = false;
    public LAYER_TYPE LastLayer;
    public bool PlayedIdle = false;
    public GameObject ColliderPassed;
    public GameObject ColliderPassedToFinish;
    public void PlayAttackAnimation(LAYER_TYPE layer, Vector2 direction, bool isAttacking, GameObject attackCollider)
    {
        AttackPlayed = true;
        AttackLayer = layer;
        AttackDirection = direction;
        IsAttacking = isAttacking;
        ColliderPassed = attackCollider;
        
    }

    public void FinishAttackAnimation(GameObject attackCollider)
    {
        AttackFinished = true;
        ColliderPassedToFinish = attackCollider;
    }
    public void CheckLayers(LAYER_TYPE layer)
    {
        LayersChecked = true;
    }

    public void ResetAllLayers()
    {
        throw new System.NotImplementedException();
    }

    public void PlayIdleAnimation(LAYER_TYPE layer, Vector2 direction)
    {
        PlayedIdle = true;
        LastLayer = layer;
        LastDirection = direction;
    }

    public void PlayMovementAnimation(LAYER_TYPE layer, Vector2 direction)
    {
        PlayedMovement = true;
        MovementDirection = direction;
        MovementLayer = layer;
    }
}