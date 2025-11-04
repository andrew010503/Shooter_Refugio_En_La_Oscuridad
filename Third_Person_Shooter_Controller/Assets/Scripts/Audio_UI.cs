using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Audio_UI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioSource audioSource;
    public AudioClip sonidoHover;
    public AudioClip sonidoClick;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (sonidoHover != null)
            audioSource.PlayOneShot(sonidoHover);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (sonidoClick != null)
            audioSource.PlayOneShot(sonidoClick);
    }
}
