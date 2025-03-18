using System;
using UnityEngine;

namespace ForTheVillage.Villager
{
    public class GoToResource:IState
    {
        private Action<string> _logAction;
        public GoToResource(Action<string> logAction)
        {
            _logAction = logAction;
        }

        public void Enter()
        {
            _logAction?.Invoke("GoToResource Enter");
        }

        public void Tick()
        {
            _logAction?.Invoke("GoToResource Tick");
        }

        public void Exit()
        {
            _logAction?.Invoke("GoToResource Exit");
        }
    }
}
