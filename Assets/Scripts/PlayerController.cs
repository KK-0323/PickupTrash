using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    //public float leftX = -2.0f;
    //public float rightX = 2.0f;

    void Start()
    {
        Application.targetFrameRate = 60;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // キーボード操作(デバッグ用)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bool isMoving = (horizontal != 0 || vertical != 0);

        animator.SetBool("IsWalking", isMoving);
        //if(Input.GetKeyDown(KeyCode.A))
        //{
        //    this.transform.Translate(leftX, 0.0f, 0.0f);
        //}
        //if(Input.GetKeyDown(KeyCode.D))
        //{
        //    this.transform.Translate(rightX, 0.0f, 0.0f);
        //}
    }
}
