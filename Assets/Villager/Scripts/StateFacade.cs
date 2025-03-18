using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace ForTheVillage.Villager
{
    public class StateFacade
    {
        private IState _curState;

        public void ChangeState(IState state)
        {
            if (_curState != null)
            {
                _curState.Exit();
            }
            _curState = state;
            _curState.Enter();
        }

        public void Execute()
        {
            if (_curState != null)
            {
                _curState.Tick();
            }
        }
    }
}
