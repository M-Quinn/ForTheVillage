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
        public WaitForTask(ref ResourceController target, ref VillageController village, Action<string>logAction)
        {
            _resourceController = target;
            _villageController = village;
            _logAction = logAction;
        }

        public void Enter()
        {
            _resourceController = _villageController.RequestNextResource();
        }

        public void Tick()
        {
        }

        public void Exit()
        {
        }
    }
}
