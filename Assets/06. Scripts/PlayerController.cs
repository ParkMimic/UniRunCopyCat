using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 300f;
    public float dashSpeed = 10f;
    public float defaultTime = 0.5f;
    private float dashTime;
    private float gravity;

    private int jumpCount = 0;

    private bool isGrounded = false;
    private bool isWalking = false;
    private bool isFalling = false;
    private bool isDead = false;
    private bool isDash = false;
    private bool isJumping = false;
    private bool isGrap = false;

    private Vector2 lastMoveDirection = Vector2.right; // 기본 이동 방향 (오른쪽)
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    //  잔상 효과 관련 변수
    public GameObject ghostPrefab;
    public float ghostDelay = 1f;  // 잔상 생성 간격
    private float ghostDelayTime;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        ghostDelayTime = 0;  // 처음부터 바로 잔상이 생성되도록 수정!
        gravity = rigid.gravityScale;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        // 대시 중이 아닐 때만 이동 가능
        if (!isDash || !isGrap)
        {
            if (h != 0)
            {
                rigid.linearVelocity = new Vector2(h * speed, rigid.linearVelocity.y);
                isWalking = true;

                // 이동 방향 저장
                lastMoveDirection = new Vector2(h, 0).normalized;

                // 이동 방향에 따른 뒤집기
                spriteRenderer.flipX = h < 0;
            }
            else
            {
                isWalking = false;
                rigid.linearVelocity = new Vector2(0, rigid.linearVelocity.y); // 멈추기
            }
        }

        // 점프 중이 아닐 때에만 
        // 대쉬 입력 처리
        if (Input.GetKeyDown(KeyCode.X) && dashTime <= 0)
        {
            isDash = true;
            dashTime = defaultTime;

            // 대시 방향 설정 (입력이 없으면 마지막 방향 사용)
            Vector2 dashDirection = (h != 0) ? new Vector2(h, 0f).normalized : lastMoveDirection;

            // 중력을 0으로 처리 (대시 중에는 밑으로 떨어지지 않게!)
            rigid.gravityScale = 0;
            // 대시 실행
            rigid.linearVelocity = new Vector2(dashDirection.x * dashSpeed, 0f);
        }

        // 대쉬 실행 및 종료 처리
        if (isDash)
        {
            dashTime -= Time.deltaTime;
            MakeGhost();  // 대시 중에 잔상 생성

            if (dashTime <= 0)
            {
                isDash = false;
                rigid.gravityScale = gravity; // 대시가 끝나면 중력 다시 되돌려 주기
                rigid.linearVelocity = new Vector2(0, rigid.linearVelocity.y); // 대시 후 멈추기
            }
        }

        // 점프 처리
        if (Input.GetKeyDown(KeyCode.Z) && jumpCount < 2)
        {
            jumpCount++;
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, 0); // 기존 속도 초기화
            rigid.AddForce(new Vector2(0, jumpForce));
        }
        else if (Input.GetKeyDown(KeyCode.Z) && rigid.linearVelocity.y > 0)
        {
            rigid.linearVelocity *= new Vector2(1, 0.5f);
        }

        if (jumpCount == 2)
        {
            isJumping = true;
        }

        // 벽에 달라붙는 처리
        if (isGrap == true)
        {
            
            rigid.gravityScale = 0f;
            rigid.linearVelocity = Vector2.zero;
        }
        

        // 낙하 상태 체크
        isFalling = rigid.linearVelocity.y < 0;

        // 애니메이션 설정
        anim.SetBool("isGround", isGrounded);
        anim.SetBool("isWalk", isWalking);
        anim.SetBool("isFall", isFalling);
        anim.SetBool("isDash", isDash);
        anim.SetBool("isJump", isJumping);
        anim.SetBool("isWall", isGrap);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0f)
        {
            isGrounded = true;
            isJumping = false;
            jumpCount = 0;
        }

        if (collision.collider.tag == "Wall")
        {
            if (collision.contacts[0].normal.x > 0.7f || collision.contacts[0].normal.x < 0.7)
            {
                rigid.linearVelocity = lastMoveDirection;
                isGrap = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (jumpCount == 1)
        {
            isGrounded = false;
        }
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
        rigid.linearVelocity = Vector2.zero;
        anim.SetTrigger("isDead");
    }

    // 잔상 생성 함수
    void MakeGhost()
    {
        // 대시 중일 때만 잔상 생성
        if (!isDash) return;

        // 일정 시간이 지나야 잔상 생성
        ghostDelayTime -= Time.deltaTime;

        if (ghostDelayTime <= 0)
        {
            // 잔상 생성
            GameObject currentGhost = Instantiate(ghostPrefab, transform.position, transform.rotation);
            SpriteRenderer ghostSprite = currentGhost.GetComponent<SpriteRenderer>();

            // 플레이어의 현재 스프라이트 적용
            ghostSprite.sprite = spriteRenderer.sprite;

            // 플레이어의 방향(flipX) 복사
            ghostSprite.flipX = spriteRenderer.flipX;

            // 크기 유지
            currentGhost.transform.localScale = transform.localScale;

            // 잔상 일정 시간 후 삭제
            Destroy(currentGhost, 0.3f);

            // 잔상 생성 간격을 짧게 설정
            ghostDelayTime = 0.05f;  // 잔상을 더 자주 생성 (0.05초마다)
        }
    }
}