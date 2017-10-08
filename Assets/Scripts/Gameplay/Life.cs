using UnityEngine;

public class Life : MonoBehaviour
{
	[SerializeField, Range(0,1)]
	float m_life = 1;

	void Start ()
	{
	}
	
	void Update ()
	{
	}

	public void TakeDamages(float damages)
	{
		m_life -= damages;

		if (m_life <= 0)
			GameManager.Instance.EndGame(false);
	}
}
