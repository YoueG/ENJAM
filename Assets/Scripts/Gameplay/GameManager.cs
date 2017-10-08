using UnityEngine;

[System.Serializable]
public struct Wave
{
	[SerializeField]
	public float delay;
	[SerializeField]
	public float time;
	[SerializeField, Range(0.5f,10f)]
	public float spawnTimeMin, spawnTimeMax;
	[SerializeField]
	public Vector2 countLimits;
	[SerializeField, Tooltip("-1 = Random")]
	public int division;
	[SerializeField, Range(0,2)]
	public float speed;
	[SerializeField]
	public Pattern pattern;
}

enum GameState
{
	Intro,
	Tutorial,
	Main,
	End
}

public class GameManager : Singleton<GameManager>
{
	[SerializeField]
	EnemySpawner m_enemySpawnerMainController;

	[SerializeField]
	Wave[] m_waves;

	[SerializeField]
	Animator m_UIAnimator;

	GameState m_state = GameState.Intro;

	bool m_alive = true;
	float m_gameTime = 0;
	float m_nextWaveTime;
	
	float m_tutoTime = 5;
	float m_tutoStartTime;
	void Update()
	{
		switch (m_state)
		{
			case GameState.Intro:
				if (Input.anyKeyDown)
				{
					m_UIAnimator.Play("Intro_End");
					m_tutoStartTime = Time.time;
					m_state++;
				}
				break;
			case GameState.Tutorial:
				if(Time.time > m_tutoStartTime + m_tutoTime)
				{
					m_UIAnimator.Play("Tuto_End");
					m_nextWaveTime = Time.time + m_waves[0].time;
					StartWave();
					m_state++;
				}
				break;
			case GameState.Main:
				if (m_alive)
				{
					m_gameTime += Time.deltaTime;
				}
				break;
			case GameState.End:
				break;
		}
	}

	int m_currentWave = 0;
	void StartNextWave()
	{
		if (++m_currentWave >= m_waves.Length)
		{
			EndGame(true);
			return;
		}

		m_nextWaveTime = Time.time + m_waves[m_currentWave].time;
		StartWave();
	}
	
	void StartWave()
	{
		if(Time.time < m_nextWaveTime)
		{
			m_enemySpawnerMainController.Spawn(m_waves[m_currentWave]);
			Invoke("StartWave", Random.Range(m_waves[m_currentWave].spawnTimeMin, m_waves[m_currentWave].spawnTimeMin));
		}
		else
			Invoke("StartNextWave", m_waves[m_currentWave].delay);
	}

	public void EndGame(bool victory)
	{
		if(victory)
		{
			print("Victory");
		}
		else
			m_alive = false;

		m_UIAnimator.Play("End_Start");
		m_gameTime++;
	}

	public float GetGameTime()
	{
		return m_gameTime;
	}
}
