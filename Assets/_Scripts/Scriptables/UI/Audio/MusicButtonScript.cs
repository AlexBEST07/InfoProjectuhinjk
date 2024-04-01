using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MusicButtonScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            MainUIManager.Instance.OnMusicButtonClick(1f);
        }
        if (eventData.button == PointerEventData.InputButton.Right) {
            MainUIManager.Instance.OnMusicButtonClick(-1f);
        }
    }
}
