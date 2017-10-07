using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerMainController : MonoBehaviour
{
    [Tooltip("The main sphere")]
    public GameObject world;

    [Tooltip("Represents all quartes around the mini-world")]
    public List<GameObject> quarters;

    // Initializes the main controller spawner from inspector values
	void Start ()
    {
        // Precomputing values
        float circleX = world.transform.position.x;
        float circleZ = world.transform.position.z;
        float circleY = world.transform.position.y;
        float circleR = world.transform.lossyScale.x / 2;

        int   quarterCount  = quarters.Count;
        float quarterOffset = 360.0f / quarterCount;
        for(int nQuarter = 0; nQuarter < quarterCount; ++nQuarter)
        {
            // Buffers ref.
            EnemySpawnerController spawner = quarters[nQuarter].GetComponent<EnemySpawnerController>();
            Debug.Assert(spawner != null);

            // Getting the coordinate of a point on the circle
            float x = circleX + circleR * Mathf.Cos(nQuarter * quarterOffset);
            float z = circleZ + circleR * Mathf.Sin(nQuarter * quarterOffset);

            // Debug.Log("X = " + x); // OK
            // Debug.Log("Z = " + z); // OK

            // Places all spawners into their respectives quarters
            spawner.transform.position = new Vector3(x, circleY, z);
            spawner.SetSpawnRange(new Vector2(quarterOffset / 2.0f, quarterOffset / 2.0f));
        }

    }
	
    // Updates the object
	void Update ()
    {
		// None
	}
}
