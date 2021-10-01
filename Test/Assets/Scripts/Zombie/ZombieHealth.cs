using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private float damage;

    [Header("Slider")]
    [SerializeField] private Slider slider;

    [Header("Weapon")]
    [SerializeField] private Weapon weapon;
    private int weapontype;

    [Header("Fire")]
    [SerializeField] private Fire fire;
    private Vector3 shot_point;

    [Header("Local")]
    private bool oneShot;

    [Header("Const")]
    private const float damageExplosion = 20f;
    private const float timeExplosion = 4f;
    private const float radiusRifle = 1f;
    private const float radiusGrenade = 3f;

    private void Awake()
    {
        GetComponent<Zombie>().Damage += Damage;
    }

    IEnumerator Time_Explosion()
    {
        yield return new WaitForSeconds(timeExplosion);
        oneShot = false;
        slider.value -= damageExplosion;
    }

    private void Damage()
    {
        if (Input.GetMouseButton(1) && weapontype == 1)
        {
            DamageRifle();
        }
        if (Input.GetMouseButton(1) && weapontype == 2 && !oneShot)
        {
            DamageGrenade();
        }
    }

    private void DamageRifle()
    {
        if (shot_point.x > transform.position.x - radiusRifle && shot_point.x < transform.position.x + radiusRifle)
        {
            if (shot_point.z > transform.position.z - radiusRifle && shot_point.z < transform.position.z + radiusRifle)
            {
                slider.value -= damage;
            }
        }
    }

    private void DamageGrenade()
    {
        if (shot_point.x > transform.position.x - radiusGrenade && shot_point.x < transform.position.x + radiusGrenade)
        {
            if (shot_point.z > transform.position.z - radiusGrenade && shot_point.z < transform.position.z + radiusGrenade)
            {
                oneShot = true;
                StartCoroutine(Time_Explosion());
            }
        }
    }

    private void Update()
    {
        weapontype = weapon.weapontype;
        shot_point = fire.shot_point;
    }
}
