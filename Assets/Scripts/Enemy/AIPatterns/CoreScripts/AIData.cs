using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIData : MonoBehaviour
{
    public List<Transform> targets = null; // Stores all the positions of possible targets
    public Collider2D[] obstacles = null;  // Stores all the obstacle colliders

    public Transform currentTarget;

    public int GetTargetCount() => targets == null ? 0 : targets.Count; // Prevents null Exceptions
}
