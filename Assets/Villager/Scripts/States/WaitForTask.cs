using System;
using ForTheVillage.Resources;
using ForTheVillage.Village;
using UnityEngine;

namespace ForTheVillage.Villager
{
    public class WaitForTask : IState
    {
        private Action<ResourceController> _updateTargetAction; 
        private VillageController _villageController;
        private Action<string> _logAction;
        
        private ResourceController _resourceController;

        private float _delay = 2.0f;
        private float _delayTimer = 0.0f;
        
        public WaitForTask(Action<ResourceController> updateTarget, VillageController village, Action<string>logAction)
        {
            _updateTargetAction = updateTarget;
            _villageController = village;
            _logAction = logAction;
        }

        public void Enter()
        {
            _delayTimer = _delay + Time.time;
            _logAction("-> Enter Wait For Task");
        }

        public void Tick()
        {
            if (Time.time >= _delayTimer)
            {
                if (_resourceController == null)
                {
                    _logAction("request resource controller");
                    _resourceController = _villageController.RequestNextResource();
                    if (_resourceController == null)
                    {
                        _logAction?.Invoke("request resource controller failed");
                    }
                }//Don't change this, if is null assign, if now not null send out
                if (_resourceController != null)
                {
                    _updateTargetAction(_resourceController);
                    return;
                }
                _delayTimer = _delay + Time.time;
            }
        }

        public void Exit()
        {
            _resourceController = null;
        }
    }
}
