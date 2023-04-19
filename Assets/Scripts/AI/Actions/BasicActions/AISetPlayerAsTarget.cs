using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISetPlayerAsTarget : MonoBehaviour
{
    [SerializeField]
    MMFollowTarget followTarget;

    [SerializeField]
    MMF_Player attackEffect;
    void Awake()
    {
        followTarget.ChangeFollowTarget(Player.player.transform);
        attackEffect.GetFeedbackOfType<MMF_DestinationTransform>().Destination = Player.player.transform;
    }

    public void UpdateOffsetPos()
    {
        followTarget.Offset = followTarget.Offset.MMSetX(Random.Range(-14f, 14f));
    }
}
