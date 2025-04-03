using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 따라갈 대상 (플레이어)
    public Vector3 offset; // 카메라와 플레이어 사이의 거리

    void Update()
    {
        if (target == null) return; // 대상이 없으면 실행 안 함

        transform.position = target.position + offset; // 플레이어 위치 + 오프셋
    }
}