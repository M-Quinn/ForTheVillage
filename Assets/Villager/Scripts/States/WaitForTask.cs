using System;
using ForTheVillage.Resources;
using ForTheVillage.Village;
using UnityEngine;

namespace ForTheVillage.Villager
{
    public class WaitForTask : IState
    {
        private ResourceController _resourceController; 
        private VillageController _villageController;
        private Action<string> _logAction;

        private float _delay = 1.0f;
        private float _delayTimer = 0.0f;
        
        public WaitForTask(ref ResourceController target, ref VillageController village, Action<string>logAction)
        {
            _resourceController = target;
            _villageController = village;
            _logAction = logAction;
        }

        public void Enter()
        {
            _resourceController = _villageController.RequestNextResource();
            _delayTimer = _delay + Time.time;
        }

        public void Tick()
        {
            if (Time.time >= _delayTimer)
            {
                _resourceController = _villageController.RequestNextResource();
                _delayTimer = _delay + Time.time;
            }
            
        }

        public void Exit()
        {
        }
    }
}
