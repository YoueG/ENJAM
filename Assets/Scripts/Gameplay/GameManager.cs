using UnityEngine;

[System.Serializable]
struct Wave
{
	[SerializeField]
	public float delay;
	[SerializeField]
	public int ennemiesNumer;
	[SerializeField]
	public int division;
}

public class GameManager : Singleton<GameManager>
{
	[SerializeField]
	EnemySpawner m_enemySpawnerMainController;

	[SerializeField]
	Wave[] m_waves;

	bool m_alive = true;
	float m_gameTime = 0;

	void Start ()
	{
		StartWave(m_waves[0]);
	}
	
	void Update()
	{
		if (m_alive)
			m_gameTime += Time.deltaTime;
	}

	int m_currentWave = 0;
	void StartNextWave()
	{
		if (++m_currentWave >= m_waves.Length)
		{
			EndGame(true);
			return;
		}

		StartWave(m_waves[m_currentWave]);
	}

	void StartWave(Wave wave)
	{
		m_enemySpawnerMainController.Spawn(wave.division, wave.ennemiesNumer);
		Invoke("StartNextWave", wave.delay);
	}

	public void EndGame(bool victory)
	{
		if(victory)
		{

		}
		else
			m_alive = false;
	}

	public float GetGameTime()
	{
		return m_gameTime;
	}
}
