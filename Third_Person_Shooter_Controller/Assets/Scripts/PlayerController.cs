using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    private CharacterController controller;

    [Header("Disparo")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public int ammo = 10;
    public Transform cameraTransform;

    [Header("Sonidos")]
    public AudioSource audioSource;
    public AudioClip[] pasos; // sonidos de pasos
    public float pasoIntervalo = 0.5f;
    private float tiempoSiguientePaso;

    public AudioClip sonidoDisparo;
    public AudioClip sonidoDaño; // para usar cuando el enemigo golpee al jugador

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        tiempoSiguientePaso = 0f;
    }

    void Update()
    {
        Movimiento();
        Disparo();
        SonidoPasos();
    }

    // -------------------- MOVIMIENTO --------------------
    void Movimiento()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * moveZ + right * moveX).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        // Rotación del jugador igual a la cámara (solo eje Y)
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;

        if (cameraForward != Vector3.zero)
        {
            Quaternion cameraRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, cameraRotation, Time.deltaTime * 10f);
        }
    }

    // -------------------- DISPARO --------------------
    void Disparo()
    {
        if (Input.GetButtonDown("Fire1") && ammo > 0)
        {
            if (projectilePrefab != null && firePoint != null)
            {
                Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            }

            // 🔊 Reproducir sonido de disparo
            if (sonidoDisparo != null && audioSource != null)
            {
                audioSource.PlayOneShot(sonidoDisparo);
            }

            ammo--;
        }
    }

    // -------------------- SONIDO DE PASOS --------------------
    void SonidoPasos()
    {
        // Si el jugador se mueve y está en el suelo
        if (controller.isGrounded && controller.velocity.magnitude > 0.2f)
        {
            if (Time.time > tiempoSiguientePaso)
            {
                ReproducirPaso();
                tiempoSiguientePaso = Time.time + pasoIntervalo;
            }
        }
    }

    void ReproducirPaso()
    {
        if (pasos.Length > 0 && audioSource != null)
        {
            int index = Random.Range(0, pasos.Length);
            audioSource.PlayOneShot(pasos[index]);
        }
    }

    // -------------------- SONIDO DE DAÑO --------------------
    public void ReproducirDaño()
    {
        if (sonidoDaño != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoDaño);
        }
    }

    // -------------------- RECARGA DE MUNICIÓN --------------------
    public void CollectAmmo(int amount)
    {
        ammo += amount;
    }
}
