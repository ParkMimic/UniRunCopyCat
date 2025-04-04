using UnityEngine;

public class LaserMove : MonoBehaviour
{
    public float XmoveSpeed;
    public float YmoveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(XmoveSpeed * Time.deltaTime, YmoveSpeed * Time.deltaTime, 0);
        if (GameManager.instance.levelCount == 1)
        {
            if (transform.position.x >= 28f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
