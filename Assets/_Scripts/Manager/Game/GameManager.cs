using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Setup

    public static GameManager Instance;

    private GameState prevGameState;

    [HideInInspector]
    public GameState currentGameState;

    private void Awake() {
        Instance = this;
        UpdateGameState(GameState.PlayerSelection);    
    }

    #endregion

    #region GameStateUpdating

    public void UpdateGameState(GameState state) {

        if (state != GameState.Pause && state != GameState.Resume) prevGameState = state;
        
        currentGameState = state;

        switch (state) {
            case GameState.PlayerSelection:
                HandlePlayerSelection();
                break;
            case GameState.Preparation:
                HandlePreparation();
                break;
            case GameState.Wave: 
                HandleWave();
                break;
            case GameState.Pause: 
                HandlePause(); 
                break;
            case GameState.Resume:
                HandleResume();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
        }
    }

    #endregion

    #region GameStateHandling

    private void HandlePlayerSelection() {
        MainUIManager.Instance.UpdateUIState(UIState.PlayerSelectionMenu);
    }
    
    private void HandlePreparation() {
        MainUIManager.Instance.UpdateUIState(UIState.None);
        UnitManager.Instance.RemovePlayer();
        CameraManager.Instance.ZoomIn(1f, () => {
            UnitManager.Instance.SpawnPlayer();
            HUDManager.Instance.ShowHUD();
            UnitManager.Instance.MarkEnemiesSpawn();
            CameraManager.Instance.ZoomOut(1f, () => {
                AudioSystem.Instance.PlayMusic(Music.PreparationPhaseMusic);
                HUDManager.Instance.StartCoroutine(HUDManager.Instance.Countdown(5f, () => GameManager.Instance.UpdateGameState(GameState.Wave)));
            });
        });
    } 

    private void HandleWave() {
        AudioSystem.Instance.PlayMusic(Music.WavePhaseMusic);
        UnitManager.Instance.SpawnEnemy();
    }

    private void HandlePause() {
        Time.timeScale = 0;
        MainUIManager.Instance.UpdateUIState(UIState.PauseMenu);
    }
    
    private void HandleResume() {
        MainUIManager.Instance.UpdateUIState(UIState.None);
        if (prevGameState != GameState.Preparation) {
            HUDManager.Instance.StartCoroutine(HUDManager.Instance.Countdown(3f, () => {
                UpdateGameState(prevGameState);
            }));
        }
    }

    private void HandleGameOver() {
        UnitManager.Instance.DestroyAllEnemies();
        MainUIManager.Instance.UpdateUIState(UIState.GameOver);
    }

    #endregion
}

public enum GameState {
    PlayerSelection,
    Preparation,
    Wave,
    Pause,
    Resume,
    GameOver,
}
