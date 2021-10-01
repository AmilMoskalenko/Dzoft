using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public event Action DamageZombie = delegate { };
    public event Action DamageFastZombie = delegate { };
    public event Action ChooseWeapon = delegate { };
    public event Action Moving = delegate { };
    public event Action Fire = delegate { };
    public event Action Animation = delegate { };
    
    [Header("Death")]
    private Death death;
    private bool isDying;
    
    private void Awake()
    {
        death = GetComponent<Death>();
    }

    private void Update()
    {
        isDying = death.isDying;
        if (!isDying)
        {
            DamageZombie();
            DamageFastZombie();
            ChooseWeapon();
            Moving();
            Fire();
            Animation();
        }
    }
}
