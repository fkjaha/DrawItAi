using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSound : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SoundPresetBase clickSound;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SoundsPlayer.Instance == null) return;
        SoundsPlayer.Instance.PlaySound(clickSound);
    }
}