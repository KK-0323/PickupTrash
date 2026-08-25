using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4.0f;
    [SerializeField] private float rotateSpeed = 10.0f;
    [SerializeField] private Transform cameraTransform;

    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        Application.targetFrameRate = 60;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputDir.magnitude >= 0.1f)
        {
            // カメラの前方・右方方向を取得
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;
            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            // 移動方向を計算
            Vector3 moveDirection = cameraForward * inputDir.z + cameraRight * inputDir.x;

            // キャラ回転
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

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
