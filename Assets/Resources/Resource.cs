using UnityEngine;

namespace ForTheVillage.Resources
{
    public class Resource
    {
        public Vector3 SpawnPosition { get; private set; }
        public ResourceType ResourceType { get; private set; }
        public int Amount { get; private set; }

        public Resource(Vector3 spawnPosition, ResourceType resourceType, int amount)
        {
            SpawnPosition = spawnPosition;
            ResourceType = resourceType;
            Amount = amount;
        }
    }

    public enum ResourceType
    {
        WOOD,
        STONE,
        FOOD,
        
    }
}
