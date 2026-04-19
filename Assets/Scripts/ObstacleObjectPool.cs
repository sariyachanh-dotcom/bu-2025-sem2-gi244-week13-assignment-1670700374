using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    [SerializeField]  private GameObject obstacleBarrelPrefab;
    [SerializeField]  private GameObject obstacleBarrierPrefab;
    [SerializeField]  private GameObject obstacleStoneWallPrefab;
    public int poolSize = 10;

    private readonly List<GameObject> BarrelPool = new();
    private readonly List<GameObject> BarrierPool = new();
    private readonly List<GameObject> StoneWallPool = new();

    void Awake()
    {
        for (int i = 0; i < poolSize; i++) 
        {
            CreateNew(obstacleBarrelPrefab, BarrelPool);
            CreateNew(obstacleBarrierPrefab, BarrierPool);
            CreateNew(obstacleStoneWallPrefab, StoneWallPool);
        }
    }
    private void CreateNew(GameObject prefab, List<GameObject> pool)
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        pool.Add(obj);
    }



    public GameObject Acquire(int obstacleType)
    {
        

        List<GameObject> pool = null;
        GameObject prefab = null;

        if (obstacleType == 0)
        {
            pool = BarrelPool;
            prefab = obstacleBarrelPrefab;
        }
        else if (obstacleType == 1)
        {
            pool = BarrierPool;
            prefab = obstacleBarrierPrefab;
        }
        else if (obstacleType == 2)
        {
            pool = StoneWallPool;
            prefab = obstacleStoneWallPrefab;
        }

        if (pool.Count == 0)
        {
            CreateNew (prefab, pool);
        }

        GameObject obj = pool[0];
        pool.RemoveAt(0);
        obj.SetActive(true);
        return obj;
        
    }

    public void Release(GameObject obstacle, int obstacleType)
    {
        obstacle.SetActive(false);

        if (obstacleType == 0)
            BarrelPool.Add(obstacle);
        else if (obstacleType == 1)
            BarrierPool.Add(obstacle);
        else if (obstacleType == 2)
            StoneWallPool.Add(obstacle);
        else
            Debug.LogError("Invalid obstacleType");
    }
}
