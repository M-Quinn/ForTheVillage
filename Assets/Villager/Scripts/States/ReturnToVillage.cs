using System;
using UnityEngine;
using UnityEngine.AI;

namespace ForTheVillage.Villager
{
    public class ReturnToVillage: IState
    {
        private Action<string> _logAction;
        private GameObject _village;
        private NavMeshAgent _navMeshAgent;
        
        public ReturnToVillage(ref GameObject village, NavMeshAgent navMeshAgent, Action<string>logAction)
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
