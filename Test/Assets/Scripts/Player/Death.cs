using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField] private Slider slider;

    [Header("Const")]
    private const float timeDeath = 7f;

    public event Action<bool> Die = delegate { };

    public bool isDying { get; set; }

    private void Awake()
    {
        isDying = false;
    }

    IEnumerator Time_Death()
    {
        yield return new WaitForSeconds(timeDeath);
        Time.timeScale = 0;
    }

    private void Death_Player()
    {
        isDying = true;
        Die(isDying);
        StartCoroutine(Time_Death());
    }

    private void Update()
    {
        if (slider.value <= 0)
        {
            Death_Player();
        }
    }
}
