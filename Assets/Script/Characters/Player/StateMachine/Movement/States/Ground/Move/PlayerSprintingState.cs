using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movementsystem
{
    public class PlayerSprintingState : PlayerMovingState
    {
        public PlayerSprintingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
    }
}
