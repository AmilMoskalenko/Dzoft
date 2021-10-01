using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ZombieDeath : MonoBehaviour
{
    public event Action<bool> Die = delegate { };
    public event Action<bool> Spawn = delegate { };

    [Header("Slider")]
    [SerializeField] private Slider slider;

    public bool isDying { get; set; }

    [Header("Local")]
    private Vector3 start_position;

    [Header("Const")]
    private const float fullHealth = 100f;
    private const float timeInstantiate = 3f;

    private void Awake()
    {
        isDying = false;
    }

    private void Start()
    {
        start_position = transform.position;
    }

    private void Death()
    {
        Die(isDying);
        StartCoroutine(Time_Instantiate());
    }

    IEnumerator Time_Instantiate()
    {
        yield return new WaitForSeconds(timeInstantiate);
        transform.position = start_position;
        Spawn(isDying);
        slider.value += fullHealth;
    }

    private void Update()
    {
        if (slider.value <= 0)
        {
            Death();
        }
    }
}
