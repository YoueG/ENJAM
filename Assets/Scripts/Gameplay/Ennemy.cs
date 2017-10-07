using UnityEngine.AI;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
	[SerializeField]
	Transform m_weaponParent;
	[SerializeField]
	Transform m_buste;
	[SerializeField]
	Animator m_animator;

	void Start()
	{
		m_buste.transform.localScale = new Vector3(Random.Range(0, 2) == 1 ? -1 : 1, 1, 1);

		m_weaponParent.GetChild(Random.Range(0, m_weaponParent.childCount)).gameObject.SetActive(true);
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Projectile"))
		{
			Die();
		}
	}

	void Die()
	{
		GetComponent<NavMeshAgent>().enabled = false;
		GetComponent<Collider>().enabled = false;
		m_animator.SetBool("Dead", true);
	}
}
