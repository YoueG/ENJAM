using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	Rigidbody m_player;
	[SerializeField]
	Animation m_animAttack;

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

	[SerializeField, Range(0, 100)]
	float m_xSpeed;
	[SerializeField, Range(0, 30)]
	float m_zSpeed;

	[Header("Shot"), Range(0,10)]
	[SerializeField]
	float m_reloadTime;
	float m_nextShotTime;
	[SerializeField]
	GameObject m_projectile;
	[SerializeField]
	Transform m_spawnPoint, m_parent;

	void Start ()
	{
		m_minPosZ = m_ship.transform.position.z;

		m_nextShotTime = 0;
	}
	
	void Update ()
	{
        if (Input.GetKey(m_left))
		{
			if (m_player.angularVelocity.y < 0)
				m_player.angularVelocity = new Vector3(0, 0, 0);
			else
				m_player.angularVelocity = new Vector3(0, m_xSpeed * Time.deltaTime, 0);
        }
		else if(Input.GetKey(m_right))
		{
			if (m_player.angularVelocity.y > 0)
				m_player.angularVelocity = new Vector3(0, 0, 0);
			else
				m_player.angularVelocity = new Vector3(0, -m_xSpeed * Time.deltaTime, 0);
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

		if (Input.GetKeyDown(m_fire) && m_nextShotTime <= 0)
		{
			m_animAttack.Play();
			AkSoundEngine.PostEvent("drop_cow", gameObject);
			m_nextShotTime = m_reloadTime;
			GameObject newProjectile = Instantiate(m_projectile, m_spawnPoint.position, transform.rotation, m_spawnPoint);
			newProjectile.GetComponent<Projectile>().SetParentOnCollision(m_parent);
		}
		else
		{
			m_nextShotTime -= Time.deltaTime;
		}
	}
}
