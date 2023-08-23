using Movementsystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerMovementState
{
    public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {

    }
    #region Reusable Methobs

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
