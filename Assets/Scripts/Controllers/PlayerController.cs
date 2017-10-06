using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Controls")]
	[SerializeField]
	KeyCode m_left;
	[SerializeField]
	KeyCode m_right;
	[SerializeField]
	KeyCode m_fire;

	[SerializeField]
	Transform m_ship;
	float m_minPosZ;
	[SerializeField, Range(0, 10)]
	float m_range;

	[Header("Shot")]
	[SerializeField]
	GameObject m_projectile;
	[SerializeField]
	Transform m_spawnPoint, m_parent;

	[SerializeField, Range(0,10)]
	float m_speed;

	Rigidbody m_rigidbody;
	
	void Start ()
	{
		m_minPosZ = m_ship.transform.position.z;
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
	}

	void FixedUpdate()
	{
		if (Input.GetKeyDown(m_fire))
		{
		}
		else if (Input.GetKey(m_fire))
		{
			m_ship.position = new Vector3(m_ship.position.x,
											m_ship.position.y,
											Mathf.MoveTowards(m_ship.position.z, m_minPosZ + (-m_range), Time.fixedDeltaTime));
		}
		else if (Input.GetKeyUp(m_fire))
		{
			Instantiate(m_projectile, m_spawnPoint.position, transform.rotation, m_parent);
		}
		else
		{
			m_ship.position = new Vector3(m_ship.position.x,
											m_ship.position.y,
											Mathf.MoveTowards(m_ship.position.z, m_minPosZ, Time.fixedDeltaTime));
		}
	}
}
