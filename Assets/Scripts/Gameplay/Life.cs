using UnityEngine;

public class Life : MonoBehaviour
{
	[SerializeField, Range(0,1)]
	float m_life = 1, m_ennemiesDamages = 0.1f;

	[SerializeField]
	GameObject[] m_donuts;

	void Start ()
	{
	}
	
	void Update ()
	{
	}

	void Repare()
	{
		AkSoundEngine.PostEvent("ufo_reparation", gameObject);
	}

	public void TakeDamages()
	{
		if(m_life > 0)
		{
			m_life -= m_ennemiesDamages;

			if (m_life <= 0)
				GameManager.Instance.EndGame(false);
			else
			{
				EnableShipPart((int)((1 - m_life) * (m_donuts.Length + 1)));
			}
		}
	}

	void EnableShipPart(int ID)
	{
		foreach (var g in m_donuts)
			g.SetActive(false);

		m_donuts[ID].SetActive(true);
	}
}
