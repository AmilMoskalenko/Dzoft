using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    private Vector3 offset;

    private void Start()
    {
        offset = target.position - transform.position;
    }

    private void LateUpdate()
    {
        transform.position = target.position - offset;
    }
}
