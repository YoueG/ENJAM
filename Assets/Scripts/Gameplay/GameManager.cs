using UnityEngine;

[System.Serializable]
struct Wave
{
	[SerializeField]
	float delay;
	[SerializeField]
	int ennemiesNumer;
}

public class GameManager : Singleton<GameManager>
{
	[SerializeField]
	Wave[] m_waves;

	bool m_alive = true;
	float m_gameTime = 0;

	void Start ()
	{

	}
	
	void Update()
	{
		if (m_alive)
			m_gameTime += Time.deltaTime;
	}

	public void EndGame()
	{
		m_alive = false;
	}

	public float GetGameTime()
	{
		return m_gameTime;
	}
}
