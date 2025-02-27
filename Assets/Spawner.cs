using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] platformPrefabs;
    public float platformSpawnTime = 2f;

    private float timeUntilPlatformSpawn;

    private void Update()
    {
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

        GameObject spawnedPlatform = Instantiate(platformToSpawn, transform.position, Quaternion.identity);

        Rigidbody2D platformRB = spawnedPlatform.GetComponent<Rigidbody2D>();

    }

}