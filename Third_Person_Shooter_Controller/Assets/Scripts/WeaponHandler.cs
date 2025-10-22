using UnityEngine;
using StarterAssets;
using System.Collections;
using UnityEngine.Animations.Rigging;

public class WeaponHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraFollowTarget;
    private Animator anim;
    private ThirdPersonController controller;

    [Header("Shooting")]
    [SerializeField] private float fireRate = 0.09f;
    [SerializeField] private float shootBlendTime = 0.075f;
    [SerializeField] private string shootStateName = "Fire_Rifle";
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private ParticleSystem muzzleFlash;
    private bool canShoot = true;

    [Header("Projectile Settings")]
    [SerializeField] private Transform muzzleSocket;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 40f;

    [Header("Aiming")]
    [SerializeField] private float cameraTransitionSpeed = 7f;
    [SerializeField] private float ikTransitionSpeed = 10f;
    [SerializeField] private MultiAimConstraint aimIK;
    [SerializeField] private Vector3 aimOffset = new Vector3(0.5f, 1.5f, -0.5f);
    private Vector3 defaultLocalPos;

    public bool Aiming { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject crosshair;
    [SerializeField] private float crosshairScaleSpeed = 8f;
    [SerializeField] private float aimingCrosshairScale = 0.8f;
    private Vector3 defaultCrosshairScale;

    private void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<ThirdPersonController>();

        if (cameraFollowTarget != null)
            defaultLocalPos = cameraFollowTarget.localPosition;

        if (crosshair != null)
        {
            crosshair.SetActive(true);
            defaultCrosshairScale = crosshair.transform.localScale;
        }
    }

    private void Update()
    {
        if (cameraFollowTarget == null || anim == null || controller == null) return;

        Aiming = Input.GetButton("Fire2");
        bool shootInp = Input.GetButton("Fire1");

        anim.SetBool("Aiming", Aiming);
        controller.Strafe = Aiming; // 👈 Ahora funciona

        Vector3 targetPos = Aiming ? aimOffset : defaultLocalPos;
        cameraFollowTarget.localPosition = Vector3.Lerp(
            cameraFollowTarget.localPosition,
            targetPos,
            cameraTransitionSpeed * Time.deltaTime
        );

        if (crosshair != null)
        {
            crosshair.SetActive(true);
            Vector3 targetScale = Aiming
                ? defaultCrosshairScale * aimingCrosshairScale
                : defaultCrosshairScale;

            crosshair.transform.localScale = Vector3.Lerp(
                crosshair.transform.localScale,
                targetScale,
                Time.deltaTime * crosshairScaleSpeed
            );
        }

        if (aimIK != null)
        {
            float targetWeight = Aiming ? 1f : 0f;
            aimIK.weight = Mathf.Lerp(aimIK.weight, targetWeight, ikTransitionSpeed * Time.deltaTime);
        }

        if (shootInp && canShoot)
        {
            if (!Aiming) Aiming = true;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!canShoot) return;

        if (shootSound != null)
            AudioSource.PlayClipAtPoint(shootSound, transform.position);

        if (muzzleFlash != null)
            muzzleFlash.Play();

        if (projectilePrefab != null && muzzleSocket != null)
        {
            GameObject projGO = Instantiate(projectilePrefab, muzzleSocket.position, muzzleSocket.rotation);
            projGO.AddComponent<ProjectileMover>().Initialize(muzzleSocket.forward, projectileSpeed);
        }

        anim.CrossFadeInFixedTime(shootStateName, shootBlendTime);
        StartCoroutine(ResetFireRate());
    }

    private IEnumerator ResetFireRate()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}

public class ProjectileMover : MonoBehaviour
{
    private Vector3 direction;
    private float speed;

    public void Initialize(Vector3 dir, float spd)
    {
        direction = dir.normalized;
        speed = spd;
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;

        EnemyHealth enemy = other.GetComponentInParent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(25f);
        }

        Destroy(gameObject);
    }
}
