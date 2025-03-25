using System.Collections.Generic;
using ForTheVillage.Resources;
using UnityEngine;

namespace ForTheVillage.Village
{
    public class Village
    {
        public List<Resource> Resources = new List<Resource>();
        public int Level;
        public Vector3 Position;
        public int TotalVillagers;

        public Village(List<Resource> resources, int level, Vector3 position, int totalVillagers)
        {
            Resources = resources;
            Level = level;
            Position = position;
            TotalVillagers = totalVillagers;
        }
    }
}