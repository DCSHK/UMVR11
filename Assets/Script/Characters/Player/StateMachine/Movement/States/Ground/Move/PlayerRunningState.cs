using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movementsystem
{
    public class PlayerRunningState : PlayerMovingState
    {
        public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            stateMachine.ReusableDate.MovementSpeedModifier = movementDate.RunDate.SpeedModifier;
        }
        #endregion


        #region Input Methods

        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);
            stateMachine.ChangeState(stateMachine.WalkingState);
        }

        #endregion
    }
}
