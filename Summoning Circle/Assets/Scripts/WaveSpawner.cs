using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    private static WaveSpawner _instance;
    public static WaveSpawner Instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;
    }

    public Transform Enemy;

    public List<EntityHub> SpawnedEnemies = new List<EntityHub>();
    public bool WaveRunning = false;

    public int CompletedWaves = 0;

    public float BaseDifficulty = 5;
    public float CurrentDifficulty;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2) && !WaveRunning)
        {
            StartCoroutine(SpawnWave());
        }
    }

    public void ReportDeath(EntityHub entity)
    {
        SpawnedEnemies.RemoveIfThere(entity);
        bool stillRunning = SpawnedEnemies.Count > 0;
        if (WaveRunning && !stillRunning)
        {
            CompletedWaves += 1;
            Debug.Log("Wave complete");
        }
        WaveRunning = stillRunning;
    }

    public IEnumerator SpawnWave()
    {
        WaveRunning = true;
        CurrentDifficulty = BaseDifficulty + BaseDifficulty * CompletedWaves / 5f;
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
