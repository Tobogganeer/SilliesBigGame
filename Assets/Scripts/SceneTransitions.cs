using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public void NewScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}