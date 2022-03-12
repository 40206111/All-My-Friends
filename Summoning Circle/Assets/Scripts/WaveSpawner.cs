using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveSpawner : MonoBehaviour
{
    private static WaveSpawner _instance;
    public static WaveSpawner Instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;
    }

    public static Action OnWaveStart;
    public static Action OnPreBoss;
    public static Action OnBossEnd;

    public Transform Enemy;

    public List<EntityHub> SpawnedEnemies = new List<EntityHub>();
    public bool CombatRunning = false;

    public int CompletedWaves = 0;
    public int CompletedSummons = 0;

    public float BaseDifficulty = 5;
    public float CurrentDifficulty;

    private eRoundSigil NextReward = eRoundSigil.none;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(2) && !CombatRunning)
        //{
        //    StartCoroutine(SpawnWave());
        //}
    }

    public void ReportDeath(EntityHub entity)
    {
        SpawnedEnemies.RemoveIfThere(entity);
        bool stillRunning = SpawnedEnemies.Count > 0;
        if (CombatRunning && !stillRunning)
        {
            CompletedWaves += 1;
            Debug.Log("Wave complete");
        }
        CombatRunning = stillRunning;
    }

    public void StartSummon(eRoundSigil sigil)
    {
        NextReward = sigil;
        StartCoroutine(RunSummon());
    }

    private IEnumerator RunSummon()
    {
        CompletedWaves = 0;

        for (int i = 0; i < 5; ++i) {
            yield return StartCoroutine(SpawnWave(i / 5f));
            float nextWave = 8.0f;
            while(nextWave > 0 && CombatRunning)
            {
                yield return null;
                nextWave -= Time.deltaTime;

            }
        }
        OnPreBoss?.Invoke();
        // ~~~ spawn reward
        NextReward = eRoundSigil.none;
        // ~~~ spawn boss
        OnBossEnd?.Invoke();
    }

    private IEnumerator SpawnWave(float difficultyMultiplier)
    {
        OnWaveStart?.Invoke();
        CombatRunning = true;
        CurrentDifficulty = BaseDifficulty + BaseDifficulty * difficultyMultiplier;
        for (int i = 0; i < CurrentDifficulty; ++i)
        {
            SpawnEnemy(Enemy);
            yield return new WaitForSeconds(1.5f);
        }
    }

    private void SpawnEnemy(Transform prefab)
    {
        EntityHub enemy = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform).GetComponent<EntityHub>();
        enemy.OnDeath += ReportDeath;
        SpawnedEnemies.Add(enemy);
    }
}
