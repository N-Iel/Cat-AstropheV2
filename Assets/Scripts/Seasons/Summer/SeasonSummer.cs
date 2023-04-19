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

    [SerializeField]
    List<GameObject> rocks;

    public override void StartSeason()
    {
        Debug.Log("Summer Started, The rivers has dried, break the rocks and make that waters run again through the river.");
        rocks = Utils.updateListItems(rocks, true);
        Rock.rockDestroyed += CheckObjetive;
    }

    public override void StopSeason()
    {
        Debug.Log("Summer Stoped, i hope you got it, otherwhise we are pretty much done");
        rocks = Utils.updateListItems(rocks, false);
        Rock.rockDestroyed -= CheckObjetive;
    }

    public override void CheckObjetive()
    {
        Debug.Log("Rock Destroyed");
        TriggerEvent();
        count++;
    }
}
