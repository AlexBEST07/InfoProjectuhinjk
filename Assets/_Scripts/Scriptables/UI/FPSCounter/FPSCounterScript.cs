using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounterScript : MonoBehaviour
{
    private float fps;
    private TMP_Text FPSCounter;
    
    void Awake() {
        FPSCounter = GetComponent<TMP_Text>();
    }

    void Start() {
        InvokeRepeating("ShowFPS", 1, 1);  
    }

    private void ShowFPS() {
        fps = (int)(1f / Time.unscaledDeltaTime);
        FPSCounter.text = "FPS: " + fps.ToString();
    }
}
