using Unity.VisualScripting;
using UnityEngine;

public class LaserMove : MonoBehaviour
{
    public float XmoveSpeed;
    public float YmoveSpeed;

    private float XmoveDef;
    private float YmoveDef;

    private bool isSpeedUp = false;

    private void Start()
    {
        XmoveDef = XmoveSpeed;
        YmoveDef = YmoveSpeed;
    }

    void Update()
    {
        // �̵�
        transform.Translate(XmoveSpeed * Time.deltaTime, YmoveSpeed * Time.deltaTime, 0);

        // ������ �����Ǿ���, ���� �ӵ��� �ø��� �ʾҴٸ�
        if (GameManager.instance.isClear == true && !isSpeedUp)
        {
            XmoveSpeed *= 5f;
            YmoveSpeed *= 5f;
            isSpeedUp = true;
        }

        // �����ϸ� �ʱ�ȭ
        if (transform.position.x >= 28f)
        {
            GameManager.instance.isClear = false;
            GameManager.instance.OnTrigger();
            gameObject.SetActive(false);
        }
        else if (transform.position.x <= -30f)
        {
            GameManager.instance.isClear = false;
            GameManager.instance.OnTrigger();
            gameObject.SetActive(false);
        }
        else if (transform.position.y <= -5f)
        {
            GameManager.instance.isClear = false;
            GameManager.instance.OnTrigger();
            gameObject.SetActive(false);
        }
        else if (transform.position.y >= 21f)
        {
            GameManager.instance.isClear = false;
            GameManager.instance.OnTrigger();
            gameObject.SetActive(false);
        }
    }
}