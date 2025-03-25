using System;
using ForTheVillage.Village;
using UnityEngine;
using UnityEngine.AI;

namespace ForTheVillage.Villager
{
    public class ReturnToVillage: IState
    {
        private Action<string> _logAction;
        private VillageController _village;
        private NavMeshAgent _navMeshAgent;
        
        public ReturnToVillage(ref VillageController village, NavMeshAgent navMeshAgent, Action<string>logAction)
        {
            _village = village;
            _navMeshAgent = navMeshAgent;
            _logAction = logAction;
        }
        
        public void Enter()
        {
            if (_village == null)
            {
                _logAction("No Village found");
            }
            else
            {
                _navMeshAgent.SetDestination(_village.transform.position);
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
