using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("制限時間")]
    [SerializeField] private float timeLimit = 180.0f;

    [Header("UI要素")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private float currentTimer;
    private int collectedCount = 0;
    public bool IsGameOver { get; private set; } = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentTimer = timeLimit;
        UpdateScoreUI();
    }

    void Update()
    {
        if (IsGameOver) return;

        // タイマー減算
        currentTimer -= Time.deltaTime;

        if(currentTimer <= 0)
        {
            currentTimer = 0;
            GameOver();
        }

        UpdateTimerUI();
    }

    public void AddScore(int amount = 1)
    {
        if (IsGameOver) return;

        collectedCount += amount;
        UpdateScoreUI();
    }

    private void UpdateTimerUI()
    {
        if (timerText == null) return;

        // 分：秒のフォーマットへ変換
        int minutes = Mathf.FloorToInt(currentTimer / 60F);
        int seconds = Mathf.FloorToInt(currentTimer % 60F);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateScoreUI()
    {
        if (scoreText == null) return;
        scoreText.text = "ゴミ回収数: " + collectedCount;
    }

    private void GameOver()
    {
        IsGameOver = true;
        Debug.Log("ゲーム終了！");
    }
}
