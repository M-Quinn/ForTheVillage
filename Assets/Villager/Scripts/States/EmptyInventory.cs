using System;
using ForTheVillage.Village;

namespace ForTheVillage.Villager
{
    public class EmptyInventory : IState
    {
        private VillagerInventory _inventory;
        private VillageController _villageController;
        private Action<string> _logAction;
        
        public EmptyInventory(ref VillageController villageController, VillagerInventory inventory, Action<string>logAction)
        {
            _villageController = villageController;
            _inventory = inventory;
            _logAction = logAction;
        }
        public void Enter()
        {
            if (_villageController == null)
            {
                _logAction?.Invoke("village Controller is null");
            }
            else
            {
                if (_inventory.Resource == null)
                {
                    _logAction?.Invoke("inventory resource is null");
                }
                else
                {
                    _villageController.AcceptResource(_inventory.Resource);
                    _inventory.Resource = null;
                }
            }
        }

        public void Tick()
        {
            
        }

        public void Exit()
        {
            
        }
    }

}