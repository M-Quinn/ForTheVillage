using System;
using UnityEngine;
using UnityEngine.AI;

namespace ForTheVillage.Villager
{
    public class VillagerController : MonoBehaviour
    {
        [SerializeField] NavMeshAgent _navMesh;
        public GameObject targetObject;


        StateFacade _stateFacade = new StateFacade();
        IState _waitForTaskState;
        IState _goToResourceState;
        IState _getResourceState;
        IState _returnToVillageState;
        IState _emptyInventoryState;

        void Awake()
        {
            _waitForTaskState = new WaitForTask(Log);
            _goToResourceState = new GoToResource(Log);
        }


        void Start()
        {
            _stateFacade.ChangeState(_waitForTaskState);

            _navMesh.SetDestination(targetObject.transform.position);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                _stateFacade.ChangeState(_waitForTaskState);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _stateFacade.ChangeState(_goToResourceState);
            }

            _stateFacade.Execute();
        }

        void Log(string msg) => Debug.Log(msg);
    }
}
