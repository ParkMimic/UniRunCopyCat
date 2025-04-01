using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 300f;
    public float dashSpeed = 10f;
    public float defaultTime = 0.5f;
    private float dashTime;
    private float defaultSpeed = 3f;

    private int jumpCount = 0;

    private bool isGrounded = false;
    private bool isWalking = false;
    private bool isFalling = false;
    private bool isDead = false;
    private bool isDash = false;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        // 캐릭터 기본 이동 구현
        if (Input.GetButton("Horizontal"))
        {
            rigid.linearVelocity = new Vector2(h * speed, rigid.linearVelocity.y);
            isWalking = true;

            // 이동 방향에 따른 뒤집기 구현
            spriteRenderer.flipX = h < 0;
        }
        else if (Input.GetButtonUp("Horizontal"))
        {
            rigid.linearVelocity = new Vector2(0, rigid.linearVelocity.y);
            isWalking = false;
        }

        // 대쉬 입력 처리
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTime <= 0)
        {
            isDash = true;
            dashTime = defaultTime;
        }

        // 대쉬 실행 및 종료 처리
        if (isDash)
        {
            speed = dashSpeed;
            dashTime -= Time.deltaTime;

            if (dashTime <= 0)
            {
                isDash = false;
                speed = defaultSpeed;
            }
        }

        // 점프 처리
        if (Input.GetKeyDown(KeyCode.Z) && jumpCount < 2)
        {
            jumpCount++;
            rigid.linearVelocity = Vector2.zero;
            rigid.AddForce(new Vector2(0, jumpForce));
        }
        else if (Input.GetKeyDown(KeyCode.Z) && rigid.linearVelocity.y > 0)
        {
            rigid.linearVelocity *= 0.5f;
        }

        // 낙하 상태 체크
        isFalling = rigid.linearVelocity.y < 0;

        // 애니메이션 설정
        anim.SetBool("isGround", isGrounded);
        anim.SetBool("isWalk", isWalking);
        anim.SetBool("isFall", isFalling);
        anim.SetBool("isDash", isDash); // isDash가 유지될 수 있도록 설정
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dead")
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        anim.SetTrigger("isDead");
        rigid.linearVelocity = Vector2.zero;
    }
}