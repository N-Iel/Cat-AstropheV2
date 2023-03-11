using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will generate shadows of the customTarget behind them in order to generate a stele effect
/// </summary>
public class Shadow : MonoBehaviour
{
    [SerializeField]
    float frecuency = 0.2f;
    [SerializeField]
    [Tooltip("Introduce a gameobject to take as shadow target")]
    GameObject customTarget;
    [SerializeField]
    SpriteRenderer shadowRender;
    [SerializeField]
    GameObject shadowParent;

    float counter;
    SpriteRenderer targetRenderer;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        counter = frecuency;

        // In case the target is a customTarget it will be selected instead of the gameobject
        target = customTarget ? customTarget : gameObject;
        targetRenderer = target.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter > 0)
            counter -= Time.deltaTime;
        else
            SpawnShadow();
    }

    // Instantiate a shadow in the customTarget 
    void SpawnShadow()
    {
        // Instantiate
        SpriteRenderer currentGhost = Instantiate(shadowRender, transform.position, transform.rotation, shadowParent.transform);
        currentGhost.sprite = targetRenderer.sprite;
        currentGhost.flipX = target.transform.localScale.x < 0;
        counter = frecuency;
    }
}
