using System;
using ForTheVillage.Resources;
using UnityEngine;

namespace ForTheVillage.Villager
{
    public class GetResource : IState
    {
        private ResourceController _resourceController;
        private Func<ResourceController> _getResource;
        private VillagerInventory _inventory;
        private Action<string> _logAction;
        private float _delay = 1.0f;
        private float _delayTimer = 0.0f;
        
        public GetResource(Func<ResourceController> getResource, VillagerInventory inventory, Action<string>logAction)
        {
            _getResource = getResource;
            _inventory = inventory;
            _logAction = logAction;
        }
        public void Enter()
        {
            _logAction?.Invoke("-> Get Resource State");
            _resourceController = _getResource?.Invoke();
            
            if (_resourceController == null)
            {
                _logAction("ResourceController is null");
            }
            else
            {
                _inventory.Resource = _resourceController.Resource;
                _inventory.Resource.Amount = 0;
                _delayTimer = Time.time + _delay;
                _logAction("Resource Reached, Harvesting now");
            }
        }

        public void Tick()
        {
            if (_resourceController == null)
                return;
            if (Time.time >= _delayTimer)
            {
                _inventory.Resource.Amount = _resourceController.HarvestResource(5);
                _delayTimer = Time.time + _delay;
            }
        }

        public void Exit()
        {
            _resourceController = null;
            _logAction?.Invoke("<- Get Resource State");
        }
    }
}
