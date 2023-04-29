using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Season : MonoBehaviour
{
    public abstract float goal { get; set; }
    public abstract float count { get; set; }
    public abstract Seasons season { get; set; }
    public abstract Seasons goodSeason { get; set; }
    public abstract Seasons badSeason { get; set; }
    public abstract UnityEvent onSeasonStart { get; set; }

    public delegate void ObjetiveAdded();
    public static event ObjetiveAdded objetiveAdded;

    public abstract void StartSeason();
    public abstract void StopSeason();
    public abstract void CheckObjetive();

    public void TriggerEvent()
    {
        objetiveAdded.Invoke();
    }

}
