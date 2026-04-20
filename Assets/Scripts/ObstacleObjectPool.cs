using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    public GameObject obstacleBarrelPrefab;
    public GameObject obstacleBarrierPrefab;
    public GameObject obstacleStoneWallPrefab;
    public int poolSize = 10;

    private List<GameObject> obstacleBarrelPool;
    private List<GameObject> obstacleBarrierPool;
    private List<GameObject> obstacleStoneWallPool;

    void Awake()
    {
        obstacleBarrelPool = new List<GameObject>();
        obstacleBarrierPool = new List<GameObject>();
        obstacleStoneWallPool = new List<GameObject>();

      
        for (int i = 0; i < poolSize; i++)
        {
            CreateNew(obstacleBarrelPrefab, obstacleBarrelPool);
            CreateNew(obstacleBarrierPrefab, obstacleBarrierPool);
            CreateNew(obstacleStoneWallPrefab, obstacleStoneWallPool);
        }
    }

    
    void CreateNew(GameObject prefab, List<GameObject> pool)
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
            pool = obstacleBarrelPool;
            prefab = obstacleBarrelPrefab;
        }
        else if (obstacleType == 1)
        {
            pool = obstacleBarrierPool;
            prefab = obstacleBarrierPrefab;
        }
        else if (obstacleType == 2)
        {
            pool = obstacleStoneWallPool;
            prefab = obstacleStoneWallPrefab;
        }

        
        if (pool.Count == 0)
        {
            CreateNew(prefab, pool);
        }

        if (pool == null)
        {
            Debug.LogError("Invalid obstacleType");
            return null;
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
            obstacleBarrelPool.Add(obstacle);
        else if (obstacleType == 1)
            obstacleBarrierPool.Add(obstacle);
        else if (obstacleType == 2)
            obstacleStoneWallPool.Add(obstacle);
    }
}