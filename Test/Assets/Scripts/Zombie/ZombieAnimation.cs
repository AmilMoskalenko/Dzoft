using UnityEngine;
using UnityEngine.AI;

public class ZombieAnimation : MonoBehaviour
{
    [Header("Local")]
    private NavMeshAgent agent;
    private Animator animator;
    private bool isBiting;

    [Header("Const")]
    private const float distance = 1f;

    private void Awake()
    {
        GetComponent<Zombie>().Animation += Animation;
        GetComponent<ZombieDeath>().Die += Die;
        GetComponent<ZombieDeath>().Spawn += Spawn;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Die(bool isDying)
    {
        agent.isStopped = true;
        isDying = true;
        isBiting = false;
        animator.SetBool("isBiting", isBiting);
        animator.SetBool("isDying", isDying);
    }

    private void Spawn(bool isDying)
    {
        isDying = false;
        animator.SetBool("isDying", isDying);
        agent.isStopped = false;
    }

    private void Animation()
    {
        if (agent.remainingDistance > 0 && agent.remainingDistance <= distance)
        {
            isBiting = true;
        }
        if (agent.remainingDistance > 0 && agent.remainingDistance > distance)
        {
            isBiting = false;
        }
        animator.SetBool("isBiting", isBiting);
    }
}
