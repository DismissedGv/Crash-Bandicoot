using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    Vector3 offset;
    Vector3 currentVelocity = Vector3.zero;
    [SerializeField] Transform target;
    [SerializeField] float smoothTime;

    void Awake()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}
