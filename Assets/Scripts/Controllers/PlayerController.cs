using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Controls")]
	[SerializeField]
	KeyCode m_left;
	[SerializeField]
	KeyCode m_right;
	[SerializeField]
	KeyCode m_up;
	[SerializeField]
	KeyCode m_down;
	[SerializeField]
	KeyCode m_fire;

	[SerializeField]
	Transform m_ship;
	float m_minPosZ;
	[SerializeField, Range(0, 10)]
	float m_range;

	[SerializeField, Range(0, 30)]
	float m_xSpeed;
	[SerializeField, Range(0, 30)]
	float m_zSpeed;

	[Header("Shot")]
	[SerializeField]
	GameObject m_projectile;
	[SerializeField]
	Transform m_spawnPoint, m_parent;

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
				m_rigidbody.angularVelocity = new Vector3(0, m_xSpeed * Time.deltaTime, 0);
		}
		else if(Input.GetKey(m_right))
		{
			if (m_rigidbody.angularVelocity.y > 0)
				m_rigidbody.angularVelocity = new Vector3(0, 0, 0);
			else
				m_rigidbody.angularVelocity = new Vector3(0, -m_xSpeed * Time.deltaTime, 0);
		}

		if(Input.GetKey(m_up))
		{
			m_ship.localPosition = new Vector3(m_ship.localPosition.x,
											m_ship.localPosition.y,
											Mathf.MoveTowards(m_ship.localPosition.z, m_minPosZ, m_zSpeed * Time.deltaTime));
		}
		else if (Input.GetKey(m_down))
		{
			m_ship.localPosition = new Vector3(m_ship.localPosition.x,
											m_ship.localPosition.y,
											Mathf.MoveTowards(m_ship.localPosition.z, m_minPosZ + (-m_range), m_zSpeed * Time.deltaTime));
		}

		if (Input.GetKeyDown(m_fire))
		{
			Instantiate(m_projectile, m_spawnPoint.position, transform.rotation, m_parent);
		}
	}

	void FixedUpdate()
	{
		
	}
}
