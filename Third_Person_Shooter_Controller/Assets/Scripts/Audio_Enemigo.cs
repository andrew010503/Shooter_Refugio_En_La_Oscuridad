using UnityEngine;

public class Audio_Enemigo : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sonidoAtaque;
    public AudioClip sonidoMuerte;
    public AudioClip sonidoIdle;

    public void ReproducirAtaque()
    {
        if (sonidoAtaque != null)
            audioSource.PlayOneShot(sonidoAtaque);
    }

    public void ReproducirMuerte()
    {
        if (sonidoMuerte != null)
            audioSource.PlayOneShot(sonidoMuerte);
    }

    public void ReproducirIdle()
    {
        if (sonidoIdle != null && !audioSource.isPlaying)
            audioSource.PlayOneShot(sonidoIdle);
    }
}
