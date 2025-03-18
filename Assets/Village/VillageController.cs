using System;
using System.Collections.Generic;
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
        
        double villagersPerLevel = 1.75;
        int housingCapacity = 30;
        int maxLevel = 10;
        
        List<VillagerController>  villagers = new List<VillagerController>();

        void Start()
        {
            _gridController = GameManager.Instance.GetGridController();
            delayTimer = delayTime + Time.time;
            for (int level = 1; level <= 12; level++) // Test levels beyond maxLevel
            {
                int maxVillagers = CalculateMaxVillagers(level, villagersPerLevel, housingCapacity, maxLevel);
                Debug.Log($"Level {level}: Max Villagers = {maxVillagers}");
            }
        }

        void Update()
        {
            if (Time.time > delayTimer)
            {
                
            }
        }

        /*void UpdateVillage()
        {
            if(villagers.Count > level)
        }*/


        public ResourceController RequestNextResource()
        {
            var resources = _gridController.GetResourcesInRadius(transform.position, searchRadius, ResourceType.FOOD);
            return resources[0];
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