using System.Net.Mime;
using ForTheVillage.Management;
using ForTheVillage.Resources;
using UnityEngine;

namespace ForTheVillage.Village
{
    public class VillageController : MonoBehaviour
    {
        public int searchRadius;
        GridController _gridController;
        public GameObject go;

        void Start()
        {
            _gridController = GameManager.Instance.GetGridController();
        }


        public ResourceController RequestNextResource()
        {
            var resources = _gridController.GetResourcesInRadius(transform.position, searchRadius, ResourceType.FOOD);
            return resources[0];
        }
        
        private void OnDrawGizmosSelected() // Use OnDrawGizmosSelected for editor visualization
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, searchRadius);
        }
    }

}