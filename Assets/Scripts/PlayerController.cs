using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4.0f;
    [SerializeField] private float rotateSpeed = 10.0f;
    [SerializeField] private Transform cameraTransform;

    private Animator animator;
    private Rigidbody rb;
    private Vector2 inputVector;

    void Start()
    {
        Application.targetFrameRate = 60;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            animator.SetBool("IsWalking", false);
            return;
        }

        Vector3 inputDir = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        if (inputDir.magnitude >= 0.1f)
        {
            // カメラの前方・右方方向を取得
            Vector3 cameraForward = cameraTransform != null ? cameraTransform.forward : Vector3.forward;
            Vector3 cameraRight = cameraTransform != null ? cameraTransform.right : Vector3.right;
            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            // 移動方向を計算
            Vector3 moveDirection = cameraForward * inputDir.z + cameraRight * inputDir.x;

            // キャラ回転
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime);

            // Rigidbodyによる物理移動
            Vector3 nextPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(nextPosition);

            // 歩きアニメーション再生
            animator.SetBool("IsWalking", true);
        }
        else
        {
            // 静止アニメーション再生
            animator.SetBool("IsWalking", false);
        }
    }
}
