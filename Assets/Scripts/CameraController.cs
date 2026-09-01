using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target; // 追従対象
    [SerializeField] private float lookSensitivity = 2.0f;
    [SerializeField] private float yMinLimit = -20f;
    [SerializeField] private float yMaxLimit = 60f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private Vector2 lookInput;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 初期角度をプレイヤーの向きにする
        if (target != null)
        {
            rotationX = target.eulerAngles.y;
        }
        else
        {
            rotationX = 0.0f;
        }

        rotationY = 0.0f;
    }

    public void SetLookInput(Vector2 input)
    {
        lookInput = input;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // スティックの移動量取得
        rotationX += lookInput.x * lookSensitivity;
        rotationY -= lookInput.y * lookSensitivity;
        rotationY = Mathf.Clamp(rotationY, yMinLimit, yMaxLimit);

        // カメラホルダーの回転と位置の設定
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        transform.rotation = rotation;
        transform.position = target.position;
    }
}
