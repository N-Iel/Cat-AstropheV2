using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayClick()
    {
        source.PlayOneShot(ProjectManager.Instance.onClick);
    }

    public void PlaySelect()
    {
        source.PlayOneShot(ProjectManager.Instance.onSelect);
    }
}
