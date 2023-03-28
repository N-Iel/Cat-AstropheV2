using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateParticlePosition : MonoBehaviour
{
    [SerializeField]
    Transform particle;

    [SerializeField]
    Vector2 offset;

    public void UpdatePosition(GameObject origin)
    {
        particle.position = (Vector2)origin.transform.localPosition + offset;
    }
}
