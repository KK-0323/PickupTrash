using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4.0f;
    [SerializeField] private float rotateSpeed = 10.0f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private int playerIndex = 0;

    private Animator animator;
    private Rigidbody rb;
    private Vector2 inputVector;
    private TrashItem targetTrash;

    void Start()
    {
        Application.targetFrameRate = 60;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        SetGamepad();
    }

    public void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            if (cameraController != null)
            {
                cameraController.SetLookInput(Vector2.zero);
            }
            return;
        }

        if (cameraController != null)
        {
            cameraController.SetLookInput(value.Get<Vector2>());
        }
    }

    public void OnCollect(InputValue value)
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            return;
        }

        if (value.isPressed && targetTrash != null)
        {
            if(targetTrash.TryCollect())
            {
                targetTrash = null;
            }
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash") || other.GetComponent<InteractItem>() != null)
        {
            TrashItem trash = other.GetComponentInParent<TrashItem>();
            if (trash != null)
            {
                targetTrash = trash;
                trash.SetPlayerInRange(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trash") || other.GetComponent<InteractItem>() != null)
        {
            TrashItem trash = other.GetComponentInParent<TrashItem>();
            if(trash != null && trash == targetTrash)
            {
                trash.SetPlayerInRange(false);
                targetTrash = null;
            }
        }
    }

    private void SetGamepad()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            return;
        }

        if (!playerInput.user.valid)
        {
            return;
        }

        var gamepads = Gamepad.all;

        if (gamepads.Count > playerIndex)
        {
            // 自動切り替え無効化
            playerInput.neverAutoSwitchControlSchemes = true;

            // ペアリング
            playerInput.user.UnpairDevices();
            InputUser.PerformPairingWithDevice(gamepads[playerIndex], playerInput.user);
            Debug.Log($"Player{playerIndex + 1}にGamepad[{playerIndex}] ({gamepads[playerIndex].displayName})を割り当てました");
        }
        else
        {
            Debug.LogWarning($"Player{playerIndex + 1}: 対応するコントローラーが見つかりません (接続数: {gamepads.Count})");
        }
    }
}