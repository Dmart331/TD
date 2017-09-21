using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING }
    [System.Serializable]
    public class Wave
    {
        public string Name;
        public Transform Enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown = 0f;
    public Transform SpawnPoint;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;

	// Use this for initialization
	void Start ()
    {
        waveCountdown = timeBetweenWaves;

	}
	
	// Update is called once per frame
	void Update ()
    {
        if(state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                //Start newe round
                Debug.Log("Wave Completed");
            }
            else
            {
                
                return;
            }
        }
		if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
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
        
        if (searchCountdown <= 0f)
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
        Debug.Log("Spawn Wave");
        state = SpawnState.SPAWNING;

        for(int i =0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.Enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("completed all waves"); 
        }
        nextWave++;
    }
    void SpawnEnemy(Transform _enemy)
    {
        //Spawn Enemy
        Instantiate(_enemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
        Debug.Log("Spawning Enemy");
    }
}
