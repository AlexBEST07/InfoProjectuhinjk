using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FramerateButtonScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            MainUIManager.Instance.OnFramerateButtonClick(1);
        }
        if (eventData.button == PointerEventData.InputButton.Right) {
            MainUIManager.Instance.OnFramerateButtonClick(-1);
            AudioSystem.Instance.PlaySound(Sound.ButtonClick);
        }
    }
}
