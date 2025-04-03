using UnityEngine;

public class LaserMoveX : MonoBehaviour
{
    public float moveSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
    }
}
