using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movementsystem
{
    public abstract class StateMachine //abstract 把類別變成抽象類
    {
        protected IState currentState;
        public void ChangeState(IState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }

        public void Handleinput()
        {
            currentState?.Handleinput();
        }
        public void Update()
        {
            currentState?.Update();
        }
        public void PhysicsUpdate()
        {
            currentState?.PhysicsUpdate();
        }
    }
}
