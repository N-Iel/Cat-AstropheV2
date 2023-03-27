using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    // Main
    public static ProjectManager Instance;

    // ButtonsSoundsTest
    [SerializeField]
    AudioClip[] clicks, selects;
    [field: SerializeField]
    public AudioClip onClick { get; private set; }
    [field: SerializeField]
    public AudioClip onSelect { get; private set; }
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeSounds();
    }

    private void InitializeSounds()
    {
        //onClick = clicks[Random.Range(0, clicks.Length)];
        //onSelect = selects[Random.Range(0, selects.Length)];
        Debug.Log("Selected click: " + onClick);
        Debug.Log("Select sound: " + onSelect);
    }
}
