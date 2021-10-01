using UnityEngine;
using System;

public class Zombie : MonoBehaviour
{
    public event Action Moving = delegate { };
    public event Action Animation = delegate { };
    public event Action Damage = delegate { };

    [Header("ZombieDeath")]
    private ZombieDeath death;
    private bool isDying;

    private void Awake()
    {
        death = GetComponent<ZombieDeath>();
    }

    private void Update()
    {
        isDying = death.isDying;
        if (!isDying)
        {
            Moving();
            Animation();
            Damage();
        }
    }
}
