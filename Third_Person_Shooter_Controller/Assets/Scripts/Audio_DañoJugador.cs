using UnityEngine;

public class Audio_DañoJugador : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sonidoDaño;

    public void ReproducirDaño()
    {
        if (audioSource != null && sonidoDaño != null)
        {
            audioSource.PlayOneShot(sonidoDaño);
        }
    }
}
