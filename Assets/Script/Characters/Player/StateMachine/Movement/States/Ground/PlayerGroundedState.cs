using Movementsystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.EventSystems;

public class PlayerGroundedState : PlayerMovementState
{
    private SlopeData slopeData;
    public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        slopeData = stateMachine.Player.ColliderUtiliry.SlopeData;
    }
    #region IState Methods
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Float();
    }
    #region Main Methobs
    private void Float()
    {
        Vector3 capsculeColliderCenterInWorldSpace = stateMachine.Player.ColliderUtiliry.CapsuleColliderData.Collider.bounds.center;
        Ray downwardRayFromCapsuleCenter = new Ray(capsculeColliderCenterInWorldSpace, Vector3.down);
        if (Physics.Raycast(downwardRayFromCapsuleCenter , out RaycastHit hit, slopeData.FloatRayDistance , stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
        {
            float distanceToFloatPoint = stateMachine.Player.ColliderUtiliry.CapsuleColliderData.ColliderCenterInLocalSpace.y * stateMachine.Player.transform.localScale.y - hit.distance;
            if (distanceToFloatPoint== 0f) 
            {
                return;
            }
            float amountToLift = distanceToFloatPoint * slopeData.StepReachForce - GetPlayerVerticalVelocity().y;
            Vector3 liftForce = new Vector3(0f, amountToLift, 0f);
            stateMachine.Player.Rigidbody.AddForce(liftForce,ForceMode.VelocityChange);
        }
    }
    #endregion

    #endregion
    #region Reusable Methods

    protected override void AddInputActionCallbacks()
    {
        base.AddInputActionCallbacks();
        stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
    }



    protected override void RemoveInputActionCallbacks()
    {
        base.RemoveInputActionCallbacks();
        stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;

    }

    protected virtual void OnMove()
    {
        if (stateMachine.ReusableDate.shouldWalk)
        {
            stateMachine.ChangeState(stateMachine.WalkingState);
            return;
        }
        stateMachine.ChangeState(stateMachine.RunningState);
    }

    #endregion
    #region Input Methods

    //protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
    //{
    //    base.OnWalkToggleStarted(context);
    //    stateMachine.ChangeState(stateMachine.RunningState);
    //}
    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }
    #endregion
}
