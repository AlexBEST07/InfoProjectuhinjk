using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    #region Setup

    public static HUDManager Instance;

    void Awake() {
        Instance = this;       
        Cursor.visible = false;
    }

    #endregion

    #region HUDControl

    [Header("HUD")]

    [SerializeField] private Canvas hudCanvas; 

    public void ShowHUD() {
        hudCanvas.enabled = true;
    }


    #region Crosshair

    [Header("Crosshair")]

    public Image crosshair;

    [SerializeField] private GameObject scopeCanvas;
    [SerializeField] private GameObject backgroundCanvas;
    [SerializeField] private GameObject scopeImage;
    [SerializeField] private GameObject scopeCrosshairImage;

    [HideInInspector]
    public bool scopeActivated = false;

    void Update() {
        if (!scopeCanvas.GetComponent<Canvas>().enabled) crosshair.transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        if (scopeCanvas.GetComponent<Canvas>().enabled) {
            scopeImage.transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            scopeCrosshairImage.transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        }
    }

    public void ManageScope() {
        crosshair.GetComponent<Image>().enabled = !crosshair.GetComponent<Image>().enabled;
        scopeCanvas.GetComponent<Canvas>().enabled = !scopeCanvas.GetComponent<Canvas>().enabled;
        backgroundCanvas.GetComponent<Canvas>().enabled = !backgroundCanvas.GetComponent<Canvas>().enabled;
        scopeActivated = !scopeActivated;
    }

    #endregion

    #region Score

    [Header("Score")]

    [SerializeField] private TMP_Text _scoreText;
    private float _score;

    public void AddScore(float points) {
        _score += points;
        _scoreText.text = _score.ToString();
    }

    #endregion

    #region Countdown 

    [Header("Countdown")]

    [SerializeField] private TMP_Text _countdownText;

    public IEnumerator Countdown(float time, Action action) {

        _countdownText.GetComponent<TMP_Text>().enabled = true;

        while (time >= 0) {
            _countdownText.text = time.ToString();
            yield return new WaitForSeconds(1);
            time = time - 1f;
        }      
        _countdownText.GetComponent<TMP_Text>().enabled = false;

        action();
    }

    #endregion

    #region Ammo

    [Header("Ammo")]

    [SerializeField] private TMP_Text _ammoText;

    public void UpdateAmmo(float currentAmmo, float maxAmmo) {
        _ammoText.text = currentAmmo + " / " + maxAmmo;
    }

    #endregion

    #endregion
}
