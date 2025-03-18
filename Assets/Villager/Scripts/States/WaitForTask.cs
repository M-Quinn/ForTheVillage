using System;
using UnityEngine;

namespace ForTheVillage.Villager
{
    public class WaitForTask : IState
    {
        private Action<string> _logAction;
        public WaitForTask(Action<string>logAction)
        {
            _logAction = logAction;
        }

        public void Enter()
        {
            _logAction?.Invoke("WaitForTask Enter");
        }

        public void Tick()
        {
            _logAction?.Invoke("WaitForTask Tick");
        }

        public void Exit()
        {
            _logAction?.Invoke("WaitForTask Exit");
        }
    }
}
