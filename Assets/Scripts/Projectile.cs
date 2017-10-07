using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField, Range(0,10)]
	float m_speed;

	Rigidbody m_rigidbody;
	Transform m_parent;

	// Use this for initialization
	void Start()
	{
		m_rigidbody = GetComponent<Rigidbody>();
	}

	public void SetParentOnCollision(Transform parent)
	{
		m_parent = parent;
	}

	void Update()
	{
		if (!m_rigidbody.useGravity)
		{
			transform.localPosition = new Vector3(0, transform.localPosition.y - (m_speed * Time.deltaTime), 0);
			transform.localRotation = Quaternion.identity;
		}
	}

	public void OnCollisionEnter(Collision collision)
	{
		m_rigidbody.useGravity = true;
		transform.SetParent(m_parent);
		Destroy(this);
	}
}
