using UnityEngine;

public class IncomingBlocksSpawner : MonoBehaviour
{
    public GameObject blockPrefab;       
    public Transform upperBoundary;      
    public Transform lowerBoundary;      
    public float spawnRate = 0.5f;         
    public float fixedXPosition = 33f;   

    private float timeSinceLastSpawn = 0f;
    private bool gameStarted = false;
    private bool spawningStopped = false;
    void Start()
    {

    }

    void Update()
    {
        if (gameStarted && !spawningStopped)
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= spawnRate)
            {
                SpawnBlock();
                timeSinceLastSpawn = 0f;
            }
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        Debug.Log("Game Started! Spawning blocks...");
    }

    void SpawnBlock()
    {
        float randomY = Random.Range(lowerBoundary.position.y, upperBoundary.position.y);

        Vector2 actualSpawnPosition = new Vector2(fixedXPosition, randomY);
        GameObject newBlock = Instantiate(blockPrefab, actualSpawnPosition, Quaternion.identity);

        SpriteRenderer blockRenderer = newBlock.GetComponent<SpriteRenderer>();
        Color blockColor = Random.Range(0, 2) == 0 ? Color.red : Color.blue;
        blockRenderer.color = blockColor;

        Debug.Log("Spawned a block at X: " + fixedXPosition + " and Y: " + randomY);
    }

    public void StopSpawning()
    {
        gameStarted = false;
        Debug.Log("Game Stopped! No more blocks will spawn.");
    }

}
