using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Load the level
    public void LoadLevel()
    {
        SceneManager.LoadScene("Main");
    }

    // Exit the Game
    public void ExitGame()
    {
        Application.Quit();
    }
}
