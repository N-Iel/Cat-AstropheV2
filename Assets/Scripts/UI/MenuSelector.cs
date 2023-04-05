using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuSelector : MonoBehaviour
{
    [SerializeField]
    UnityEvent onClick;
    bool mouseOver;

    private void Update()
    {
        if (!mouseOver) return;

        if (Input.GetMouseButtonUp(0)) { onClick.Invoke(); Debug.Log("Click"); }
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
        Debug.Log(transform.position);
        // Trigger selected effect
    }

    private void OnMouseExit()
    {
        mouseOver = false;
        //Stop Selected Effect
    }
}
