using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System;

public class AnimationPlayer : MonoBehaviour
{
    public event Action LookAtShotPoint = delegate { };

    [Header("ParticleSystems")]
    private ParticleSystems particlesystems;
    private bool isExplode;

    [Header("Local")]
    private Animator animator;
    private NavMeshAgent agent;
    private bool check_throwing;
    private bool check_run_throw;
    private bool isRunning;
    private bool isShooting;
    private bool isThrowing;
    private bool run_throw;

    [Header("Const")]
    private const float timeThrowing = 2f;

    private void Awake()
    {
        GetComponent<Player>().Animation += Animation_Player;
        GetComponent<Fire>().Shooting += Shooting;
        GetComponent<Fire>().Throwing += Throwing;
        GetComponent<Death>().Die += Die;
        particlesystems = GetComponent<ParticleSystems>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Shooting()
    {
        if (Input.GetMouseButton(1))
        {
            isShooting = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isShooting = false;
        }
    }

    private void Throwing()
    {
        if (Input.GetMouseButton(1))
        {
            isThrowing = true;
            check_throwing = true;
            check_run_throw = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isThrowing = false;
        }
    }

    private void Die(bool isDying)
    {
        isRunning = false;
        isShooting = false;
        isThrowing = false;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isShooting", isShooting);
        animator.SetBool("isThrowing", isThrowing);
        animator.SetBool("isDying", isDying);
    }

    IEnumerator Time_Throwing()
    {
        yield return new WaitForSeconds(timeThrowing);
        check_throwing = false;
    }

    private void Animation_Player()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            isRunning = false;
        }
        if (agent.remainingDistance > agent.stoppingDistance && (!isExplode || check_run_throw))
        {
            isRunning = true;
            run_throw = false;
        }
        if (agent.remainingDistance > agent.stoppingDistance && (!isExplode || check_run_throw) && check_throwing)
        {
            StartCoroutine(Time_Throwing());
            LookAtShotPoint();
        }
        if (agent.remainingDistance > agent.stoppingDistance && isExplode && !check_run_throw)
        {
            check_run_throw = true;
            run_throw = true;
        }
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isShooting", isShooting);
        animator.SetBool("isThrowing", isThrowing);
        animator.SetBool("Run_Throw", run_throw);
    }

    private void Update()
    {
        isExplode = particlesystems.isExplode;
    }
}
