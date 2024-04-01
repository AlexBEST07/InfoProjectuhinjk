using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    
    static public CameraManager Instance;
    
    private Camera _cam;

    private void Awake() {
        Instance = this;
        _cam = Camera.main;
    }

    public void ZoomIn(float duration, Action action) {
        StartCoroutine(Zoom(duration, action, 0.6f, 5));
    }
    public void ZoomOut(float duration, Action action) {
        StartCoroutine(Zoom(duration, action, 5f, 0.6f));
    }

    public IEnumerator Zoom(float duration, Action action, float targetSize, float initialSize) {
        float elapsedTime = 0f;

        yield return new WaitForSeconds(1.2f);

        while (elapsedTime < duration) {
            float t = elapsedTime / duration;
            Camera.main.orthographicSize = Mathf.Lerp(initialSize, targetSize, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.orthographicSize = targetSize;

        action();
    }
}
