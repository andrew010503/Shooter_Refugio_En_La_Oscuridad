using UnityEngine; // Esto siempre debe estar al principio

public class Audio_Disparo : MonoBehaviour
{
    // Asignaremos esto desde el Inspector
    public AudioSource audioSource;
    public AudioClip disparoClip;

    // Método para reproducir el sonido
    public void Disparar()
    {
        if (audioSource != null && disparoClip != null)
        {
            audioSource.PlayOneShot(disparoClip); // Reproduce el clip
        }
        else
        {
            Debug.LogWarning("AudioSource o AudioClip no asignado");
        }
    }
}
