using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonWinter : Season
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

    public override void CheckObjetive() { }

    public override void StartSeason()
    {
        Debug.Log("Winter Started, Melth down that river, we need water to drink!");
    }

    public override void StopSeason()
    {
        Debug.Log("Winter stoped, if we run out of water because of you, i will make you pay");
    }
}
