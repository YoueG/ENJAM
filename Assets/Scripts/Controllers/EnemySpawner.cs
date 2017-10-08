using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField]
	GameObject ennemyPrefab;
	[SerializeField]
	Transform m_parent;
	[SerializeField]
	Transform m_friend;

	[SerializeField]
	int m_divisionsNumber;
	[SerializeField, Range(0,50)]
	float m_radius;

	Vector3 getRandomDivisionPos(int division)
	{
		float range = 1f / m_divisionsNumber;
		float q = (division + Random.Range(-range, range)) * 2 * Mathf.PI/ m_divisionsNumber;
		
		// Getting the coordinate of a point on the circle
		float x = transform.position.x + m_radius * Mathf.Cos(q);
		float z = transform.position.z + m_radius * Mathf.Sin(q);

		return new Vector3(x, transform.position.y, z);
	}

	void OnDrawGizmos()
	{
		Handles.color = Color.green;
		Handles.DrawWireDisc(transform.position, Vector3.up, m_radius);
	}
	
	public void Spawn(int division, int count, Pattern pattern, float speed)
    {
		GameObject ennemy;
		Vector3 randomPos = getRandomDivisionPos(division);

		for (int n = 0; n < count; ++n)
		{
			ennemy = Instantiate(ennemyPrefab, randomPos + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)), Quaternion.identity); // Randomizes spawns location
			ennemy.transform.parent = m_parent;

			ennemy.GetComponent<Ennemy>().SetPattern(pattern, m_friend.position, speed);
			//ennemy.GetComponent<NavMeshAgent>().SetDestination(m_friend.position);
		}
	}
}
