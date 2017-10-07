using UnityEngine;

[System.Serializable]
struct Wave
{
	[SerializeField]
	public float delay;
	[SerializeField]
	public int ennemiesNumer;
	[SerializeField]
	public int quarter;
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
		m_enemySpawnerMainController.Spawn( 1, m_waves[0].ennemiesNumer);
		m_enemySpawnerMainController.Spawn( 2, m_waves[0].ennemiesNumer);
		m_enemySpawnerMainController.Spawn( 3, m_waves[0].ennemiesNumer);
		m_enemySpawnerMainController.Spawn( 0, m_waves[0].ennemiesNumer);
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
