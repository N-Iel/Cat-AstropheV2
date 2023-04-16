using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonDark : Season
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
        Debug.Log("Dark falls, bring the light back... this is our last chance. Enemies have high ground now... be fast and take care.");
    }

    public override void StopSeason()
    {
        Debug.Log("You Made IT!!! Congratulations");
    }
}
