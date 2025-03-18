using System;
using System.Collections.Generic;
using ForTheVillage.Village;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ForTheVillage.Resources
{
    public class ResourceCreator
    {
        Transform _spawnLocation;
        MeshRenderer _spawnLocationRenderer;
        int _amountMax = 50;
        int _amountMin = 5;
        
        public ResourceCreator(Transform spawnLocation, MeshRenderer spawnLocationRenderer)
        {
            _spawnLocation = spawnLocation;
            _spawnLocationRenderer = spawnLocationRenderer;
        }


        Vector3 GetSpawnLocation()
        {
            Bounds bounds = _spawnLocationRenderer.bounds;
            Vector3 min = bounds.min; // Bottom-left corner
            Vector3 max = bounds.max; // Top-right corner

            //Debug.Log($"Plane Min: {min}, Max: {max}");

            
            return new Vector3(
                Random.Range(min.x, max.x),
                _spawnLocation.position.y, 
                Random.Range(min.z, max.z)
            );
        }

        public Resource GetRandomResource()
        {
            Array values = Enum.GetValues(typeof(ResourceType));
            ResourceType resourceType = (ResourceType)values.GetValue(Random.Range(0, values.Length));
            
            return GetResource(resourceType);
        }

        public Resource GetResource(ResourceType type)
        {
            var position = GetSpawnLocation();
            int amount = Random.Range(_amountMin, _amountMax);
            
            return new Resource(position, type, amount);
        }

        public Resource GetResourceAtPosition(ResourceType type, Vector3 position)
        {
            int amount = Random.Range(_amountMin, _amountMax);
            
            return new Resource(position, type, amount);
        }
    }
}
