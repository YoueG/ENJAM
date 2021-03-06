﻿using System.Collections;
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
		else if(m_rigidbody.velocity.sqrMagnitude < .01f)
		{
			GetComponent<Collider>().isTrigger = true;
		}
	}

	bool m_triggered = false;
	public void OnCollisionEnter(Collision collision)
	{
		if(!m_triggered)
		{
			AkSoundEngine.PostEvent("cow_ground", gameObject);
			m_rigidbody.useGravity = true;
			m_rigidbody.velocity = new Vector3(transform.position.normalized.x * 4, 1, transform.position.normalized.z * 4);
			m_rigidbody.angularVelocity = new Vector3(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90));
			transform.SetParent(m_parent);
			m_triggered = true;
		}

	}
}
