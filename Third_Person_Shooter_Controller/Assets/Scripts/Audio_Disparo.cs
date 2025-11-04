using UnityEngine;

public class Audio_Disparo : MonoBehaviour
{
    [Header("Configuración de Audio")]
    public AudioSource audioSource;    // El componente AudioSource del personaje
    public AudioClip sonidoDisparo;    // El sonido que se reproducirá al disparar

    // 🔊 Método que reproduce el sonido del disparo
    public void Disparar()
    {
        if (audioSource == null)
        {
            Debug.LogWarning("⚠️ No hay AudioSource asignado en Audio_Disparo.");
            return;
        }

        if (sonidoDisparo == null)
        {
            Debug.LogWarning("⚠️ No hay AudioClip asignado en Audio_Disparo.");
            return;
        }

        // Reproduce el sonido una sola vez (sin necesidad de que esté en loop)
        audioSource.PlayOneShot(sonidoDisparo);
    }
}
