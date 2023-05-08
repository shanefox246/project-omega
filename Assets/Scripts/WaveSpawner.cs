using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState {Spawning, Waiting, Counting};

    [System.Serializable]
    public class Wave
    {
        public Transform enemy;
        public int amount;
        public float spawnRate;
    }

    public Text waveText;

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.Counting;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
        UpdateWaveText();
    }

    void Update()
    {
        if(state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
                UpdateWaveText();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if(state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.Spawning;
        for(int i =0; i < _wave.amount; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }

        state = SpawnState.Waiting;


        yield break;
    }
    void SpawnEnemy(Transform _enemy)
    {
        //Spawn enemy
        Debug.Log("Spawning enemy" + _enemy.name);

        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, sp.position, sp.rotation);
        
    }

    void WaveCompleted()
    {
        
        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }
    }

    void UpdateWaveText()
    {
        waveText.text = "Wave: " + (nextWave + 1);
    }

}
