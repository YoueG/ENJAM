using UnityEngine;
using UnityEditor;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField]
	GameObject ennemyPrefab;
	[SerializeField]
	Transform m_parent;

	[SerializeField]
	int m_divisionsNumber;
	[SerializeField, Range(0,50)]
	float m_radius;
	
    // Initializes the main controller spawner from inspector values
	void Start ()
    {
	}

	Vector3 getRandomDivisionPos(int division)
	{
		float q = division * 2 * Mathf.PI/ m_divisionsNumber;
		
		// Getting the coordinate of a point on the circle
		float x = transform.position.x + m_radius * Mathf.Cos(q);
		float z = transform.position.z + m_radius * Mathf.Sin(q);

		print(new Vector3(x, transform.position.y, z));

		return new Vector3(x, transform.position.y, z);
	}

	void OnDrawGizmos()
	{
		Handles.color = Color.green;
		Handles.DrawWireDisc(transform.position, Vector3.up, m_radius);
	}
	
	public void Spawn(int division, int count)
    {
		GameObject ennemy;
		
		for (int n = 0; n < count; ++n)
		{
			ennemy = Instantiate(ennemyPrefab, getRandomDivisionPos(division), Quaternion.identity); // Randomizes spawns location
			ennemy.transform.parent = m_parent;
		}
	}
}
