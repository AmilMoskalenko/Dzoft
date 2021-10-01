using UnityEngine;
using UnityEngine.AI;

public class Moving : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera main_camera;

    [Header("Local")]
    private Ray ray;
    private RaycastHit raycastHit;
    private NavMeshAgent agent;

    [Header("Const")]
    private const float speed = 2f;

    private void Awake()
    {
        GetComponent<Player>().Moving += Player_Moving;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.speed = speed;
    }

    private void Player_Moving()
    {
        if (Input.GetMouseButton(0))
        {
            ray = main_camera.ScreenPointToRay(Input.mousePosition);
        }
        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out raycastHit))
        {
            agent.SetDestination(raycastHit.point);
        }
    }
}
