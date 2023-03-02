using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public Obstacle spawnerPrefab;

    public List<GameObject> spawnLocations;
    [SerializeField] private int counter;   //Used to count the current index of locations to spawn at

    private Player player;
    private float playerDamage;
    private float playerFireRate;

    private float objectHealth; //Average Health of the object to be spawned
    private float objectSpeed;  //Movement speed of the obstacle
    [SerializeField] private float maxObjectSpeed;

    [SerializeField] private float objectSpawnCounter;
    private float objectSpawnTimer; //The difficulty curve value for spawning items

    [SerializeField] private float difficultyQuotient;
    [SerializeField] private float maxDifficultyQuotient;

    [SerializeField] private ScoreManager scoreManager;

    [SerializeField] private GameManager gameManager;

    //Test Action || TOBEREMOVED
    public Action OnObjectSpeedUpdateEvent;

   
    private void Start()
    {
        player = Player.Instance;
        scoreManager = ScoreManager.Instance;
        gameManager = GameManager.instance;

        if (player)
        {
            player.fireRateUpdateEvent += OnFireRateUpdateHandler;
            player.playerDamageUpdateEvent += OnDamageUpdateHandler;

            SpawnedObjectHealth();
        }

        if (scoreManager)
        {
            scoreManager.OnScoreUpdateEvent += OnScoreUpdateEventHandler;
        }

        if (gameManager)
        {
            objectSpeed = 3.5f;
            gameManager.GameTimerEvent += OnGameTimerUpdateEventHandler;
        }

        objectSpawnTimer = 5f;
    }

    private void Update()
    {
        if(objectSpawnCounter >= objectSpawnTimer)
        {
            SpawnObstacle();
        }

        objectSpawnCounter += Time.deltaTime;
    }

    private void OnDisable()
    {
        player.fireRateUpdateEvent -= OnFireRateUpdateHandler;
        player.playerDamageUpdateEvent -= OnDamageUpdateHandler;
    }

    private void OnFireRateUpdateHandler()
    {
        Debug.Log("Spawner: Fire Rate Updated");
       
        SpawnedObjectHealth();
    }

    private void OnDamageUpdateHandler()
    {
        Debug.Log("Spawner: Damage Updated");
        
        SpawnedObjectHealth();
    }

    private void SpawnedObjectHealth()
    {
        //This function will assign the health of the spawn object
        //For testing purpose, all objects currently would be spawned with same health
        //Formula: 3 * damage/ fire rate

        playerFireRate = player.fireRate;
        playerDamage = player.playerDamage;

        objectHealth = difficultyQuotient * playerDamage / playerFireRate;

        objectHealth = Mathf.Round(objectHealth);
    } 

    private void SpawnObstacle()
    {
        Obstacle ob = Instantiate(spawnerPrefab,
            spawnLocations[counter].transform.position,
            Quaternion.identity);

        ob.SetHealth(objectHealth,playerDamage,difficultyQuotient);
        objectSpawnCounter = 0;
        counter++;

        if (counter >= spawnLocations.Count)
        {
            counter = 0;
        }
    }

    private void OnScoreUpdateEventHandler()
    {
        if (difficultyQuotient >= maxDifficultyQuotient) return;

        difficultyQuotient += 0.01f;
    }

    private void OnGameTimerUpdateEventHandler()
    {
        Debug.Log($"Timer update increase game speed");

        objectSpeed += 0.10f;

        if(objectSpeed >= maxObjectSpeed)
            objectSpeed = maxObjectSpeed;
        else
            OnObjectSpeedUpdateEvent?.Invoke();
    }

    public float GetObjectSpeed()
    {
        return objectSpeed;
    }
}
