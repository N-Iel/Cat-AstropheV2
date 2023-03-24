using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Exitting game");
        Application.Quit();
    }
}
