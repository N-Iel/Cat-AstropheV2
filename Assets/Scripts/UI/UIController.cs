using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void NextScene(string scene)
    {
        Debug.Log("Loading: " + scene);
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Debug.Log("Exitting game");
        Application.Quit();
    }
}
