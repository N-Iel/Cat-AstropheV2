using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will generate shadows of the customTarget behind them in order to generate a stele effect
/// </summary>
public class Shadow : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    float frecuency = 0.2f;
    //[SerializeField]
    //[Tooltip("Prevent shadow from flipX")]
    //bool flipXInverted = false;

    [Header("Components")]
    [SerializeField]
    [Tooltip("Introduce a gameobject with the Scale reference")]
    GameObject scaleReference;
    [SerializeField]
    [Tooltip("Introduce a gameobject with the sprite reference")]
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
        if (!shadowParent) shadowParent = GameObject.Find("Shadows");

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
        SpriteRenderer currentGhost = Instantiate(shadowRender, target.transform.position, target.transform.rotation, shadowParent.transform);
        currentGhost.sprite = targetRenderer.sprite;

        currentGhost.transform.localScale = scaleReference ? scaleReference.transform.localScale : target.transform.localScale;
        currentGhost.flipX = targetRenderer.flipX;
        currentGhost.flipY = targetRenderer.flipY;
        //if (flipXInverted) currentGhost.flipX = !currentGhost.flipX;

        counter = frecuency;
    }
}
