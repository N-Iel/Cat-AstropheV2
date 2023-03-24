using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUISound : MonoBehaviour
{
    [SerializeField]
    AudioClip[] clicks, selects;
    [SerializeField]
    AudioSource source;

    AudioClip onClick;
    AudioClip onSelect;
    void Awake()
    {
        onClick = clicks[Random.Range(0, clicks.Length)];
        onSelect = selects[Random.Range(0, selects.Length)];
        Debug.Log("Selected click: " + onClick);
        Debug.Log("Select sound: " + onSelect);
    }

    public void PlayClick()
    {
        source.PlayOneShot(onClick);
    }
    public void PlaySelect()
    {
        source.PlayOneShot(onSelect);
    }
}
