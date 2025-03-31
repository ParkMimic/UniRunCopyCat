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
        // ���� ������Ʈ���� Rigidbody2D�� ã�� �÷��̾�� �Ҵ�
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
            // ���� ������ �ӵ��� ���������� ����(0,0)�� ����
            rigid.linearVelocity = Vector2.zero;
            rigid.AddForce(new Vector2(0, jumpForce));
        }
        else if (Input.GetButtonUp("Jump") && rigid.linearVelocity.y > 0)
        {
            // ���콺 ���� ��ư���� ���� ���� ���� && �ӵ��� y ���� ������(���� ��� ��)
            // ���� �ӵ��� �������� ����
            rigid.linearVelocity = rigid.linearVelocity * 0.5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴڿ� ������� �����ϴ� ó��
        // � �ݶ��̴��� �������, �浹 ǥ���� ������ ���� ������
        if (collision.contacts[0].normal.y > 0.7f)
        {
            //isGrounded�� true�� �����ϰ�, ���� ���� Ƚ���� 0���� ����
            isGrounded = true;
            jumpCount = 0;
        }
    }
}
