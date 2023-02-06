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

    public void AddToScore(float amount)
    {
        score += amount;
        OnScoreUpdateEvent?.Invoke();
    }
}
