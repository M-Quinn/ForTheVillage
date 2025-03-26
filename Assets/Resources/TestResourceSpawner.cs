using ForTheVillage.Resources;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ForTheVillage.Resources.Test
{
    public class TestResourceSpawner : ResourceSpawner
    {

        // Update is called once per frame
        void Update()
        {
            if(Keyboard.current.numpad1Key.wasPressedThisFrame)
            {
                SpawnRandomResource();
            }
            
            if(Keyboard.current.numpad2Key.wasPressedThisFrame)
            {
                SpawnResource(ResourceType.FOOD);
            }
            
            if(Keyboard.current.numpad3Key.wasPressedThisFrame)
            {
                SpawnResourceAtPosition(ResourceType.WOOD, new Vector3(1,0,3));
            }

            if (Keyboard.current.numpad4Key.wasPressedThisFrame)
            {
                for (int i = 0; i < 50; i++)
                {
                    SpawnResource(ResourceType.FOOD);
                }
            }
        }
    }
}
