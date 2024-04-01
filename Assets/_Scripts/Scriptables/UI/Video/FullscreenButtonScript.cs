using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FullscreenButtonScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) {
        if(eventData.button == PointerEventData.InputButton.Left) {
            MainUIManager.Instance.OnFullscreenButtonClick();
        }
        if(eventData.button == PointerEventData.InputButton.Right) {
            MainUIManager.Instance.OnFullscreenButtonClick();
            AudioSystem.Instance.PlaySound(Sound.ButtonClick);
        }
    }
}
