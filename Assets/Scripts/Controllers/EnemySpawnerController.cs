using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [Tooltip("This the object to spawn")]
    public GameObject prefab;

    [Range(0.5f, 100.0f)]
    public float spawnDelay;

    [Range(1, 10)]
    public int minWorkForce;

    [Range(1, 10)]
    public int maxWorkForce;

    // Used to place enemies at the right place
    private Vector2 spawnRange;
    private float   elapsedTime;

    void Start ()
    {
        elapsedTime = 0.0f; // Co-routines ?
    }
	
	void Update ()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= spawnDelay)
        {
            elapsedTime = 0.0f;

            // Time to instantiates dummy humans
            // to destroy the poor alien muahaha
            SpawnEvilHumans(Random.Range(minWorkForce, maxWorkForce));
        }
    }

    private void SpawnEvilHumans(int humanCount)
    {
        // Possible optimisation : Pool allocator of humans
        for(int nHuman = 0; nHuman < humanCount; ++nHuman)
        {
            GameObject evilHuman = Instantiate(prefab, this.transform.position, this.transform.rotation); // Randomizes spawns location
            evilHuman.transform.parent = this.transform;
        }
    }

    public Vector2 GetSpawnRange()
    {
        return spawnRange;
    }

    public void SetSpawnRange(Vector2 range)
    {
        spawnRange = range;
    }
}
