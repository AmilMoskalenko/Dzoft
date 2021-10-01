using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Grenade")]
    [SerializeField] private GameObject grenade;

    [Header("ParticleSystems")]
    private ParticleSystems particlesystems;
    private bool isExplode;

    [Header("Local")]
    private bool isFlyingStart;
    private float flying;

    [Header("Const")]
    private const float timeGrenade = 1.9f;

    private void Awake()
    {
        GetComponent<Fire>().Grenade += Grenade_Weapon;
        particlesystems = GetComponent<ParticleSystems>();
    }

    IEnumerator Time_Grenade()
    {
        yield return new WaitForSeconds(timeGrenade);
        isFlyingStart = true;
    }

    private void Grenade_Weapon(Vector3 shot_point)
    {
        if (isExplode)
        {
            StartCoroutine(Time_Grenade());
        }
        if (isExplode && isFlyingStart)
        {
            flying += Time.deltaTime;
            flying = flying % 5f;
            grenade.transform.position = MathParabola.Parabola(transform.position + new Vector3(0f, 1.8f, 0.5f), shot_point, 3f, flying / 2f);
        }
    }

    private void Update()
    {
        isExplode = particlesystems.isExplode;
    }
}
