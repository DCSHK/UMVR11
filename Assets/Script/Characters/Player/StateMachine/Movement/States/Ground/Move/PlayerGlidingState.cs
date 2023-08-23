using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movementsystem
{
    public class PlayerGlidingState : PlayerMovingState
    {
        public PlayerGlidingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
    }
}
