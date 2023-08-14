using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Constants;
using UnityEngine.Events;
using System.Threading.Tasks;
using RengeGames.HealthBars;
using MoreMountains.Feedbacks;

public class TutorialManager : MonoBehaviour
{
    // Enemigos: Habrá que controlar el spawn de ratones y buhos
    // Seasons: Solo se pasará de neutra a primavera
    // Goals: Para la primera fase se usarán flores
    // Bars: La barra mala aparecerá a mitad del tutorial
    [Header("Spawners")]
    [SerializeField]
    GameObject mouseSpawner;
    [SerializeField]
    GameObject owlSpawner;

    [field: Header("Goals")]
    [field: SerializeField]
    List<Flower> flowers = new List<Flower>();
    [SerializeField]
    AudioSource effectsSource;
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioClip onGoodEnemyKill;
    [SerializeField]
    AudioClip onGoodBarCompleted;
    public int goal { get; set; }

    [Header("UI")]
    [SerializeField]
    RadialSegmentedHealthBar goodBar;
    [SerializeField]
    RadialSegmentedHealthBar goodBar2;
    [SerializeField]
    TextMeshProUGUI goodEnemyText;
    [SerializeField]
    RadialSegmentedHealthBar badBar;
    [SerializeField]
    TextMeshProUGUI badEnemyText;
    [SerializeField]
    TextMeshProUGUI season;
    [SerializeField]
    MMF_Player badBarEnabled;

    Enemies goodEnemy = Enemies.Mouse;
    Enemies badEnemy = Enemies.Owl;
    int phase = 0;


    [Header("Events")]
    [SerializeField]
    UnityEvent onSeasonStart;

    private void OnEnable()
    {
        Flower.flowercollected += IncreaseGoal;
        EnemyHealth.onKill += OnEnemyKilled;

        UpdatePhase();
    }

    private void OnDisable()
    {
        Flower.flowercollected -= IncreaseGoal;
        EnemyHealth.onKill -= OnEnemyKilled;
    }

    private void Update()
    {
        if (goodBar.RemoveSegments.Value <= 3 && phase != 1)
            UpdatePhase();
    }

    void IncreaseGoal()
    {
        goal++;
        if(phase == 1)
        {
            goodBar.AddRemoveSegments(-2.5f);
            if (goal == 3)
            {
                UpdatePhase();
                return;
            }
                
            effectsSource.pitch += 0.5f;
            effectsSource.PlayOneShot(onGoodEnemyKill);            
        }
    }

    void ResetBars()
    {
        goodBar?.SetRemovedSegments(10);
        badBar?.SetRemovedSegments(10);
    }

    public void UpdatePhase()
    {
        phase++;
        effectsSource.PlayOneShot(onGoodBarCompleted);
        switch (phase)
        {
            case 1:
                Debug.Log("Phase1");
                foreach (Flower flower in flowers)
                {
                    flower.gameObject.SetActive(true);
                }
                badBar.gameObject.SetActive(false);
                break;
            case 2:
                Debug.Log("Phase2");
                effectsSource.pitch = 1;
                foreach (Flower flower in flowers)
                {
                    flower.OnFlowerDestroyed();
                }
                mouseSpawner.SetActive(true);
                season.text = "Spring";
                goodEnemyText.gameObject.SetActive(true);
                goodEnemyText.text = "Mouse";
                ResetBars();
                // show glow on enemy
                break;
            case 3:
                Debug.Log("Phase3");
                goodEnemy = Enemies.Owl;
                badEnemy = Enemies.Mouse;
                goodEnemyText.text = "Owl";
                badBarEnabled.PlayFeedbacks();
                StartCoroutine(IncreaseBadBar());
                owlSpawner.SetActive(true);
                badBar.gameObject.SetActive(true);
                goodBar.gameObject.GetComponent<RectTransform>().localPosition = new Vector2(57.8f, 0);
                goodBar.SetRemovedSegments(10);
                ResetBars();
                break;
            case 4:
                Debug.Log("Fin del tutorial");
                break;
        }
    }

    IEnumerator IncreaseBadBar()
    {
        if (badBar.RemoveSegments.Value > 3)
        {
            badBar.AddRemoveSegments(-0.01f);
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(IncreaseBadBar());
        }        
    }

    void OnEnemyKilled(Enemies id)
    {
        if (id == goodEnemy && goodBar.RemoveSegments.Value > 3)
        {
            // Subir Barra buena
            if (goodBar.gameObject.activeInHierarchy)
            {
                goodBar.AddRemoveSegments(-2.5f);
                goodBar.SetRemovedSegments(Mathf.Clamp(goodBar.RemoveSegments.Value, 3, 10));
            }
            else
            {
                goodBar2.AddRemoveSegments(-2.5f);
                goodBar2.SetRemovedSegments(Mathf.Clamp(goodBar2.RemoveSegments.Value, 3, 10));
                Debug.Log(goodBar2.RemoveSegments.Value);
            }
                
            effectsSource.pitch += 0.5f;
            effectsSource.PlayOneShot(onGoodEnemyKill);
            //goodEnemyCount++;
            //goodNumber.text = (maxGoodEnemies - goodEnemyCount).ToString();
        }
        else if (id == badEnemy && badBar.RemoveSegments.Value > 3)
        {
            // Subir velocidad barra mala
            badBar.AddRemoveSegments(-1);
            badBar.SetRemovedSegments(Mathf.Clamp(badBar.RemoveSegments.Value, 3, 10));
            if (badBar.RemoveSegments.Value <= 5) musicSource.pitch = 1.05f;
            if (badBar.RemoveSegments.Value <= 6) musicSource.pitch = 1.1f;
            //badEnemyCount++;
            //badNumber.text = (maxBadEnemies - badEnemyCount).ToString();
        }

        //if (goodEnemyCount >= maxGoodEnemies || badEnemyCount >= maxBadEnemies)
        //    ResetEnemyNumbers();
    }
}



