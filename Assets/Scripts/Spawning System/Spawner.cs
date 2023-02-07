using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public GameObject spawnerPrefab;

    public List<GameObject> spawnLocations;
    [SerializeField] private int counter;   //Used to count the current index of locations to spawn at

    private Player player;
    private float playerDamage;
    private float playerFireRate;

    private void Start()
    {
        player = Player.Instance;

        player.fireRateUpdateEvent += OnFireRateUpdateHandler;
        player.playerDamageUpdateEvent += OnDamageUpdateHandler;
    }

    private void OnDisable()
    {
        player.fireRateUpdateEvent -= OnFireRateUpdateHandler;
        player.playerDamageUpdateEvent -= OnDamageUpdateHandler;
    }

    private void OnFireRateUpdateHandler()
    {
        playerFireRate = player.fireRate;
    }

    private void OnDamageUpdateHandler()
    {
        playerDamage = player.playerDamage;
    }

    private void SpawnedObjectHealth()
    {
        //This function will assign the health of the spawn object
        //For testing purpose, all objects currently would be spawned with same health
        //Formula: 3 * damage/ fire rate


    } 
}
