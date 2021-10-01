using UnityEngine;
using UnityEngine.AI;

public class ZombieMoving : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private float speed;

    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Local")]
    private NavMeshAgent agent;

    private void Awake()
    {
        GetComponent<Zombie>().Moving += Moving;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.speed = speed;
    }

    private void Moving()
    {
        agent.SetDestination(target.position);
    }
}
