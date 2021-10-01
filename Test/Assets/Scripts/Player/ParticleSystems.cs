using System.Collections;
using UnityEngine;
using System;

public class ParticleSystems : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private GameObject system_Shooting;
    [SerializeField] private GameObject system_Explosion;

    [Header("Const")]
    private const float timeExplosion = 5f;

    public event Action LookAtShotPoint = delegate { };

    public bool isExplode { get; set; }

    private void Awake()
    {
        GetComponent<Fire>().System_Fire += System_Fire;
        GetComponent<Fire>().System_Explosion += System_Explosion;
        isExplode = false;
    }

    private void Start()
    {
        system_Shooting.SetActive(false);
        system_Explosion.SetActive(false);
    }

    IEnumerator Time_Explosion()
    {
        LookAtShotPoint();
        yield return new WaitForSeconds(timeExplosion);
        isExplode = false;
        system_Explosion.SetActive(false);
    }

    private void System_Fire()
    {
        if (Input.GetMouseButton(1))
        {
            system_Shooting.SetActive(true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            system_Shooting.SetActive(false);
        }
    }

    private void System_Explosion(Vector3 shot_point)
    {
        system_Explosion.SetActive(true);
        system_Explosion.transform.position = shot_point;
        system_Explosion.GetComponent<ParticleSystem>().Play();
        isExplode = true;
        StartCoroutine(Time_Explosion());
    }
}
