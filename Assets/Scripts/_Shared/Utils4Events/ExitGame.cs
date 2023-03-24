using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    [SerializeField]
    int delay = 0;
    public async void Exit()
    {
        await Task.Delay(delay);
        Debug.Log("Exitting game");
        Application.Quit();
    }
}
