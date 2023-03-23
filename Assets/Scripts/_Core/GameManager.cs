using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    InputActionReference input;

    [Header("Events")]
    [SerializeField]
    UnityEvent onAwake, onStart, onPause, onResume;
    private void Awake()
    {
        onAwake?.Invoke();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        input.action.started += PauseController;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        input.action.started -= PauseController;
    }

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        onStart?.Invoke();
    }

    #region Pause
    void PauseController(InputAction.CallbackContext obj)
    {
        if (Time.timeScale == 0)
        {
            onResume?.Invoke();
        }
        else
        {
            onPause?.Invoke();
        }
    }
    #endregion
}
