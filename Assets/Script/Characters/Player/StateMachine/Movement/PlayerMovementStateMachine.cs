using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movementsystem
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public Player Player { get; }
        public PlayerStateReusableDate ReusableDate { get; }
        public PlayerIdlingState IdlingState { get; }
        public PlayerWalkingState WalkingState { get; }
        public PlayerRunningState RunningState { get; }
        public PlayerSprintingState SprintingState { get; }
        public PlayerGlidingState GlidingState { get; }
        public PlayerClimbingState ClimbingState { get; }
        public PlayerMovementStateMachine(Player player)
        {
            Player = player;
            ReusableDate = new PlayerStateReusableDate();
            IdlingState = new PlayerIdlingState(this);
            WalkingState = new PlayerWalkingState(this);
            RunningState = new PlayerRunningState(this);
            SprintingState = new PlayerSprintingState(this);
            GlidingState = new PlayerGlidingState(this);
            ClimbingState = new PlayerClimbingState(this);
        }

    }
}
