using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
	void OnCollisionEnter(Collision collision)
	{
		Destroy(collision.gameObject);
	}

	public void OnTriggerEnter(Collider other)
	{
		Destroy(other.gameObject);
	}
}
