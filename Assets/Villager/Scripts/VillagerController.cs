using System;
using ForTheVillage.Resources;
using ForTheVillage.Village;
using UnityEngine;
using UnityEngine.AI;

namespace ForTheVillage.Villager
{
    public class VillagerController : MonoBehaviour
    {
        [SerializeField] NavMeshAgent _navMesh;
        public ResourceController targetObject;
        public bool TargetReached = false;
        public bool VillageReached = false;

        VillagerInventory _inventory = new VillagerInventory();
        VillageController _village;

        StateFacade _stateFacade = new StateFacade();
        IState _waitForTaskState;
        IState _goToResourceState;
        IState _getResourceState;
        IState _returnToVillageState;
        IState _emptyInventoryState;

        void InitFSM()
        {
            _waitForTaskState = new WaitForTask(ref targetObject, ref _village, Log);
            _goToResourceState = new GoToResource(_navMesh, ref targetObject, Log);
            _getResourceState = new GetResource(ref targetObject, _inventory, Log);
            _returnToVillageState = new ReturnToVillage(ref _village, _navMesh, Log);
            _emptyInventoryState = new EmptyInventory(ref _village, _inventory, Log);

            _stateFacade.AddTransition(_waitForTaskState, _goToResourceState, CheckIfTargetAquired);
            _stateFacade.AddTransition(_goToResourceState, _getResourceState, CheckHasReachedTarget);
            _stateFacade.AddTransition(_getResourceState, _returnToVillageState, CheckIfResourceIsHarvested);
            _stateFacade.AddTransition(_returnToVillageState, _emptyInventoryState, CheckHasReachedVillage);
            _stateFacade.AddTransition(_emptyInventoryState, _waitForTaskState, CheckHasInventory);
        }


        public void SetVillage(VillageController villageController)
        {
            _village = villageController;
            InitFSM();
        }

        void Update()
        {
            if (_stateFacade == null)
                return;

            _stateFacade.Execute();
        }

        public void AssignTargetResource(ResourceController target)
        {
            TargetReached = false;
            targetObject = target;
        }

        public bool CheckIfTargetAquired()
        {
            return targetObject != null;
        }

        public bool CheckHasReachedTarget()
        {
            return TargetReached;
        }

        public bool CheckHasReachedVillage()
        {
            return VillageReached;
        }

        public bool CheckIfResourceIsHarvested()
        {
            if (targetObject == null)
            {
                VillageReached = false;
                return true;
            }

            return false;
        }

        public bool CheckHasInventory()
        {
            if (_inventory.Resource.Amount <= 0)
            {
                return true;
            }

            return false;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ResourceController>(out ResourceController resource) == targetObject)
            {
                TargetReached = true;
            }

            if (other.TryGetComponent<VillageController>(out VillageController village) == _village)
            {
                VillageReached = true;
            }
        }

        void Log(string msg) => Debug.Log(msg);
    }
}
