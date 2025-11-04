using UnityEngine;

public class Audio_Pasos : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] pasos;  // varios sonidos diferentes para no sonar repetitivo
    public float pasoIntervalo = 0.5f; // tiempo entre pasos
    private float tiempoSiguientePaso;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        tiempoSiguientePaso = 0f;
    }

    void Update()
    {
        // Solo suena cuando el personaje se mueve y está en el suelo
        if (characterController != null && characterController.isGrounded && characterController.velocity.magnitude > 0.1f)
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
}
