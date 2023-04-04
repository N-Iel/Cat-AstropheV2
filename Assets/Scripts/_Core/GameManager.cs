using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables

    #region Params
    [Header("Parameters")]
    [SerializeField]
    float initialDelay = 0f;
    #endregion

    #region Components
    [Header("Components")]
    [SerializeField]
    InputActionReference input;
    #endregion

    #region Events
    [Header("Events")]
    [SerializeField]
    UnityEvent onAwake;
    [SerializeField]
    UnityEvent onStart;
    [SerializeField]
    UnityEvent onDelayedStart;
    [SerializeField]
    UnityEvent onPause;
    [SerializeField]
    UnityEvent onResume;
    #endregion

    #endregion

    #region LifeCycle
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
        Invoke("DelayedStart", initialDelay);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && Time.timeScale == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion

    #region Methods
    void DelayedStart()
    {
        onDelayedStart?.Invoke();
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        onStart?.Invoke();
    }
    void PauseController(InputAction.CallbackContext obj)
    {
        if (Player.player.isDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }
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
