using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target; // 追従対象
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float yMinLimit = -20f;
    [SerializeField] private float yMaxLimit = 60f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rotationX = 0.0f;
        rotationY = 0.0f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null) return;

        // マウスの移動量取得
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationY = Mathf.Clamp(rotationY, yMinLimit, yMaxLimit);

        // カメラホルダーの回転と位置の設定
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        transform.rotation = rotation;
        transform.position = target.position;
    }
}
