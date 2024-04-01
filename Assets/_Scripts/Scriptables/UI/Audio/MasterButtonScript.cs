using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MasterButtonScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            MainUIManager.Instance.OnMasterButtonClick(1f);            
        }
        if (eventData.button == PointerEventData.InputButton.Right) {
            MainUIManager.Instance.OnMasterButtonClick(-1f);  
        }
    }
}
