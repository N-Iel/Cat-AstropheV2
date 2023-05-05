using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField]
    List<GameObject> frozenParts;

    [field: Header("Events")]
    [field: SerializeField]
    public override UnityEvent onSeasonStart { get; set; }

    public override void StartSeason()
    {
        Debug.Log("Winter Started, Melth down that river, we need water to drink!");
        //frozenParts = Utils.updateListItems(frozenParts, true);
        //FrozenPart.melted += CheckObjetive;
        onSeasonStart.Invoke();
    }

    public override void StopSeason()
    {
        Debug.Log("Winter stoped, if we run out of water because of you, i will make you pay");
        //frozenParts = Utils.updateListItems(frozenParts, false);
        //FrozenPart.melted -= CheckObjetive;
    }

    public override void CheckObjetive() 
    {
        //TriggerEvent();
        //count++;
    }
}
