using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Season : MonoBehaviour
{
    public abstract Seasons season { get; set; }

    public abstract void StartSeason();

    public abstract void StopSeason();
}
