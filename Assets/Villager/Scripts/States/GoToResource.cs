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
        private Action<bool> _destinationReachedAction;
        
        private Action<string> _logAction;
        public GoToResource(NavMeshAgent navMeshAgent, Func<ResourceController> getTarget, 
            Action<bool> destinationReachedAction, Action<string> logAction)
        {
            _destinationReachedAction = destinationReachedAction;
            _navMeshAgent = navMeshAgent;
            _getTargetAction = getTarget;
            _logAction = logAction;
        }

        public void Enter()
        {
            _logAction?.Invoke("-> Go To Resource State");
            var _resourceController = _getTargetAction?.Invoke();
            if (_resourceController != null)
            {
                _navMeshAgent.SetDestination(_resourceController.Resource.SpawnPosition);
                _logAction?.Invoke("Moving towards resource");
            }
            else
            {
                _logAction("No resource found");
            }
        }

        public void Tick()
        {
            if (_navMeshAgent.remainingDistance <=0)
            {
                _logAction?.Invoke("Path Complete");
                _destinationReachedAction?.Invoke(true);
            }
        }

        public void Exit()
        {
            _resourceController = null;
            _navMeshAgent.ResetPath();
            _logAction?.Invoke("<- Go To Resource State");
        }
    }
}
