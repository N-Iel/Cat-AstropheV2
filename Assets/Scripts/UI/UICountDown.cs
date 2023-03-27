using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

/// <summary>
/// Necesitamos que pueda arrancarse la cuenta atras, y con un boolean saber si tiene que alterarse el tiempo o no
/// </summary>
public class UICountDown : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip countSound;
    [SerializeField]
    AudioClip finishSound;

    [Header("UI")]
    [SerializeField]
    TextMeshProUGUI text;
    int count = 3;

    public async void StartCountDown(bool updateTime)
    {
        if (count != 3) return;
        if (updateTime) Time.timeScale = 0;
        do
        {
            text.SetText(count.ToString());
            audioSource.PlayOneShot(countSound);
            await Task.Delay(1000);
            count--;
        } while (count != 0);

        text.SetText("GO!!");
        audioSource.PlayOneShot(finishSound);
        await Task.Delay(500);
        Finish(updateTime);
    }

    private void Finish(bool updateTime)
    {
        if (updateTime) Time.timeScale = 1;
        text.SetText("");
        count = 3;
    }
}
