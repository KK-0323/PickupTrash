using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("制限時間")]
    [SerializeField] private float timeLimit = 180.0f;

    [Header("1P UI")]
    [SerializeField] private TextMeshProUGUI timerText1P;
    [SerializeField] private TextMeshProUGUI scoreText1P;

    [Header("2P UI")]
    [SerializeField] private TextMeshProUGUI timerText2P;
    [SerializeField] private TextMeshProUGUI scoreText2P;

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

        // サブモニターも使うようにする
        if(Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
        }
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
        // 分：秒のフォーマットへ変換
        int minutes = Mathf.FloorToInt(currentTimer / 60F);
        int seconds = Mathf.FloorToInt(currentTimer % 60F);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

        // テキスト更新
        if (timerText1P != null)
        {
            timerText1P.text = timeString;
        }
        if (timerText2P != null)
        {
            timerText2P.text = timeString;
        }
    }

    private void UpdateScoreUI()
    {
        
        string scoreString = "ゴミ回収数: " + collectedCount;

        if (scoreText1P != null)
        {
            scoreText1P.text = scoreString;
        }
        if (scoreText2P != null)
        {
            scoreText2P.text = scoreString;
        }
    }

    private void GameOver()
    {
        IsGameOver = true;
        Debug.Log("ゲーム終了！");

        StartCoroutine(ChangeToResult());
    }

    private IEnumerator ChangeToResult()
    {
        yield return new WaitForSeconds(2.0f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("ResultScene");
    }
}
