using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    //allows to change vlaues of instance of the class
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }
    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetween = 5f;
    public float waveCountDown;

    private float searchCountDown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        waveCountDown = timeBetween;
    }

    void Update()
    {
        if(state == SpawnState.WAITING)
        {
            //check if enemys are dead
            if (!EnemyIsAlive())
            {
                //begin a new round
                WaveCompleted();
                return;
            }
            else
            {
                return;
            }
        }

        if(waveCountDown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                //Start spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");
        state = SpawnState.COUNTING;
        waveCountDown = timeBetween;

        if(nextWave + 1 > waves.Length -1)
        {
            //scene change here current input starts it again
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE!! Looping..");
        }
        nextWave++;
    }

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("EnemyShipTag") == null)
            {

                return false;
            }
        }
        return true;

    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave" + _wave.name);
        state = SpawnState.SPAWNING;

        //Spawn
        for(int i = 0;i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);

        }

        state = SpawnState.WAITING;

        yield break;
    }

    public void SpawnEnemy(Transform _enemy)
    {
       
        Debug.Log("Spawning Enemy" + _enemy.name);
        
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        _enemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        Instantiate(_enemy, _enemy.transform.position, transform.rotation);
    }
    public void ScheduleSpawner()
    {
        

        Invoke("SpawnEnemy", searchCountDown);

        
    }


    public void UnscheduledSpawner()
    {
        CancelInvoke("SpawnEnemy");
       
    }
}












/* public GameObject Enemy;
    public int enemyCount;

    float maxSpawnRateInSeconds = 2f;
	// Use this for initialization
	void Start () {

        
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject anEnemy = (GameObject)Instantiate(Enemy);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextEnemySpawn();
    }
    void ScheduleNextEnemySpawn()
    {
        float spawnInSeconds;

        if (maxSpawnRateInSeconds > 1f)
        {
            spawnInSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
            spawnInSeconds = 1f;

        Invoke("SpawnEnemy", spawnInSeconds);
    }

    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;
        if (maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    public void ScheduleSpawner()
    {
        maxSpawnRateInSeconds = 5f;

        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }


    public void UnscheduledSpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }*/
