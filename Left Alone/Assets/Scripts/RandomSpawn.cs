using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject objectToSpawn; 
    public float spawnRadius = 10f;
    public int numberOfObjectsToSpawn = 5; 

    private void Start()
    {
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        Vector3 randomPosition = GetRandomSpawnPosition();
        Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 randomPosition = new Vector3(randomCircle.x, 0f, randomCircle.y) + transform.position;
        return randomPosition;
    }
}