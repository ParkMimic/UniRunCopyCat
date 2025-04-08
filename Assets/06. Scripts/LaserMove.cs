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
        // 이동
        transform.Translate(XmoveSpeed * Time.deltaTime, YmoveSpeed * Time.deltaTime, 0);

        // 조건이 충족되었고, 아직 속도를 올리지 않았다면
        if (GameManager.instance.isClear == true && !isSpeedUp)
        {
            XmoveSpeed *= 5f;
            YmoveSpeed *= 5f;
            isSpeedUp = true;
        }

        // 도달하면 초기화
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