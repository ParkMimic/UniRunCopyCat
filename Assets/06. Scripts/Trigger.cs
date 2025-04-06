using UnityEngine;

public class Trigger : MonoBehaviour
{
    void Start()
    {
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("Player"))
        {
            GameManager.instance.isClear = true;
        }
    }
}
