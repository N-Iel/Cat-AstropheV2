using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Season : MonoBehaviour
{
    public abstract float goal { get; set; }
    public abstract float count { get; set; }
    public abstract Seasons season { get; set; }
    public abstract Seasons goodSeason { get; set; }
    public abstract Seasons badSeason { get; set; }


    public abstract void StartSeason();
    public abstract void StopSeason();
    public abstract void CheckObjetive();
}
