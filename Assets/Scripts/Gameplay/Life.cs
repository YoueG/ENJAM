using UnityEngine;

public class Life : MonoBehaviour
{
	[SerializeField, Range(0,1)]
	float m_life = 1;

	[SerializeField]
	MeshRenderer m_jaugeRenderer;
	Material m_jaugeMat;

	void Start () {
		m_jaugeMat = m_jaugeRenderer.material;
	}
	
	void Update ()
	{
		m_jaugeMat.SetFloat("_Ratio", m_life);
	}

	public void TakeDamages(float damages)
	{
		m_life -= damages;

		if (m_life <= 0)
			GameManager.Instance.EndGame(false);
	}
}
