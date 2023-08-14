using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ResetScene : MonoBehaviour
{
    public void Reset()
    {
        Utils.UpdateTimeScale(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
