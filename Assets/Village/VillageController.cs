using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using ForTheVillage.Management;
using ForTheVillage.Resources;
using ForTheVillage.Villager;
using UnityEngine;

namespace ForTheVillage.Village
{
    public class VillageController : MonoBehaviour
    {
        [SerializeField] GameObject VillagerPrefab;
        [SerializeField] int searchRadius;
        [SerializeField] int level;
        [SerializeField] float delayTime;
        GridController _gridController;
        float delayTimer = 0;

        Village _villageData = new Village();
        
        double villagersPerLevel = 1.75;
        int housingCapacity = 30;
        int maxLevel = 10;
        
        List<VillagerController>  villagers = new List<VillagerController>();

        void Start()
        {
            _gridController = GameManager.Instance.GetGridController();
            delayTimer = delayTime + Time.time;
            /*for (int level = 1; level <= 12; level++) // Test levels beyond maxLevel
            {
                int maxVillagers = CalculateMaxVillagers(level, villagersPerLevel, housingCapacity, maxLevel);
                Debug.Log($"Level {level}: Max Villagers = {maxVillagers}");
            }*/
        }

        void Update()
        {
            if (Time.time > delayTimer)
            {
                UpdateVillage();
                delayTimer = delayTime + Time.time;
            }
        }

        void UpdateVillage()
        {
            if (villagers.Count < CalculateMaxVillagers(level, villagersPerLevel, housingCapacity, maxLevel))
            {
                GameObject villagerObject = Instantiate(VillagerPrefab, transform.position, Quaternion.identity);
                VillagerController villager = villagerObject.GetComponent<VillagerController>();
                villagers.Add(villager);
                villager.SetVillage(this);
            }
        }


        public ResourceController RequestNextResource()
        {
            //TODO: fix this so that this class determins the resource type
            var resources = _gridController.GetResourcesInRadius(transform.position, searchRadius, ResourceType.FOOD);
            if (resources.Count == 0)
                return null;
            else
            {
                //Debug.Log("Resource was found");
            }

            return resources[0];
        }

        public void LoadVillage(Village village)
        {
            _villageData = village;
            transform.position = village.Position;
            //Spawn Villagers
        }

        public void AcceptResource(Resource resource)
        {
            var rec = _villageData.Resources.FirstOrDefault(x => x.ResourceType == resource.ResourceType);
            if (rec != null)
            {
                rec.Amount += resource.Amount;
            }
            else
            {
                _villageData.Resources.Add(resource);
            }
        }





        int CalculateMaxVillagers(int level, double villagersPerLevel, int housingCapacity, int maxLevel)
        {
            return (int)MathF.Min(housingCapacity, (int)(level * villagersPerLevel));
        }
        
        private void OnDrawGizmosSelected() // Use OnDrawGizmosSelected for editor visualization
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, searchRadius);
        }
    }

}