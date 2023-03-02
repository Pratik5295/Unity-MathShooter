using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI playerDamageText;
    [SerializeField] private TextMeshProUGUI playerFireRateText;
    [SerializeField] private TextMeshProUGUI objectSpeedText;

    [SerializeField] private ScoreManager scoreManager;
    private Player player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Spawner spawner;

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
        player = Player.Instance;
        scoreManager = ScoreManager.Instance;
        gameManager = GameManager.instance;

        if (player)
        {
            player.fireRateUpdateEvent += OnFireRateUpdateHandler;
            player.playerDamageUpdateEvent += OnDamageUpdateHandler;
            OnDamageUpdateHandler();
        }
        if (scoreManager)
        {
            scoreManager.OnScoreUpdateEvent += OnScoreUpdateEventHandler;
            OnScoreUpdateEventHandler();
        }
        if (gameManager)
        {
            gameManager.GameTimerEvent += OnGameTimerUpdateEventHandler;
        }
        if (spawner)
        {
            spawner.OnObjectSpeedUpdateEvent += OnObjectSpeedUpdateEventHandler;
            OnObjectSpeedUpdateEventHandler();
        }
    }

    private void Update()
    {
        if (player)
        {
            OnFireRateUpdateHandler();
        }
    }

    private void OnFireRateUpdateHandler()
    {
        playerFireRateText.text = player.fireRate.ToString();
    }

    private void OnDamageUpdateHandler()
    {
        playerDamageText.text = player.playerDamage.ToString();
    }

    private void OnScoreUpdateEventHandler()
    {
        scoreText.text = scoreManager.GetScore().ToString();
    }

    private void OnGameTimerUpdateEventHandler()
    {

    }

    private void OnObjectSpeedUpdateEventHandler()
    {
        objectSpeedText.text = spawner.GetObjectSpeed().ToString();
    }

}
