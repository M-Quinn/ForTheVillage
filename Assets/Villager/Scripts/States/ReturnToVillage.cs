using System;
using ForTheVillage.Village;
using UnityEngine;
using UnityEngine.AI;

namespace ForTheVillage.Villager
{
    public class ReturnToVillage: IState
    {
        private Action<bool> _destinationReachedAction;
        private Action<string> _logAction;
        private VillageController _village;
        private NavMeshAgent _navMeshAgent;
        
        public ReturnToVillage(VillageController village, NavMeshAgent navMeshAgent, Action<bool> destinationReachedAction,  Action<string>logAction)
        {
            _destinationReachedAction = destinationReachedAction;
            _village = village;
            _navMeshAgent = navMeshAgent;
            _logAction = logAction;
        }
        
        public void Enter()
        {
            _logAction?.Invoke("-> Return To Village State");
            if (_village == null)
            {
                _logAction("No Village found");
            }
            else
            {
                _navMeshAgent.SetDestination(_village.transform.position);
                _logAction?.Invoke("Moving towards village");
            }
        }

        public void Tick()
        {
            if (_navMeshAgent.remainingDistance <= 0)
            {
                _destinationReachedAction?.Invoke(true);
            }

            
        }

        public void Exit()
        {
            _navMeshAgent.ResetPath();
        }
    }
}
