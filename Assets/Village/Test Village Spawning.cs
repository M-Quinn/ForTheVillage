using UnityEngine;

public class TestVillageSpawning : MonoBehaviour
{
    [SerializeField] GameObject _villagePrefab;
    [SerializeField] Transform _spawnLocation;
    [SerializeField] Renderer _spawnLocationRenderer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //spawn random location
            Instantiate(_villagePrefab, GetSpawnLocation(), Quaternion.identity);
        }
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
}
