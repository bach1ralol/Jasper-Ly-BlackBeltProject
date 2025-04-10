using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private GameObject[] platformPrefabs;
    public float platformSpawnTime = 2f;

    private float timeUntilPlatformSpawn;

    public Transform[] SpawnPoints;

    private void Update()
    {
        transform.position = new Vector3(( player.transform.position.x + 20), transform.position.y, transform.position.z);
        SpawnLoop();
    }

    private void SpawnLoop()
    {
        timeUntilPlatformSpawn += Time.deltaTime;

        if (timeUntilPlatformSpawn >= platformSpawnTime)
        {
            Spawn();
            timeUntilPlatformSpawn = 0f;
        }
    }

    private void Spawn()
    {
        GameObject platformToSpawn = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
        int randInt = Random.Range(0, 3);
        Vector3 SpawnLocation = SpawnPoints[randInt].position;
        GameObject spawnedPlatform = Instantiate(platformToSpawn,SpawnLocation, Quaternion.identity);
            
        Rigidbody2D platformRB = spawnedPlatform.GetComponent<Rigidbody2D>();

        platformRB.linearVelocity = new Vector2(-10, 1);

        Destroy(spawnedPlatform, 10);
    }
}