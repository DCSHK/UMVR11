using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movementsystem
{
    public class PlayerWalkingState : PlayerGroundedState
    {
        public PlayerWalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        #region ISatae Methods


        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableDate.MovementSpeedModifier = 0.225f;
        }
        #endregion
        
    }
}
