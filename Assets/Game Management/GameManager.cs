using System.Collections.Generic;
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

        private List<VillageController> _villages = new List<VillageController>();

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

    }
}

