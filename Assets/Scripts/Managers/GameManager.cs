using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Game Time elapsed of the game, starting from 0
    [SerializeField] private float gameTime;
    private float timerCounter;

    public Action GameTimerEvent;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameTime = 0;
        timerCounter = 0;
    }


    private void Update()
    {
        gameTime += Time.deltaTime;
        timerCounter += Time.deltaTime;

        if(timerCounter >= 30f)
        {
            GameTimerEvent?.Invoke();
            timerCounter = 0;
        }
    }

    public void OnObstacleHitDestroyer(float obsHealth)
    {
        Debug.Log("Hit destroyer block");

        ScoreManager.Instance.ReduceFromScore(obsHealth);
    }

    public void GameOverState()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0;
    }


}
