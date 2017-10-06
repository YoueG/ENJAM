using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	KeyCode m_left, m_right, m_fire;

	[SerializeField]
	GameObject m_projectile;
	[SerializeField]
	Transform m_spawnPoint, m_parent;

	[SerializeField, Range(0,10)]
	float m_speed;

	Rigidbody m_rigidbody;
	
	void Start ()
	{
		m_rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update ()
	{
		if(Input.GetKey(m_left))
		{
			if (m_rigidbody.angularVelocity.y < 0)
				m_rigidbody.angularVelocity = new Vector3(0, 0, 0);
			else
				m_rigidbody.angularVelocity = new Vector3(0, m_speed, 0);
		}
		else if(Input.GetKey(m_right))
		{
			if (m_rigidbody.angularVelocity.y > 0)
				m_rigidbody.angularVelocity = new Vector3(0, 0, 0);
			else
				m_rigidbody.angularVelocity = new Vector3(0, -m_speed, 0);
		}

		if(Input.GetKeyDown(m_fire))
		{
			Instantiate(m_projectile, m_spawnPoint.position, transform.rotation, m_parent);
			//Instantiate(original,position,rotation,parent)
		}
	}
}
