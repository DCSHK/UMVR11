using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movementsystem
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void Handleinput();
        public void Update();
        public void PhysicsUpdate();

    }
}
