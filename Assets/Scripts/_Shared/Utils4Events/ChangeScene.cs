using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    int delay = 0;

    public async void NextScene(string scene)
    {
        await Task.Delay(delay);
        Debug.Log("Loading: " + scene);
        SceneManager.LoadScene(scene);
    }
}
