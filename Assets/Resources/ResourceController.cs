using System;
using System.Collections.Generic;
using ForTheVillage.Management;
using ForTheVillage.Village;
using UnityEngine;

namespace ForTheVillage.Resources
{
    /// <summary>
    /// Handles individual Resource objects
    /// </summary>
    public class ResourceController : MonoBehaviour
    {
        [Header("Internal References")] 
        [SerializeField] Renderer _renderer;
        
        public Resource Resource { get; private set; }

        private GridController _gridController;

        void Awake()
        {
            GameManager.Instance.RegisterResource(this);
        }

        private void Start()
        {
            _gridController = GameManager.Instance.GetGridController();
        }

        public void  SetResource(Resource resource)
        {
            Resource = resource;
            //Handle prefab changes
            switch (Resource.ResourceType)
            {
                case ResourceType.FOOD:
                    _renderer.material.color = Color.green;
                    break;
                case ResourceType.WOOD:
                    _renderer.material.color = new Color(0.6f, 0.3f, 0.1f);
                    break;
                case ResourceType.STONE:
                    _renderer.material.color = Color.gray;
                    break;
            }
        }

        public int HarvestResource(int harvestAmount)
        {
            if (harvestAmount >= Resource.Amount)
            {
                GameManager.Instance.UnregisterResource(this);
                Die();
                return Resource.Amount;
            }
            else
            {
                Resource.Amount -= harvestAmount;
                return harvestAmount;
            }
        }

        [ContextMenu("Die")]
        void Die()
        {
            Destroy(gameObject, 0.1f);
        }

        //Added this way so that the project can easily be tested by just deleting go from the hierarchy
        void RemoveSelfFromGrid()
        {
            _gridController.RemoveResource(this);
        }

        private void OnDestroy()
        {
            RemoveSelfFromGrid();
        }
    }
}

