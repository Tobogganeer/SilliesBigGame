using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void onButtonPress(string scene)
    {
        if (scene == "title")
            SceneManager.LoadScene("Level1");
        else if (scene == "exit")
            SceneManager.LoadScene("TitleScreen");
    }
}
