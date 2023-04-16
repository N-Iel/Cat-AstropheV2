using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField]
    float AmountReduced = 0.5f;

    public delegate void Flowercollected();
    public static event Flowercollected flowercollected;

    Vector2 originalPos;
    SpriteRenderer _renderer;

    private void Awake()
    {
        originalPos = transform.localPosition;
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            OnFlowerLooted();
    }

    async void OnFlowerLooted()
    {
        if (_renderer.enabled == false) return;

        flowercollected.Invoke();
        transform.localPosition = originalPos;
        _renderer.enabled = false;
        await Task.Delay(2);
        _renderer.enabled = true;
        gameObject.SetActive(false);
    }
}
