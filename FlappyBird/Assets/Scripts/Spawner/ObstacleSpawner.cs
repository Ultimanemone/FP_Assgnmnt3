using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    /* 3. Main functions */
    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float minY = -3f;
    [SerializeField] private float maxY = 3f;

    private float timer;

    private void Update()
    {
        HandleSpawning();
    }

    private void HandleSpawning()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    private void SpawnObstacle()
    {
        GameObject obstacle = ObjPool.Instance.Get();

        float randomY = Random.Range(minY, maxY);
        obstacle.transform.position = new Vector3(transform.position.x, randomY, 0);
    }
}