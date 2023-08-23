using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movementsystem
{
    public class PlayerClimbingState : PlayerMovingState
    {
        public PlayerClimbingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
    }
}
