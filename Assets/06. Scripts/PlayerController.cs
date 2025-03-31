using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 300f;
    private int jumpCount = 0;

    private bool isGrounded = false;

    private Rigidbody2D rigid;
    private Animator anim;

    void Start()
    {
        // 게임 오브젝트에서 Rigidbody2D를 찾아 플레이어에게 할당
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Horizontal"))
        {
            rigid.linearVelocity = new Vector2(h * speed, rigid.linearVelocity.y);
        }

        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            jumpCount++;
            // 점프 직전에 속도를 순간적으로 제로(0,0)로 변경
            rigid.linearVelocity = Vector2.zero;
            rigid.AddForce(new Vector2(0, jumpForce));
        }
        else if (Input.GetButtonUp("Jump") && rigid.linearVelocity.y > 0)
        {
            // 마우스 왼쪽 버튼에서 손을 떼는 순간 && 속도의 y 값이 양수라면(위로 상승 중)
            // 현재 속도를 절반으로 변경
            rigid.linearVelocity = rigid.linearVelocity * 0.5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿았음을 감지하는 처리
        // 어떤 콜라이더와 닿았으며, 충돌 표면이 위쪽을 보고 있으면
        if (collision.contacts[0].normal.y > 0.7f)
        {
            //isGrounded를 true로 변경하고, 누적 점프 횟수를 0으로 리셋
            isGrounded = true;
            jumpCount = 0;
        }
    }
}
