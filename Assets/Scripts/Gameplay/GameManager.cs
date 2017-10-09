using UnityEngine;
using UnityEngine.SceneManagement;

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
	
	[SerializeField]
	float m_tutoTime = 5;
	float m_tutoStartTime;

	void Start()
	{
		AkSoundEngine.PostEvent("music_menu", gameObject);
	}

	void Update()
	{
		switch (m_state)
		{
			case GameState.Intro:
				if (Input.GetKeyDown(KeyCode.Escape))
					Application.Quit();
				else if (Input.anyKeyDown)
				{
					m_UIAnimator.Play("Intro_End");
					m_tutoStartTime = Time.time;
					m_state++;
				}
				break;
			case GameState.Tutorial:
				if (Input.GetKeyDown(KeyCode.Escape))
					Restart();
				else if (Time.time > m_tutoStartTime + m_tutoTime)
				{
					m_UIAnimator.Play("Tuto_End");
					AkSoundEngine.PostEvent("music_game", gameObject);
					m_nextWaveTime = Time.time + m_waves[0].time;
					StartWave();
					m_state++;
				}
				break;
			case GameState.Main:
				if (Input.GetKeyDown(KeyCode.Escape))
					Restart();
				else if (m_alive)
				{
					m_gameTime += Time.deltaTime;

					if(m_gameTime > 270)
					{
						EndGame (true);
						m_state++;
					}
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
			AkSoundEngine.PostEvent("end_win", gameObject);
			m_UIAnimator.Play("End_Start_Win");

			Invoke("Restart", 5);
		}
		else
		{
			m_alive = false;
			AkSoundEngine.PostEvent("end_lose", gameObject);
			m_UIAnimator.Play("End_Start_Lose");

			Invoke("Restart", 5);
		}

		m_gameTime++;
	}

	public void Restart()
	{
		AkSoundEngine.StopAll();
		SceneManager.LoadScene(0);
	}

	public float GetGameTime()
	{
		return m_gameTime;
	}
}
