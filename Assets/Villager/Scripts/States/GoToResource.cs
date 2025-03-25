using System;
using ForTheVillage.Resources;
using UnityEngine;
using UnityEngine.AI;

namespace ForTheVillage.Villager
{
    public class GoToResource:IState
    {
        private NavMeshAgent _navMeshAgent;
        private ResourceController _targetResource;
        
        private Action<string> _logAction;
        public GoToResource(NavMeshAgent navMeshAgent, ref ResourceController resource, Action<string> logAction)
        {
            _navMeshAgent = navMeshAgent;
            _targetResource = resource;
            _logAction = logAction;
        }

        public void Enter()
        {
            if (_targetResource != null)
            {
                _navMeshAgent.SetDestination(_targetResource.Resource.SpawnPosition);
            }
            else
            {
                _logAction("No resource found");
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
