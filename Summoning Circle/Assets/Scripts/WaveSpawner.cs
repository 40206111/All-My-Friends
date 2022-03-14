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
        NextReward = eRoundSigil.none;
    }

    private void OnEnable()
    {
        Pedastal.OnItemPickUp += OnPedestalPickup;
    }

    private void OnDisable()
    {
        Pedastal.OnItemPickUp -= OnPedestalPickup;
    }

    public static Action OnWaveStart;
    public static Action OnPreBoss;
    public static Action OnBossEnd;
    public static Action OnCompletedSummons;

    public Transform Enemy;
    public EnemyPool EnemyPool;

    public List<EntityHub> SpawnedEnemies = new List<EntityHub>();
    public bool CombatRunning = false;
    bool itemRetrieved = false;

    public int CompletedWaves = 0;
    public int CompletedSummons = 0;

    public int WavesPerSummon = 5;

    public float BaseDifficulty = 5;
    public float CurrentDifficulty;

    public Transform HealthPickup;

    public static eRoundSigil NextReward = eRoundSigil.none;

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
        if (NextReward != eRoundSigil.none)
        {
            return;
        }
        NextReward = sigil;
        StartCoroutine(RunSummon());
    }

    private IEnumerator RunSummon()
    {
        itemRetrieved = false;
        CompletedWaves = 0;
        // Spawn waves
        for (int i = 0; i < WavesPerSummon; ++i)
        {
            yield return StartCoroutine(SpawnWave(i / 5f));
            float nextWave = 15.0f;
            while (nextWave > 0 && CombatRunning)
            {
                yield return null;
                nextWave -= Time.deltaTime;

            }
        }
        OnCompletedSummons?.Invoke();
        // Finish stragglers
        while (CombatRunning)
        {
            yield return null;
        }
        OnPreBoss?.Invoke();
        // Wait for reward retrieval
        if (NextReward == eRoundSigil.Item || NextReward == eRoundSigil.Skull)
        {
            for (int i = 0; i < 3; ++i)
            {
                Instantiate(HealthPickup, (Vector2.down * 2f).Around(0.8f), Quaternion.identity);
            }
        }
        if (NextReward == eRoundSigil.Goblin || NextReward == eRoundSigil.Skull)
        {
            while (!itemRetrieved)
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(5);
        // Fight boss
        SpawnEnemy(EnemyPool.GetBoss());
        CombatRunning = true;
        while (CombatRunning)
        {
            yield return null;
        }

        OnBossEnd?.Invoke();
        NextReward = eRoundSigil.none;
    }

    private void OnPedestalPickup(Item _)
    {
        itemRetrieved = true;
    }

    private IEnumerator SpawnWave(float difficultyMultiplier)
    {
        OnWaveStart?.Invoke();
        if (difficultyMultiplier > 0)
        {
            WaveRewards();
        }
        CombatRunning = true;
        CurrentDifficulty = BaseDifficulty + BaseDifficulty * difficultyMultiplier;
        for (int i = 0; i < CurrentDifficulty;)
        {
            Transform enemy = null;
            int nextThreat = 0;
            while (enemy == null)
            {
                nextThreat = UnityEngine.Random.Range(1, 4);
                enemy = EnemyPool.GetEnemy(nextThreat);
            }
            SpawnEnemy(enemy);
            i += nextThreat;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void WaveRewards()
    {
        if (UnityEngine.Random.Range(0, 3) == 0)
        {
            Instantiate(HealthPickup, (Vector2.down * 2f).Around(0.8f), Quaternion.identity);
        }
    }

    private void SpawnEnemy(Transform prefab)
    {
        EntityHub enemy = Instantiate(prefab, Vector2.zero.Around(1f), Quaternion.identity, transform).GetComponent<EntityHub>();
        enemy.OnDeath += ReportDeath;
        SpawnedEnemies.Add(enemy);
    }
}
