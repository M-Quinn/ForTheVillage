using System.Collections.Generic;
using ForTheVillage.Resources;
using ForTheVillage.Village;
using UnityEngine;

namespace ForTheVillage.Management
{
    public class GameManager
    {
        #region Singleton

        private static GameManager instance = null;
        private static readonly object padlock = new object();

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new GameManager();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion

        List<VillageController> _villages = new List<VillageController>();
        List<ResourceController> _resources = new List<ResourceController>();

        public void RegisterVillage(VillageController village)
        {
            _villages.Add(village);
        }

        public void UnregisterVillage(VillageController village)
        {
            _villages.Remove(village);
        }

        public List<VillageController> GetVillages()
        {
            return _villages;
        }

        public void RegisterResource(ResourceController resource)
        {
            _resources.Add(resource);
        }

        public void UnregisterResource(ResourceController resource)
        {
            _resources.Remove(resource);
        }

        void AddResourceRegistrations()
        {
            for (int i = 0; i < _villages.Count; i++)
            {
                
            }
        }

        void RemoveResourceRegistrations()
        {
            
        }

    }
}

