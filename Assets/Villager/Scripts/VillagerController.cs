using UnityEngine;
using UnityEngine.AI;

public class VillagerController : MonoBehaviour
{
    [SerializeField] NavMeshAgent _navMesh;
    public GameObject targetObject;

    void Start()
    {
        _navMesh.SetDestination(targetObject.transform.position);
    }
}
