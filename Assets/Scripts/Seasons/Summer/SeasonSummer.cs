using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonSummer : Season
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

    public override void CheckObjetive()
    {
        
    }

    public override void StartSeason()
    {
        Debug.Log("Summer Started, The rivers has dried, break the rocks and make that waters run again through the river.");
    }

    public override void StopSeason()
    {
        Debug.Log("Summer Stoped, i hope you got it, otherwhise we are pretty much done");
    }
}
