﻿using UnityEngine;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		Handles.color = Color.green;
		Handles.DrawWireDisc(transform.position, Vector3.up, m_radius);
	}
#endif

	public void Spawn(Wave wave)
    {
		GameObject ennemy;
		Vector3 randomPos = getRandomDivisionPos(wave.division == -1 ? Random.Range(0, 4) : wave.division);

		int ennemiesCount = (int)Random.Range(wave.countLimits.x, wave.countLimits.y);

		if(ennemiesCount > 5)
			AkSoundEngine.PostEvent("enemies_group_pop", gameObject);

		Pattern test = Pattern.Random;

		for (int n = 0; n < ennemiesCount; ++n)
		{
			ennemy = Instantiate(ennemyPrefab, randomPos + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)), Quaternion.identity); // Randomizes spawns location
			ennemy.transform.parent = m_parent;

			ennemy.GetComponent<Ennemy>().SetPattern(test, m_friend.position, wave.speed);
		}
	}
}
