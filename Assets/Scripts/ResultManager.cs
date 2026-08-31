using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultTitleText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    void Start()
    {
        if (GameManager.IsGameCleared)
        {
            if(resultTitleText != null)
            {
                resultTitleText.text = "STAGE CLEAR!!";
            }
        }
        else
        {
            if(resultTitleText != null)
            {
                resultTitleText.text = "GAME OVER...";
            }
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = "最終スコア: " + GameManager.FinalScore;
        }
    }
}
