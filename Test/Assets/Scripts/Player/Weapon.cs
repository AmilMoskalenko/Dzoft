using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("RawImages")]
    [SerializeField] private RawImage rawImage1;
    [SerializeField] private RawImage rawImage2;

    public int weapontype { get; set; }

    private void Awake()
    {
        GetComponent<Player>().ChooseWeapon += ChooseWeapon;
        weapontype = 1;
    }

    private void ChooseWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapontype = 1;
            rawImage1.color = new Color32(255, 255, 255, 255);
            rawImage2.color = new Color32(90, 90, 90, 255);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapontype = 2;
            rawImage2.color = new Color32(255, 255, 255, 255);
            rawImage1.color = new Color32(90, 90, 90, 255);
        }
    }
}
