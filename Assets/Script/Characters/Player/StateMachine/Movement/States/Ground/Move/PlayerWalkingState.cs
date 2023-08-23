using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movementsystem
{
    public class PlayerWalkingState : PlayerMovingState
    {
        public PlayerWalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        #region ISatae Methods


        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableDate.MovementSpeedModifier = movementDate.WalkDate.SpeedModifier;
        }
        #endregion
        #region Input Methods

        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);
            stateMachine.ChangeState(stateMachine.RunningState);
        }

        #endregion

    }
}
