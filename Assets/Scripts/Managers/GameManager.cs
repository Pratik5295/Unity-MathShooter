using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private string gameScene;
    [SerializeField] private string menuScene;

    //Game States
    public enum GameState
    {
        MENU = 0,
        PLAY = 1,
        GAMEOVER = 2
    }

    //Game Time elapsed of the game, starting from 0
    [SerializeField] private float gameTime;
    private float timerCounter;

    [SerializeField] private GameState State;
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
        DontDestroyOnLoad(gameObject); 
    }

    private void Start()
    {
        gameTime = 0;
        timerCounter = 0;
        State = GameState.MENU;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void Update()
    {
        if (State == GameState.PLAY)
        {
            gameTime += Time.deltaTime;
            timerCounter += Time.deltaTime;

            if (timerCounter >= 30f)
            {
                GameTimerEvent?.Invoke();
                timerCounter = 0;
            }
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
        State = GameState.GAMEOVER;

        LoadMenuScene();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameScene);
  
    }

    public void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        Debug.Log("New Scene Loaded");
        if (string.Equals(scene.name, gameScene))
        {
            //Game Scene has been loaded

            Debug.Log($"Game Scene is loaded");
            State = GameState.PLAY;
            ScoreManager.Instance.ResetScore();
        }

        else if (string.Equals(scene.name, menuScene))
        {
            //Menu scene has been loaded

            Debug.Log($"Menu Scene is loaded");
            State = GameState.MENU;
        }
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(menuScene);
    }
}
