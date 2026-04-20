using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private GameObject barrierPrefab;
    [SerializeField] private GameObject stoneWallPrefab;

    [SerializeField] private int initialPoolSize = 10;

    private readonly List<GameObject> barrelPool = new();
    private readonly List<GameObject> barrierPool = new();
    private readonly List<GameObject> stoneWallPool = new();

    void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNew(barrelPrefab, barrelPool);
            CreateNew(barrierPrefab, barrierPool);
            CreateNew(stoneWallPrefab, stoneWallPool);
        }
    }

    void CreateNew(GameObject prefab, List<GameObject> pool)
    {
        var go = Instantiate(prefab);
        go.SetActive(false);
        pool.Add(go);
    }

    public GameObject Acquire(int type)
    {
        List<GameObject> pool = null;
        GameObject prefab = null;

        if (type == 0)
        {
            pool = barrelPool;
            prefab = barrelPrefab;
        }
        else if (type == 1)
        {
            pool = barrierPool;
            prefab = barrierPrefab;
        }
        else if (type == 2)
        {
            pool = stoneWallPool;
            prefab = stoneWallPrefab;
        }

        if (pool.Count == 0)
        {
            CreateNew(prefab, pool);
        }

        var go = pool[0];
        pool.RemoveAt(0);
        go.SetActive(true);
        return go;
    }

    public void Release(GameObject obj, int type)
    {
        obj.SetActive(false);

        if (type == 0) barrelPool.Add(obj);
        else if (type == 1) barrierPool.Add(obj);
        else if (type == 2) stoneWallPool.Add(obj);
    }
}