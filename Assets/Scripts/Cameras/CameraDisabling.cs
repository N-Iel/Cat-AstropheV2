using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraDisabling : MonoBehaviour
{
    [SerializeField]
    GameObject cameraToDisable;

    [SerializeField]
    [Tooltip("Optional value in case a new camera require activation")]
    GameObject cameraToEnable;

    [SerializeField]
    int timeBeforeDisabling;

    public async void DisableCamera()
    {
        await Task.Delay(timeBeforeDisabling);
        cameraToDisable.SetActive(false);

        if (cameraToEnable) cameraToEnable.SetActive(true);
    }
}
