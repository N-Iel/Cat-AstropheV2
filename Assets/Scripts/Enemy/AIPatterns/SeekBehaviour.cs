using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeekBehaviour : SteeringBehaviour
{
    [SerializeField]
    float targetReachedThreshold = 0.5f;

    [SerializeField]
    bool showGizmo = true;

    bool reachedLastTarget = true;

    // Gizmo params
    Vector2 targetPositionCached;
    float[] interestsTemp;

    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
    {

        #region Set Target
        if (reachedLastTarget)
        {
            if(aiData.targets == null || aiData.targets.Count <= 0)
            {
                // If there is no target, stop seeking
                aiData.currentTarget = null;
                return (danger, interest);
            }
            else
            {
                // If there is target update current target
                reachedLastTarget = false;

                // We order by distance all the targets and follow the closest one
                aiData.currentTarget = aiData.targets.OrderBy(
                    target => Vector2.Distance(target.position, transform.position)).First();
            }
        }
        #endregion

        #region Endings
        // If the target get out of sight we want to keep the last known position
        if (aiData.currentTarget && aiData.targets != null && aiData.targets.Contains(aiData.currentTarget))
            targetPositionCached = aiData.currentTarget.position;

        // If the target is at attack range we stop the seeking
        if(Vector2.Distance(transform.position, targetPositionCached) < targetReachedThreshold)
        {
            reachedLastTarget = true;
            aiData.currentTarget = null;
            return (danger, interest);
        }
        #endregion

        #region Interest Dirrections
        // If the target hasn't been reached we check for optimal directions
        Vector2 directionToTarget = (targetPositionCached - (Vector2)transform.position);
        for (int i = 0; i < interest.Length; i++)
        {
            float result = Vector2.Dot(directionToTarget.normalized, Directions.eightDirections[i]);

            // Only directions at least on a 90º range from the target
            if(result > 0)
            {
                float valueToPutIn = result;
                if(valueToPutIn > interest[i])
                {
                    interest[i] = valueToPutIn;
                }
            }
        }
        interestsTemp = interest;
        return (danger, interest);
        #endregion
    }

    private void OnDrawGizmos()
    {
        if(showGizmo == false)
            return;

        Gizmos.DrawSphere(targetPositionCached, 0.2f);

        if(Application.isPlaying && interestsTemp != null)
        {
            if(interestsTemp != null)
            {
                Gizmos.color = Color.green;
                for(int i = 0; i < interestsTemp.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * interestsTemp[i]);
                }
                if (!reachedLastTarget)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(targetPositionCached, 0.1f);
                }
            }
        }
    }

}
