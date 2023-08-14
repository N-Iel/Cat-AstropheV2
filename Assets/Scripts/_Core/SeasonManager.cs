using Constants;
using RengeGames.HealthBars;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SeasonManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField]
    Seasons initialSeason = Seasons.Spring;
    //[SerializeField]
    //[Range(0, 5)]
    //int maxGoodEnemies;
    //[SerializeField]
    //[Range(0, 5)]
    //int maxBadEnemies;
    [SerializeField]
    [Tooltip("Time required for faborable enemies to change")]
    float updateEnemiesRate;
    [SerializeField]
    float badAmountPerEnemy;
    [SerializeField]
    float badAmountIncreaseAmount;
    [SerializeField]
    float badAmountIncreaseRate;
    [SerializeField]
    float goodAmountPerEnemy;
    [field: SerializeField]
    public float goodAmountIncreaseRate { get; set; }
    public float goodAmountIncreaseAmount { get; set; }

    int SeasonsPlayed = 1;
    bool realEnding = true;

    [field: Header("Bars")]

    // GoodBar
    [field: SerializeField]
    public RadialSegmentedHealthBar goodBar { get; set; }
    [SerializeField]
    TextMeshProUGUI goodName;
    [SerializeField]
    TextMeshProUGUI goodNumber;
    [SerializeField]
    AudioClip onGoodEnemyKill;
    [SerializeField]
    AudioClip onGoodBarCompleted;

    // BadBar
    [field: SerializeField]
    public RadialSegmentedHealthBar badBar { get; set; }
    [SerializeField]
    TextMeshProUGUI badName;
    [SerializeField]
    TextMeshProUGUI badNumber;
    [SerializeField]
    AudioClip onBadBarCompleted;

    [Header("Components")]
    [SerializeField]
    TextMeshProUGUI seasonText;
    [SerializeField]
    AudioSource effectsSource;
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    EnemySpawner duckSpawner;
    [SerializeField]
    EnemySpawner deerSpawner;

    [Header("Event")]
    [SerializeField]
    UnityEvent<Seasons> OnNewSeason;

    public static SeasonManager seasonManager { get; private set; }
    List<Season> availableSeasons = new List<Season>();
    public Season currentSeason { get; private set; }
    Enemies goodEnemy, badEnemy;

    // Randomness
    System.Random random = new System.Random();
    Array enemies = new Enemies[99];

    private void Awake()
    {
        seasonManager = this;
    }

    private async void OnEnable()
    {
        // Events
        EnemyHealth.onKill += OnEnemyKilled;
        Season.objetiveAdded += IncreaseGoodBar;

        // Lists
        enemies.SetValue(Enemies.Mouse, 0);
        enemies.SetValue(Enemies.Owl, 1);

        foreach (GameObject season in GameObject.FindGameObjectsWithTag("Season"))
        {
            availableSeasons.Add(season.GetComponent<Season>());
        };

        await Task.Delay(4000);

        // Initialization
        seasonText.text = initialSeason.ToString();
        currentSeason = availableSeasons.Find((season) => season.season == initialSeason);
        OnNewSeason.Invoke(currentSeason.season);
        currentSeason.StartSeason();

        InvokeRepeating("ResetEnemyNumbers",0,updateEnemiesRate);
        StartCoroutine(IncreaseBadBar());
        goodAmountIncreaseAmount = 0;

        // Progresión automática de la barra buena
        // StartCoroutine(IncreaseGoodBar());
    }

    private void OnDisable()
    {
        EnemyHealth.onKill -= OnEnemyKilled;
        Season.objetiveAdded -= IncreaseGoodBar;
    }

    void Update()
    {
        // Seasons
        if (badBar.RemoveSegments.Value <= 3 || goodBar.RemoveSegments.Value <= 3)
            OnSeasonChange(goodBar.RemoveSegments.Value <= 3);

        //DEBUG
        if (Input.GetKeyDown(KeyCode.Z))
            OnEnemyKilled(goodEnemy);

        if (Input.GetKeyDown(KeyCode.X))
            OnEnemyKilled(badEnemy);

        if (Input.GetKeyDown(KeyCode.R))
            ResetEnemyNumbers();
    }

    // TODO Revisar mecánica, por ahora la quito ya que me parece un poco abrumador todo. Puede que con una buena progresión añada profundidad
    public void OnEnemyKilled(Enemies id)
    {
        if(id == goodEnemy && goodBar.RemoveSegments.Value > 3)
        {
            // Subir Barra buena
            goodBar.AddRemoveSegments(-goodAmountPerEnemy);
            goodBar.SetRemovedSegments(Mathf.Clamp(goodBar.RemoveSegments.Value, 3, 10));
            effectsSource.pitch += 0.5f;
            effectsSource.PlayOneShot(onGoodEnemyKill);
            //goodEnemyCount++;
            //goodNumber.text = (maxGoodEnemies - goodEnemyCount).ToString();
        }
        else if(id == badEnemy && badBar.RemoveSegments.Value > 3)
        {
            // Subir velocidad barra mala
            badBar.AddRemoveSegments(-badAmountPerEnemy);
            badBar.SetRemovedSegments(Mathf.Clamp(badBar.RemoveSegments.Value, 3, 10));
            if (badBar.RemoveSegments.Value <= 5) musicSource.pitch = 1.05f;
            if (badBar.RemoveSegments.Value <= 6) musicSource.pitch = 1.1f;
            //badEnemyCount++;
            //badNumber.text = (maxBadEnemies - badEnemyCount).ToString();
        }

        //if (goodEnemyCount >= maxGoodEnemies || badEnemyCount >= maxBadEnemies)
        //    ResetEnemyNumbers();
    }

    void ResetEnemyNumbers()
    {
        // Actualizar número bueno
        //maxGoodEnemies = UnityEngine.Random.Range(1, 5);
        goodEnemy = (Enemies)enemies.GetValue(random.Next(enemies.Length));
        //goodEnemyCount = 0;
        //goodNumber.text = maxGoodEnemies.ToString();
        goodName.text = goodEnemy.ToString();

        // Actualizar número malo
        //maxBadEnemies = UnityEngine.Random.Range(1, 5);
        do badEnemy = (Enemies)enemies.GetValue(random.Next(enemies.Length)); while (badEnemy == goodEnemy);
        //badEnemyCount = 0;
        //badNumber.text = maxBadEnemies.ToString();
        badName.text = badEnemy.ToString();
    }

    void OnSeasonChange(bool isGood)
    {
        effectsSource.pitch = 1;

        // Bar Managemenet
        if (isGood)
        {
            badBar.SetRemovedSegments(Mathf.Clamp(badBar.RemoveSegments.Value + 3, 3, 10));
            goodBar.SetRemovedSegments(10);
            effectsSource.PlayOneShot(onGoodBarCompleted);
        }
        else
        {
            //goodBar.SetRemovedSegments(Mathf.Clamp(goodBar.RemoveSegments.Value + 3, 3, 10));
            goodBar.SetRemovedSegments(10);
            badBar.SetRemovedSegments(10);
            effectsSource.PlayOneShot(onBadBarCompleted);
        }

        musicSource.pitch = 1;
        StopAllCoroutines();
        TriggerNewSeason(isGood);
        StartCoroutine(IncreaseBadBar());
    }

    void TriggerNewSeason(bool isGood)
    {
        string _season;

        // Real end check
        if (currentSeason.season == Seasons.Winter && realEnding && isGood)
            _season = Seasons.Dark.ToString();
        else
            _season = isGood ? currentSeason.goodSeason.ToString() : currentSeason.badSeason.ToString();

        currentSeason.StopSeason();
        currentSeason = availableSeasons.Find((season) => season.season.ToString() == _season);
        OnNewSeason.Invoke(currentSeason.season);
        currentSeason.StartSeason();

        seasonText.text = currentSeason.season.ToString();
        SeasonsPlayed++;
        if (SeasonsPlayed == 3)
        {
            duckSpawner.gameObject.SetActive(true);
            enemies.SetValue(Enemies.Duck, 3);
        }
        if (SeasonsPlayed == 4)
        {
            deerSpawner.gameObject.SetActive(true);
            enemies.SetValue(Enemies.Deer, 4);
        }

        RealEndingSequence();
    }

    IEnumerator IncreaseBadBar()
    {
        badBar.AddRemoveSegments(-badAmountIncreaseAmount);
        yield return new WaitForSeconds(badAmountIncreaseRate);
        StartCoroutine(IncreaseBadBar());
    }

    void IncreaseGoodBar()
    {
        goodBar.AddRemoveSegments(-(7.1f / currentSeason.goal));
    }

    void RealEndingSequence()
    {
        switch (SeasonsPlayed)
        {
            case 2:
                if (currentSeason.season != Seasons.Summer) realEnding = false;
                break;
            case 3:
                if (currentSeason.season != Seasons.Autumn) realEnding = false;
                break;
            case 4:
                if (currentSeason.season != Seasons.Winter) realEnding = false;
                break;
            default:
                break;
        }
    }
}
