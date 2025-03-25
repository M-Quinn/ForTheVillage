using System;
using ForTheVillage.Resources;
using UnityEngine;
using UnityEngine.AI;

namespace ForTheVillage.Villager
{
    public class GoToResource:IState
    {
        private ResourceController _resourceController;
        private NavMeshAgent _navMeshAgent;
        private Func<ResourceController> _getTargetAction;
        
        private Action<string> _logAction;
        public GoToResource(NavMeshAgent navMeshAgent, Func<ResourceController> getTarget, Action<string> logAction)
        {
            _navMeshAgent = navMeshAgent;
            _getTargetAction = getTarget;
            _logAction = logAction;
        }

        public void Enter()
        {
            var _resourceController = _getTargetAction?.Invoke();
            if (_resourceController != null)
            {
                _navMeshAgent.SetDestination(_resourceController.Resource.SpawnPosition);
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
            _resourceController = null;
        }
    }
}
