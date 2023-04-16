using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonAutumn : Season
{
    // Base params
    [field: Header("Params")]
    [field: SerializeField]
    public override float goal { get; set; }
    [field: SerializeField]
    public override float count { get; set; }
    [field: SerializeField]
    public override Seasons season { get; set; }

    [field: SerializeField]
    public override Seasons goodSeason { get; set; }
    [field: SerializeField]
    public override Seasons badSeason { get; set; }

    public override void CheckObjetive() {}

    public override void StartSeason()
    {
        Debug.Log("Autumn Started, Take out those leafs from the trees, the are supouse to fall but...");
    }

    public override void StopSeason()
    {
        Debug.Log("Autumn stoped, you filled the ground with leafs? I hope so");
    }
}
