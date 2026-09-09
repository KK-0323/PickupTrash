using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController: MonoBehaviour
{
    [SerializeField] private string nextScene;

    public void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.LogWarning("遷移先のシーン名(nextScene)が設定されていません");
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
