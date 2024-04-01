using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class MainUIManager : MonoBehaviour {

    #region Setup

    public static MainUIManager Instance;

    private void Awake() {

        Instance = this;

        #region UIStates

        #region MainMenu

        if (_playButton != null) _playButton.onClick.AddListener(OnPlayButtonClick);
        if (_optionsMainMenuButton != null) _optionsMainMenuButton.onClick.AddListener(OnOptionsMainButtonClick);
        if (_exitButton != null) _exitButton.onClick.AddListener(OnExitButtonClick);

        #endregion

        #region PlayerSelectionMenu

        if (_fighterPlayerButton != null) {
            _fighterPlayerButton.onClick.AddListener(() => OnPlayerTypeButtonClick(PlayerTypes.FighterPlayer));
        }
        if (_shooterPlayerButton != null) _shooterPlayerButton.onClick.AddListener(() => OnPlayerTypeButtonClick(PlayerTypes.ShooterPlayer));
        if (_sniperPlayerButton != null) _sniperPlayerButton.onClick.AddListener(() => OnPlayerTypeButtonClick(PlayerTypes.SniperPlayer));

        #endregion

        #region PauseMenu

        if (_continueButton != null) _continueButton.onClick.AddListener(OnContinueButtonClick);
        if (_optionsGameButton != null) _optionsGameButton.onClick.AddListener(OnOptionsGameButtonClick);
        if (_quitButton != null) _quitButton.onClick.AddListener(OnQuitButtonClick);

        #endregion

        #region Options

        _audioButton.onClick.AddListener(OnAudioButtonClick);
        _controlsButton.onClick.AddListener(OnControlsButtonClick);
        _videoButton.onClick.AddListener(OnVideoButtonClick);
        _graphicsButton.onClick.AddListener(OnGraphicsButtonClick);

        _optionsBackButton.onClick.AddListener(OnBackButtonClick);

        #endregion

        #region Audio

        // See MasterButtonScript for _audioMasterButton
        // See MusicButtonScript for _audioMusicButton
        _audioBackButton.onClick.AddListener(OnBackButtonClick);

        #endregion

        #region Controls

        _controlsBackButton.onClick.AddListener(OnBackButtonClick);

        #endregion

        #region Video

        // See FullscreenButtonScript for _fullscreenButton
        // See ResolutionButtonScript for _resolutionButton
        _applyResolutionButton.onClick.AddListener(OnApplyResolutionButtonClick);
        _applyFullscreenButton.onClick.AddListener(OnApplyFullscreenButtonClick);
        _videoBackButton.onClick.AddListener(OnBackButtonClick);

        _applyResolutionButton.gameObject.SetActive(false);
        _currentResolutionIndex = savedResolutionIndex;

        _applyFullscreenButton.gameObject.SetActive(false);
        _isFullscreen = savedIsFullscreen;

        #endregion

        #region Graphics

        //See QualityButtonScript for _qualityButton
        //See FramerateButtonScript for _framerateButton
        _applyQualityButton.onClick.AddListener(OnApplyQualityButtonClick);
        _applyFramerateButton.onClick.AddListener(OnApplyFramerateButtonClick);
        _graphicsBackButton.onClick.AddListener(OnBackButtonClick);

        #endregion

        #endregion
    }

    private void Start() {
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons) {
            button.onClick.AddListener(() => AudioSystem.Instance.PlaySound(Sound.ButtonClick));
        }
    }

    #endregion

    #region UpdateUIState

    private UIState _UIState = UIState.MainMenu, _prevUIState;

    public void UpdateUIState(UIState nextUIState) {

        if (_optionsMenuCanvas.enabled) {
            if (_playButton) _prevUIState = UIState.MainMenu;
            else _prevUIState = UIState.PauseMenu;
        }
        else _prevUIState = _UIState;

        _UIState = nextUIState;

        switch (nextUIState) {
            case UIState.None:
                HandleNone();
                break;            
            case UIState.PlayerSelectionMenu: 
                HandlePlayerSelectionMenu(); 
                break;
            case UIState.MainMenu:
                HandleMainMenu();
                break;
            case UIState.PauseMenu:
                HandlePauseMenu();
                break;            
            case UIState.Options:
                HandleOptionsMenu();
                break;
            case UIState.Audio:
                HandleAudioMenu();
                break;
            case UIState.Controls:
                HandleControlsMenu();
                break;
            case UIState.Video:
                HandleVideoMenu();
                break;
            case UIState.Graphics:
                HandleGraphicsMenu();
                break;
            case UIState.GameOver:
                HandleGameOverMenu();
                break;
        }
    }

    private void HandleNone() {
        _generalPauseMenuCanvas.enabled = false; //isnt deactivated from hideallmenus because of inner-settings navigation
        HideAllMenus();
        Cursor.visible = false;
    }

    private void HandleMainMenu() {
        HideAllMenus();
        _mainMenuCanvas.enabled = true;
        Cursor.visible = true;
    }

    private void HandlePlayerSelectionMenu() {
        HideAllMenus();
        Cursor.visible = true;
        _playerSelectionCanvas.enabled = true;
    }
    
    private void HandlePauseMenu() {
        HideAllMenus();
        _generalPauseMenuCanvas.enabled = true;
        _pauseMenuCanvas.enabled = true;    
        Cursor.visible = true;
    }

    private void HandleAudioMenu() {
        HideAllMenus();
        _audioMenuCanvas.enabled = true;
    }

    private void HandleOptionsMenu() {
        HideAllMenus();
        _optionsMenuCanvas.enabled = true;
    }

    private void HandleControlsMenu() {
        HideAllMenus();
        _controlsMenuCanvas.enabled = true;
    }

    private void HandleVideoMenu() {
        HideAllMenus();
        _videoMenuCanvas.enabled = true;
    }

    private void HandleGraphicsMenu() {
        HideAllMenus();
        _graphicsMenuCanvas.enabled = true;
    }

    private void HandleGameOverMenu() {
        HideAllMenus();
        _gameOverCanvas.enabled = true;
    }

    private void HideAllMenus() {
        _audioMenuCanvas.enabled = false;
        _controlsMenuCanvas.enabled = false;
        _optionsMenuCanvas.enabled = false;
        _videoMenuCanvas.enabled = false;
        _graphicsMenuCanvas.enabled = false;

        if (_mainMenuCanvas != null) _mainMenuCanvas.enabled = false;
        else {
            _pauseMenuCanvas.enabled = false;    
            _playerSelectionCanvas.enabled = false;
            _gameOverCanvas.enabled = false;
        }
    }
    
    #endregion

    #region UIStates

    #region MainMenu
   
    [Header("Main Menu")]

    [SerializeField] Canvas _mainMenuCanvas;

    [SerializeField] private Button _playButton;

    [SerializeField] private Button _optionsMainMenuButton;

    [SerializeField] private Button _exitButton;

    private void OnPlayButtonClick() {
        SceneManager.LoadScene("MainGame");
    }

    private void OnOptionsMainButtonClick() {
        UpdateUIState(UIState.Options);
    }

    private void OnExitButtonClick() {
        Application.Quit();
    }

    #endregion

    #region PlayerSelection

    [Header("Player Selection")]

    [SerializeField] Canvas _playerSelectionCanvas;

    [SerializeField] private Button _fighterPlayerButton;

    [SerializeField] private Button _shooterPlayerButton;

    [SerializeField] private Button _sniperPlayerButton;

    private void OnPlayerTypeButtonClick(PlayerTypes _selectedPlayerType) {
        UnitManager.Instance.ChangePlayer(_selectedPlayerType);
        GameManager.Instance.UpdateGameState(GameState.Preparation);
    }

    #endregion

    #region PauseMenu

    [Header("PauseMenu")]

    [SerializeField] Canvas _pauseMenuCanvas;

    [SerializeField] Canvas _generalPauseMenuCanvas;

    [SerializeField] private Button _continueButton;

    [SerializeField] private Button _optionsGameButton;

    [SerializeField] private Button _quitButton;

    private void OnContinueButtonClick() {
        GameManager.Instance.UpdateGameState(GameState.Resume);
    }

    private void OnOptionsGameButtonClick() {
        UpdateUIState(UIState.Options);
    }

    private void OnQuitButtonClick() {
        SceneManager.LoadScene("MainMenu");
    }

    #endregion

    #region GameOverMenu

    [Header("Game Over")]

    [SerializeField] Canvas _gameOverCanvas;



    #endregion

    #region Options

    [Header("Options")]

    [SerializeField] private Canvas _optionsMenuCanvas;

    [SerializeField] private Button _audioButton;

    [SerializeField] private Button _controlsButton;

    [SerializeField] private Button _videoButton;

    [SerializeField] private Button _graphicsButton;

    [SerializeField] private Button _optionsBackButton;

    private void OnAudioButtonClick() {
        UpdateUIState(UIState.Audio);
    }

    private void OnControlsButtonClick() {
        UpdateUIState(UIState.Controls);
    }

    private void OnVideoButtonClick() {
        UpdateUIState(UIState.Video);
    }

    private void OnGraphicsButtonClick() {
        UpdateUIState(UIState.Graphics);
    }

    #endregion

    #region Audio

    [Header("Audio")]

    [SerializeField] private Canvas _audioMenuCanvas;

    [SerializeField] private Button _audioMasterButton;

    [SerializeField] private Button _audioMusicButton;

    [SerializeField] private Button _audioBackButton;

    //werden über die jeweiligen ButtonScripts gedrückt
    public void OnMasterButtonClick(float operation) {
        OptionsManager.Instance.OnMasterChange(operation);
        float masterVolume = (3.3333333f * OptionsManager.Instance.masterLevel) + 100f;
        if (OptionsManager.Instance.masterLevel == -80f) masterVolume = 0;
        _audioMasterButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Master: " + masterVolume + "%";
    }

    //werden über die jeweiligen ButtonScripts gedrückt
    public void OnMusicButtonClick(float operation) {
        OptionsManager.Instance.OnMusicChange(operation);
        float musicVolume = (3.3333333f * OptionsManager.Instance.musicLevel) + 100f;
        if (OptionsManager.Instance.musicLevel == -80f) musicVolume = 0;
        _audioMusicButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Music: " + musicVolume + "%";
    }

    #endregion

    #region Controls

    [Header("Controls")]

    [SerializeField] private Canvas _controlsMenuCanvas;

    [SerializeField] private Button _controlsBackButton;

    #endregion

    #region Video

    [Header("Video")]

    [SerializeField] private Canvas _videoMenuCanvas;

    [SerializeField] private Button _resolutionButton;

    [SerializeField] private Button _applyResolutionButton;

    [SerializeField] private Button _fullscreenButton;

    [SerializeField] private Button _applyFullscreenButton;

    [SerializeField] private Button _videoBackButton;

    private Resolution[] _resolutions = new Resolution[] {
            new Resolution{height = 1920, width = 1080},
            new Resolution { height = 1280, width = 720 },
            new Resolution { height = 640, width = 360 },
        };



    [HideInInspector] public int _currentResolutionIndex = 0;
    private int savedResolutionIndex;

    private bool _isFullscreen;
    private bool savedIsFullscreen = true;

    public void OnResolutionButtonClick(int operation) {

        _currentResolutionIndex = _currentResolutionIndex + (operation * 1);

        if (_currentResolutionIndex > _resolutions.Length - 1) _currentResolutionIndex = 0;
        if (_currentResolutionIndex < 0) _currentResolutionIndex = _resolutions.Length - 1;

        _resolutionButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Resolution: " + _resolutions[_currentResolutionIndex].height + "x" + _resolutions[_currentResolutionIndex].width;

        if (savedResolutionIndex == _currentResolutionIndex) {
            _applyResolutionButton.gameObject.SetActive(false);
        }
        else _applyResolutionButton.gameObject.SetActive(true);
    }

    private void OnApplyResolutionButtonClick() {
        Screen.SetResolution(_resolutions[_currentResolutionIndex].height, _resolutions[_currentResolutionIndex].width, savedIsFullscreen ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed);
        savedResolutionIndex = _currentResolutionIndex;
        _applyResolutionButton.gameObject.SetActive(false);
    }

    public void OnFullscreenButtonClick() {
        _isFullscreen = !_isFullscreen;
        _fullscreenButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Fullscreen: " + (_isFullscreen ? "On" : "Off");

        if (savedIsFullscreen == _isFullscreen) _applyFullscreenButton.gameObject.SetActive(false);
        else _applyFullscreenButton.gameObject.SetActive(true);
    }

    private void OnApplyFullscreenButtonClick() {
        Screen.SetResolution(_resolutions[savedResolutionIndex].height, _resolutions[savedResolutionIndex].width, _isFullscreen ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed);
        savedIsFullscreen = _isFullscreen;
        _applyFullscreenButton.gameObject.SetActive(false);
    }

    #endregion

    #region Graphics

    [Header("Graphics")]

    [SerializeField] private Canvas _graphicsMenuCanvas;

    [SerializeField] private Button _qualityButton;

    [SerializeField] private Button _applyQualityButton;

    [SerializeField] private Button _framerateButton;

    [SerializeField] private Button _applyFramerateButton;
    
    [SerializeField] private Button _graphicsBackButton;


    public void OnQualityButtonClick(float operation) {
        OptionsManager.Instance.OnQualityChange(operation);
        _qualityButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Quality: " + OptionsManager.Instance.qualitySettings[OptionsManager.Instance.qualitySettingsIndex];
    }

    private void OnApplyQualityButtonClick() {
        OptionsManager.Instance.OnApplyQuality();
    }

    public void OnFramerateButtonClick(float operation) {
        OptionsManager.Instance.OnFramerateChange(operation);
        _framerateButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Framerate: " + OptionsManager.Instance.framerateSettings[OptionsManager.Instance.framerateSettingsIndex];
    }

    private void OnApplyFramerateButtonClick() {
        OptionsManager.Instance.OnApplyFramerate();
    }

    #endregion

    #endregion

    #region BackButtonsLogic

    private void OnBackButtonClick() {
        UpdateUIState(_prevUIState);
    }

    #endregion
}

public enum UIState {
    None,
    MainMenu,
    PlayerSelectionMenu,
    GameOver,
    PauseMenu,
    Options,
    Audio,
    Controls,
    Video,
    Graphics
}
