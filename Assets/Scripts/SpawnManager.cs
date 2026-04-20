using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject obstaclePrefab; 

    public ObstacleObjectPool pool;

   
    private Dictionary<GameObject, int> activeObjects = new();

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 0, 2f);
    }

    void Update()
    {
        List<GameObject> toRemove = new();

        foreach (var pair in activeObjects)
        {
            GameObject obj = pair.Key;

           
            if (obj == null)
            {
                toRemove.Add(obj);
                continue;
            }

            
            if (obj.transform.position.z < -10f)
            {
                pool.Release(obj, pair.Value); 
                toRemove.Add(obj);
            }
        }

        foreach (var obj in toRemove)
        {
            activeObjects.Remove(obj);
        }
    }

    void Spawn()
    {
        GameObject player = GameObject.Find("Player");
        bool isGameOver = player.GetComponent<PlayerController>().gameOver;
        if (isGameOver) return;

        if (pool != null)
        {
           
            int type = Random.Range(0, 3);

            GameObject obj = pool.Acquire(type);

            obj.transform.position = spawnPoint.position;
            obj.transform.rotation = Quaternion.identity;

           
            activeObjects[obj] = type;
        }
        else
        {
            
            Instantiate(
                obstaclePrefab,
                spawnPoint.position,
                obstaclePrefab.transform.rotation
            );
        }
    }
}