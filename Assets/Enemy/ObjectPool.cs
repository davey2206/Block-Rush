using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField][Range(0, 50)] int PoolSize = 5;
    [SerializeField][Range(1f, 30f)] float SpawnTimer = 1f;
    [SerializeField][Range(1f, 30f)] float WaveTimer = 30f;
    [SerializeField][Range(1f, 50f)] int WaveSize = 2;
    [SerializeField][Range(1.3f, 10f)] float RampUpMultiplier = 1.3f;

    GameObject[] Pool;
    Coroutine SpawnEnemies;

    void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    void PopulatePool()
    {
        Pool = new GameObject[PoolSize];

        for (int i = 0; i < PoolSize; i++)
        {
            Pool[i] = Instantiate(EnemyPrefab, transform);
            Pool[i].SetActive(false);
        }
    }

    IEnumerator SpawnEnemiesEnumerator()
    {
        for (int i = 0; i < WaveSize; i++)
        {
            if (i == WaveSize - 1)
            {
                StopCoroutine(SpawnEnemies);

                WaveSize = WaveSize * 2;
                if (WaveSize > PoolSize)
                {
                    WaveSize = PoolSize;
                }

                StartCoroutine(StartNextWave());
            }

            if (Pool[i].activeInHierarchy == false)
            {
                Pool[i].SetActive(true);
                yield return new WaitForSeconds(SpawnTimer);
            }
        }
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(WaveTimer);
        SpawnEnemies = StartCoroutine(SpawnEnemiesEnumerator());
        foreach (var enemy in Pool)
        {
            enemy.GetComponent<EnemyHealth>().IncraaseHP(RampUpMultiplier);
        }
    }
}
