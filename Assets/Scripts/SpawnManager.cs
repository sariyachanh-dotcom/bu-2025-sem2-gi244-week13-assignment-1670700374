using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject obstaclePrefab;

    [SerializeField] private ObstacleObjectPool pool;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 0, 2f);
    }

    void Spawn()
    {
        GameObject player = GameObject.Find("Player");
        bool isGameOver = player.GetComponent<PlayerController>().gameOver;
        if (isGameOver)
        {
            return;
        }

      
        if (pool != null)
        {
            int type = Random.Range(0, 3);

            GameObject obj = pool.Acquire(type);
            obj.transform.position = spawnPoint.position + new Vector3(type * 3f, 0, 0);
            obj.transform.rotation = Quaternion.identity;
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