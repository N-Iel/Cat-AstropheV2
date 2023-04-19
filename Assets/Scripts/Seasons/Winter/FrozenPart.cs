using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class FrozenPart : MonoBehaviour
{
    [SerializeField]
    float meltTime = 5;
    float time = 0;
    bool isMelting = false;

    public delegate void Melted();
    public static event Melted melted;

    private void OnEnable()
    {
        time = 0;
    }

    private void Update()
    {
        if (time >= meltTime)
        {
            melted.Invoke();
            gameObject.SetActive(false);

            return;
        }

        if (isMelting)
            time += Time.deltaTime;
        else if(time > 0)
            time -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isMelting = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isMelting = false;
    }
}
