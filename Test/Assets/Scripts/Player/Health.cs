using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField] private Slider slider;

    [Header("Local")]
    private bool isBiting;
    private bool isFastBiting;
    private bool isBiting_check;
    private bool isFastBiting_check;

    [Header("Const")]
    private const string nameZombie = "Zombie";
    private const string nameFastZombie = "FastZombie";
    private const float damage = 5f;
    private const float damageFast = 10f;

    private void Awake()
    {
        GetComponent<Player>().DamageZombie += DamageZombie;
        GetComponent<Player>().DamageFastZombie += DamageFastZombie;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(nameZombie))
        {
            isBiting = true;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer(nameFastZombie))
        {
            isFastBiting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(nameZombie))
        {
            isBiting = false;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer(nameFastZombie))
        {
            isFastBiting = false;
        }
    }

    IEnumerator Time_Damage()
    {
        while (isBiting)
        {
            slider.value -= damage;
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator Time_Damage_Fast()
    {
        while (isFastBiting)
        {
            slider.value -= damageFast;
            yield return new WaitForSeconds(2f);
        }
    }

    private void DamageZombie()
    {
        if (isBiting && !isBiting_check)
        {
            isBiting_check = true;
            StartCoroutine(Time_Damage());
        }
        if (!isBiting)
        {
            isBiting_check = false;
        }
    }

    private void DamageFastZombie()
    {
        if (isFastBiting && !isFastBiting_check)
        {
            isFastBiting_check = true;
            StartCoroutine(Time_Damage_Fast());
        }
        if (!isFastBiting)
        {
            isFastBiting_check = false;
        }
    }
}
