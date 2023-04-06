using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General AIData service, it keeps a huge variety of data but not all needs to be used
/// </summary>
public class AIData : MonoBehaviour
{
    // Context AI Data
    public List<Transform> targets { get; set; } // Stores all the positions of possible targets
    public Collider2D[] obstacles { get; set; }  // Stores all the obstacle colliders
    public Transform currentTarget { get; set; } // Stores the current Target detected
    public Vector2 detectedPos { get; set; } // Stores the current Target detected

    public int GetTargetCount() => targets == null ? 0 : targets.Count; // Prevents null Exceptions

    private void OnDisable()
    {
        currentTarget = null;
        detectedPos = Vector2.zero;
    }
}
