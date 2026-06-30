using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 5f;      // 外（Inspector）から変えられるスピード
    public float rotateSpeed = 720f; // 振り向く速さ

    private Rigidbody rb;
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 前の設定で Is Kinematic を ON にした場合は、
        // 物理演算で動かすためにここで OFF にするか、MovePositionを使います。
        rb.isKinematic = false;
        // 勝手に転ばないように回転を固定
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        // WASDキー（または矢印キー）の入力を受け取る
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(h, 0, v).normalized;
    }

    void FixedUpdate()
    {
        // 移動の処理
        Move();
    }

    void Move()
    {
        // 入力がある場合のみ動かす
        if (moveInput.magnitude >= 0.1f)
        {
            // 移動
            Vector3 targetVelocity = moveInput * moveSpeed;
            rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z); // Unity 6では velocity ではなく linearVelocity

            // 入力方向に向く
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // 入力がないときは急ブレーキ（滑り防止）
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }
}