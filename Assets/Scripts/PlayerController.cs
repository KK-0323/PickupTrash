using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float leftX = -2.0f;
    public float rightX = 2.0f;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // キーボード操作(デバッグ用)
        if(Input.GetKeyDown(KeyCode.A))
        {
            this.transform.Translate(leftX, 0.0f, 0.0f);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            this.transform.Translate(rightX, 0.0f, 0.0f);
        }
    }
}
