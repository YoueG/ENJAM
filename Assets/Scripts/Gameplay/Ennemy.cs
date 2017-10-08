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

	Pattern m_pattern;
	NavMeshAgent m_agent;

	Vector3 m_target;

	void Start()
	{
		// Randomise enemies
		m_buste.transform.localScale = new Vector3(Random.Range(0, 2) == 1 ? -1 : 1, 1, 1);
		m_weaponParent.GetChild(Random.Range(0, m_weaponParent.childCount)).gameObject.SetActive(true);
	}

	public void SetPattern(Pattern pattern, Vector3 target)
	{
		m_pattern = pattern;
		m_target = target;
		m_agent = GetComponent<NavMeshAgent>();

		if (m_pattern == Pattern.Random)
			m_pattern = (Pattern)Random.Range(1, 5);

		if (m_pattern == Pattern.Tout_droit)
			m_agent.SetDestination(target);
		else
			UpdatePath();
	}

	void UpdatePath()
	{
		Vector3 newDestination = transform.position;

		switch (m_pattern)
		{
			case Pattern.Gauche:
				newDestination = Quaternion.AngleAxis(-180, Vector3.up) * (m_target - transform.position).normalized;
				break;
			case Pattern.Droite:
				newDestination = Quaternion.AngleAxis(180, Vector3.up) * (m_target - transform.position).normalized;
				break;
			case Pattern.ZigZag:
				newDestination = Quaternion.AngleAxis(Random.Range(-180,180), Vector3.up) * (m_target - transform.position).normalized*2;
				break;
		}

		m_agent.SetDestination(newDestination);

		Invoke("UpdatePath", m_pathUpdateDelay);
	}

	void Die(Vector3 vel)
	{
		m_agent.enabled = false;

		Rigidbody rgbd = GetComponent<Rigidbody>();
		rgbd.isKinematic = false;
		rgbd.velocity = vel * m_collisionForce;

		m_animator.SetBool("Dead", true);
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Projectile"))
		{
			Die(collision.contacts[0].normal);
		}
	}
}
