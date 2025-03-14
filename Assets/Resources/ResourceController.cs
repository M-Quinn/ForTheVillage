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
        public Resource Resource { get; private set; }

        public void  SetResource(Resource resource)
        {
            //Handle prefab changes
            switch (resource)
            {
                
            }
        }

    }
}

