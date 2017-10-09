using UnityEngine.AI;
using UnityEngine;

[System.Serializable]
public enum Pattern
{
	Random,
	Tout_droit,
	Gauche,
	Droite,
	ZigZag
}

public class Ennemy : MonoBehaviour
{
	[SerializeField]
	Transform m_weaponParent;
	[SerializeField]
	Transform m_buste;
	[SerializeField]
	Animator m_animator;
	
	[SerializeField, Range(0,10)]
	float m_collisionForce;
	[SerializeField, Range(0, 10)]
	float m_pathUpdateDelay;
	[SerializeField, Range(0, 10)]
	float m_attackDelay = 1;

	Pattern m_pattern;
	NavMeshAgent m_agent;
	Rigidbody m_rgbd;

	Vector3 m_target;

	void Start()
	{
		// Randomise enemies
		m_buste.transform.localScale = new Vector3(Random.Range(0, 2) == 1 ? -1 : 1, 1, 1);
		m_weaponParent.GetChild(Random.Range(0, m_weaponParent.childCount)).gameObject.SetActive(true);

		m_rgbd = GetComponent<Rigidbody>();
	}

	Life m_ship;
	public void StartAttack(Life ship)
	{
		m_attackMode = true;
		m_agent.enabled = false;
		m_ship = ship;
		AkSoundEngine.PostEvent("enemies_climbing", gameObject);
		Invoke("Attack", m_attackDelay);
	}

	void Attack()
	{
		m_agent.enabled = true;
		m_ship.TakeDamages();
		
		AkSoundEngine.PostEvent("enemies_eating", gameObject);

		m_agent.SetDestination((transform.position - m_target) * 100);

		//Die(Vector3.zero);
	}

	public void SetPattern(Pattern pattern, Vector3 target, float speed)
	{
		m_pattern = pattern;
		m_target = target;
		m_agent = GetComponent<NavMeshAgent>();
		m_agent.speed = speed;

		if (m_pattern == Pattern.Random)
			m_pattern = (Pattern)Random.Range(1, 5);

		if (m_pattern == Pattern.Tout_droit)
			m_agent.SetDestination(target);
		else
			UpdatePath();
	}

	bool m_attackMode = false;
	void Update()
	{
		Vector3 toTarget = m_target - transform.position;

		if (toTarget.magnitude < 1.2 && !m_attackMode)
		{
			m_rgbd.isKinematic = false;
			m_rgbd.useGravity = true;
			m_rgbd.constraints = RigidbodyConstraints.FreezeAll;
			
			

			m_attackMode = true;
		}
	}
	
	void UpdatePath()
	{
		Vector3 toTarget = m_target - transform.position;

		if (toTarget.magnitude < 1.2)
		{
			m_rgbd.isKinematic = false;
			m_rgbd.useGravity = true;
			m_agent.SetDestination(m_target);
			AkSoundEngine.PostEvent("enemies_climbing", gameObject);
		}
		else
		{
			Vector3 offset = transform.position;

			switch (m_pattern)
			{
				case Pattern.Gauche:
					offset = (Quaternion.AngleAxis(-45, Vector3.up) * toTarget).normalized*10;
					break;
				case Pattern.Droite:
					offset = (Quaternion.AngleAxis(45, Vector3.up) * toTarget).normalized*10;
					break;
				case Pattern.ZigZag:
					offset = (Quaternion.AngleAxis(Random.Range(-45, 45), Vector3.up) * toTarget).normalized*10;
					break;
			}

			m_agent.SetDestination(transform.position + offset);

			Invoke("UpdatePath", m_pathUpdateDelay);
		}
	}

	void Die(Vector3 vel)
	{
		m_agent.enabled = false;
		
		m_rgbd.velocity = vel * m_collisionForce;
		m_rgbd.useGravity = true;
		m_rgbd.isKinematic = false;

		m_animator.SetBool("Dead", true);
		m_rgbd.angularVelocity = new Vector3(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90));

		CancelInvoke();
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Projectile"))
		{
			AkSoundEngine.PostEvent("cow_strike_enemies", gameObject);
			Die(collision.contacts[0].normal);
		}
	}
}
