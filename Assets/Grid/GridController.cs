using System.Collections.Concurrent;
using System.Collections.Generic;
using ForTheVillage.Management;
using ForTheVillage.Resources;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private Transform _spawnGround;
    
    public Vector3 gridOffset;
    
    public float cellSize = 10f;

    private Dictionary<Vector2Int, ConcurrentBag<ResourceController>> grid = new Dictionary<Vector2Int, ConcurrentBag<ResourceController>>();
    private Dictionary<Vector2Int, object> cellLocks = new Dictionary<Vector2Int, object>();

    private int gridWidth;
    private int gridHeight;

    private void Awake()
    {
        GameManager.Instance.RegisterGridController(this);
        InitializeGrid();
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
            InitializeGrid();
    }

    public void AddResource(ResourceController resource)
    {
        Vector2Int gridPos = GetGridPosition(resource.Resource.SpawnPosition);
        object cellLock;
        lock (cellLocks)
        {
            if (!cellLocks.ContainsKey(gridPos))
            {
                cellLocks[gridPos] = new object();
            }
            cellLock = cellLocks[gridPos];
        }

        lock (cellLock)
        {
            if (!grid.ContainsKey(gridPos))
            {
                grid[gridPos] = new ConcurrentBag<ResourceController>();
            }
            grid[gridPos].Add(resource);
        }
    }

    public void RemoveResource(ResourceController resource)
    {
        Vector2Int gridPos = GetGridPosition(resource.Resource.SpawnPosition);
        object cellLock = cellLocks[gridPos];

        lock (cellLock)
        {
            if (grid.ContainsKey(gridPos))
            {
                grid[gridPos].TryTake(out ResourceController removedResource);
            }
        }
    }

    public List<ResourceController> GetResourcesInRadius(Vector3 villagePosition, int radius, ResourceType type)
    {
        Vector2Int center = GetGridPosition(villagePosition);
        List<ResourceController> foundResources = new List<ResourceController>();

        for (int x = center.x - radius; x <= center.x + radius; x++)
        {
            for (int z = center.y - radius; z <= center.y + radius; z++)
            {
                Vector2Int cell = new Vector2Int(x, z);
                if (grid.ContainsKey(cell))
                {
                    object cellLock = cellLocks[cell];
                    lock (cellLock)
                    {
                        foreach (ResourceController resource in grid[cell])
                        {
                            if (resource.Resource.ResourceType == type)
                            {
                                foundResources.Add(resource);
                            }
                        }
                    }
                }
            }
        }
        return foundResources;
    }
    private void InitializeGrid()
    {
        if (_spawnGround == null)
        {
            Debug.LogError("Plane Transform not assigned to ResourceGrid.");
            return;
        }

        MeshRenderer planeRenderer = _spawnGround.GetComponent<MeshRenderer>();
        if (planeRenderer == null)
        {
            Debug.LogError("Plane Transform does not have a MeshRenderer.");
            return;
        }

        Bounds planeBounds = planeRenderer.bounds;
        float gridSizeX = planeBounds.size.x;
        float gridSizeZ = planeBounds.size.z;

        gridWidth = Mathf.CeilToInt(gridSizeX / cellSize);
        gridHeight = Mathf.CeilToInt(gridSizeZ / cellSize);

        // Calculate grid offset
        gridOffset = _spawnGround.position - new Vector3(gridSizeX / 2f, 0, gridSizeZ / 2f);
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        Vector3 localPosition = worldPosition - gridOffset;
        int x = Mathf.FloorToInt(localPosition.x / cellSize);
        int z = Mathf.FloorToInt(localPosition.z / cellSize);
        return new Vector2Int(x, z);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || _spawnGround == null) return;

        Gizmos.color = Color.gray;
        MeshRenderer planeRenderer = _spawnGround.GetComponent<MeshRenderer>();
        Bounds planeBounds = planeRenderer.bounds;
        float gridSizeX = planeBounds.size.x;
        float gridSizeZ = planeBounds.size.z;

        gridWidth = Mathf.CeilToInt(gridSizeX / cellSize);
        gridHeight = Mathf.CeilToInt(gridSizeZ / cellSize);

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                Vector2Int cell = new Vector2Int(x, z);
                Vector3 cellCenter = gridOffset + new Vector3(x * cellSize + cellSize / 2f, 0, z * cellSize + cellSize / 2f);

                if (grid.ContainsKey(cell) && grid[cell].Count > 2)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawCube(cellCenter, new Vector3(cellSize, 0.1f, cellSize));
                }
                else if (grid.ContainsKey(cell) && grid[cell].Count > 1)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawCube(cellCenter, new Vector3(cellSize, 0.1f, cellSize));
                }
                else if (grid.ContainsKey(cell) && grid[cell].Count > 0)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawCube(cellCenter, new Vector3(cellSize, 0.1f, cellSize));
                }
                else
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawWireCube(cellCenter, new Vector3(cellSize, 0.1f, cellSize));
                }

                
            }
        }
    }
}
