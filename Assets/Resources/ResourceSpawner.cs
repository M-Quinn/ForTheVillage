using System;
using UnityEngine;

namespace ForTheVillage.Resources
{
    public class ResourceSpawner : MonoBehaviour
    {
        [Header("Scene References")] 
        [SerializeField] Transform _spawnGroundTransform;
        [SerializeField] MeshRenderer _spawnGroundRenderer;
        [Header("External References")]
        [SerializeField] GameObject _resourcePrefab;

        ResourceCreator _resourceCreator;

        void Awake()
        {
            _resourceCreator = new ResourceCreator(_spawnGroundTransform, _spawnGroundRenderer);
            //Send reference
        }

        /// <summary>
        /// Spawns a random resource at a random location
        /// </summary>
        public void SpawnRandomResource()
        {
            Resource resource = _resourceCreator.GetRandomResource();
            GameObject go = GameObject.Instantiate(_resourcePrefab, resource.SpawnPosition, Quaternion.identity);

            SetTheResource(go, resource);
            
            //Send out or whatever for setting up the grid
        }
        
        /// <summary>
        /// Spawns a resource of your choosing at a random location
        /// </summary>
        /// <param name="resourceType"></param>
        public void SpawnResource(ResourceType resourceType)
        {
            Resource resource = _resourceCreator.GetResource(resourceType);
            GameObject go = GameObject.Instantiate(_resourcePrefab, resource.SpawnPosition, Quaternion.identity);

            SetTheResource(go, resource);
            
            //Send out or whatever for setting up the grid
        }

        /// <summary>
        /// Spawns a resource of your choosing at a location of your choosing
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="position"></param>
        public void SpawnResourceAtPosition(ResourceType resourceType, Vector3 position)
        {
            Resource resource = _resourceCreator.GetResourceAtPosition(resourceType, position);
            GameObject go = GameObject.Instantiate(_resourcePrefab, resource.SpawnPosition, Quaternion.identity);

            SetTheResource(go, resource);
            
            //Send out or whatever for setting up the grid
        }
        
        private void SetTheResource(GameObject go, Resource resource)
        {
            ResourceController rc = go.GetComponent<ResourceController>();
            rc.SetResource(resource);
        }
    }
}
