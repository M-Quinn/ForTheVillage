using UnityEngine;
using UnityEngine.InputSystem;

namespace ForTheVillage.Village.Test
{
    public class TestVillageRequests : MonoBehaviour
    {
        public VillageController village;

        void Update()
        {
            if (Keyboard.current.numpad9Key.wasPressedThisFrame)
            {
                var resource = village.RequestNextResource();
                Debug.Log(resource);
            }
        }
    }
}
