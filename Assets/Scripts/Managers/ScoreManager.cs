using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private float score;

    public Action OnScoreUpdateEvent;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public float GetScore()
    {
        return score;
    }

    public void AddToScore(float amount)
    {
        score += amount;
        OnScoreUpdateEvent?.Invoke();
    }

    public void ReduceFromScore(float amount)
    {
        score -= amount;

        if(score < 0)
        {
            //As for score = 0, we give player a chance to rebuild
            GameManager.instance.GameOverState();
        }
        //TODO: Change this to a new event/action to add shake animation on score value reduction
        OnScoreUpdateEvent?.Invoke();   
    }
}
