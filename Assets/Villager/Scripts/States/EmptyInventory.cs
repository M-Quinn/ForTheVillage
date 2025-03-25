using System;
using ForTheVillage.Village;

namespace ForTheVillage.Villager
{
    public class EmptyInventory : IState
    {
        private VillagerInventory _inventory;
        private VillageController _villageController;
        private Action<string> _logAction;
        
        public EmptyInventory(VillagerInventory inventory, Action<string>logAction)
        {
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
                //pass the inventory to the VillageController
            }
        }

        public void Tick()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }

}