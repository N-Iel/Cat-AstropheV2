using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Constants;
using UnityEngine.Events;
using System.Threading.Tasks;

public class TutorialManager : MonoBehaviour
{
    [Header("Spawners")]
    [SerializeField]
    GameObject mouseSpawner;
    [SerializeField]
    GameObject duckSpawner;
    [SerializeField]
    GameObject owlSpawner;
    [SerializeField]
    GameObject deerSpawner;

    [Header("UI")]
    [SerializeField]
    TextMeshProUGUI tutorialText;

    // Texts
    public const string
        Spring = "Sweetheart, the house needs a little bit of colour. \r\nCould you bring some flowers from outside ? That would look perfect in the kitchen.\r\n\r\nPtd: You may encounter some mouses, take care please.",
        Summer = "Sweetheart, the weather is drying up the river. Could you keep it clean from rocks and dirt ? We don`t want to run out of water at home.\r\n\r\nP.S.: The ducks could feel attacked if you get to close to the river, watch out please.",
        Autumn = "Sweetheart, the wind is getting stronger, could you bring down the leafs reamining on the trees? I don't want them all over the place at home.\r\n\r\nPtd: The owls may find your intervention... disturbing, take care please.",
        Winter = "Sweetheart, the river is frozen. Could you melt the frozen parts ? With your fur should be enough, just stay a few seconds close to the ice.\r\n\r\nPtd: The deers are hungry, try to avoid them, you can not run from them.";

    [Header("Events")]
    [SerializeField]
    UnityEvent onSeasonStart;

    public async void NewSeason(Seasons season)
    {
        await Task.Delay(1000);
        tutorialText.text = (string)this.GetType().GetField(season.ToString()).GetValue(this);
        mouseSpawner.SetActive(false);
        owlSpawner.SetActive(false);
        deerSpawner.SetActive(false);
        duckSpawner.SetActive(false);
        onSeasonStart.Invoke();

        switch (season)
        {
            case Seasons.Spring:
                mouseSpawner.SetActive(true);
                break;
            case Seasons.Autumn:
                owlSpawner.SetActive(true);
                break;
            case Seasons.Winter:
                deerSpawner.SetActive(true);
                break;
            case Seasons.Summer:
                duckSpawner.SetActive(true);
                break;
            default:
                break;
        }
    }
}



