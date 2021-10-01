using UnityEngine;
using System;

public class Fire : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera main_camera;

    public event Action System_Fire = delegate { };
    public event Action<Vector3> System_Explosion = delegate { };
    public event Action Shooting = delegate { };
    public event Action Throwing = delegate { };
    public event Action<Vector3> Grenade = delegate { };

    [Header("Weapon")]
    private Weapon weapon;
    private int weapontype;

    [Header("ParticleSystems")]
    private ParticleSystems particlesystems;
    private bool isExplode;

    public Vector3 shot_point { get; set; }

    [Header("Local")]
    private Ray ray;
    private RaycastHit raycastHit;
    private Vector3 grenade_point;

    private void Awake()
    {
        GetComponent<Player>().Fire += Fire_Player;
        GetComponent<AnimationPlayer>().LookAtShotPoint += LookAtShotPoint;
        GetComponent<ParticleSystems>().LookAtShotPoint += LookAtShotPoint;
        weapon = GetComponent<Weapon>();
        particlesystems = GetComponent<ParticleSystems>();
        shot_point = new Vector3(0, 0, 0);
    }

    private void LookAtShotPoint()
    {
        transform.LookAt(shot_point);
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
    }

    private void Fire_Player()
    {
        if (Input.GetMouseButton(1))
        {
            ray = main_camera.ScreenPointToRay(Input.mousePosition);
        }
        if (Input.GetMouseButton(1) && Physics.Raycast(ray, out raycastHit))
        {
            shot_point = raycastHit.point;
        }
        if (Input.GetMouseButton(1) && Physics.Raycast(ray, out raycastHit) && weapontype == 1)
        {
            Shooting();
            System_Fire();
            LookAtShotPoint();
        }
        if (Input.GetMouseButton(1) && Physics.Raycast(ray, out raycastHit) && weapontype == 2 && !isExplode)
        {
            Throwing();
            System_Explosion(shot_point);
            grenade_point = shot_point;
            LookAtShotPoint();
        }
        if (Input.GetMouseButtonUp(1) && weapontype == 1)
        {
            Shooting();
            System_Fire();
        }
        if (Input.GetMouseButtonUp(1) && weapontype == 2)
        {
            Throwing();
        }
    }

    private void Update()
    {
        weapontype = weapon.weapontype;
        isExplode = particlesystems.isExplode;
        Grenade(grenade_point);
    }
}
