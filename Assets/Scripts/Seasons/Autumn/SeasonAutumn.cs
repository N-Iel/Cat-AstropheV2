using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField]
    List<GameObject> trees;

    [field: Header("Events")]
    [field: SerializeField]
    public override UnityEvent onSeasonStart { get; set; }

    public override void StartSeason()
    {
        Debug.Log("Autumn Started, Take out those leafs from the trees, the are supouse to fall but...");
        Tree.treeHitted += CheckObjetive;
        onSeasonStart.Invoke();
        count = 0;
    }

    public override void StopSeason()
    {
        Debug.Log("Autumn stoped, you filled the ground with leafs? I hope so");
        Tree.treeHitted -= CheckObjetive;

        foreach (GameObject tree in trees)
            tree.SetActive(true);
    }

    public override void CheckObjetive() 
    {
        Debug.Log("Tree Hitted");
        TriggerEvent();
        count++;
    }
}
