using System.Collections;
using UnityEngine;

public class ballSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // Reference to the ball prefab
    public Camera mainCamera; // Reference to the main camera
    public float fixedYPosition = 4.78f; // The constant y-position you want the ball to spawn at
    public float spawnDelay = 1f;

    private void OnBecameInvisible()
    {
        // Destroy the ball when it goes off-screen
        Destroy(gameObject);

        // Spawn a new ball with a random x-position but a constant y-position
        SpawnBall();
        StartCoroutine(SpawnBallWithDelay());
    }

    IEnumerator SpawnBallWithDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(spawnDelay);

        // Spawn a new ball after the delay
        SpawnBall();
    }

    void SpawnBall()
    {
        // Get the world position of the camera's edges (orthographic camera assumed)
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Generate a random x-position within the screen bounds
        float randomX = Random.Range(-cameraWidth / 2, cameraWidth / 2);

        // Use the constant y-position
        float yPosition = fixedYPosition;

        // Create the new spawn position with the random x and fixed y
        Vector3 spawnPosition = new Vector3(randomX, yPosition, -7.564f);

        // Instantiate the ball at the new position
        Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
    }
}
