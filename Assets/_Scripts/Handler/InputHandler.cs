using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    #region Setup

    private Player _player;

    private IAttackable _attackable;
    private IReloadable _reloadable;

    void Awake() {
        _player = GetComponent<ShooterPlayer>();
        if(_player == null) { 
            _player = GetComponent<SniperPlayer>();
        }
        if(_player == null) {
            _player = GetComponent<FighterPlayer>();
        }
        
        _attackable = GetComponent<IAttackable>();
        _reloadable = GetComponent<IReloadable>();
    }

    #endregion

    #region Input

    private void Update() {        
        if(_attackable != null) {
            if (Input.GetButton("PrimaryButton")) _attackable.PrimaryButton();
            if (Input.GetButtonDown("SecondaryButton")) _attackable.SecondaryButton();
            if (Input.GetButtonDown("Reload") && _reloadable != null) StartCoroutine(_reloadable.Reload());
        }

        if (Input.GetButton("Ability")) _player.Ability();
        _player.Move(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0));
        _player.Rotate(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameManager.Instance.currentGameState == GameState.Pause) GameManager.Instance.UpdateGameState(GameState.Resume);
            else GameManager.Instance.UpdateGameState(GameState.Pause);
        } 
    }

    #endregion
}
