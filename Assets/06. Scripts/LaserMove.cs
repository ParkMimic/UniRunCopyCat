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
        if (GameManager.instance.levelCount == 1)
        {
            if (GameManager.instance.isClear == true && !isSpeedUp)
            {
                XmoveSpeed *= 2f;
                YmoveSpeed *= 2f;
                isSpeedUp = true;
            }

            // �����ϸ� �ʱ�ȭ
            if (transform.position.x >= 28f)
            {
                GameManager.instance.isClear = false;
                GameManager.instance.levelCount += 1;
                gameObject.SetActive(false);
            }
        }
    }
}