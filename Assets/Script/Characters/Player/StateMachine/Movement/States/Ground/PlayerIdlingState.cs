using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Movementsystem
{
    public class PlayerIdlingState : PlayerGroundedState
    {
        public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableDate.MovementSpeedModifier = 0f;
            ResetVelocity();
        }
        public override void Update()
        {
            base.Update();

            if (        stateMachine.ReusableDate.MovementInput == Vector2.zero)
            {
                return;
            }
            OnMove();
         }

        #endregion
    }
}
