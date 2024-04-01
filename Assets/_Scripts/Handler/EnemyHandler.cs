using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour {

    #region Setup

    //Components
    private Enemy _enemy;
    IAttackable _attackable;
    IReloadable _reloadable;


    //Player
    private Transform _playerPosition;

    private void Awake() {
        _playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

        if (GetComponent<FighterEnemy>()) {
            _enemy = GetComponent<FighterEnemy>();
        }
        else if (_enemy = GetComponent<ShooterEnemy>()) {
            _enemy = GetComponent<ShooterEnemy>();
        }
        else _enemy = GetComponent<SniperEnemy>();
    
        _attackable = GetComponent<IAttackable>();
        _reloadable = GetComponent<IReloadable>();
    }

    #endregion

    #region Handling

    private void Update() {
        
        if(_attackable != null) {
            _attackable.PrimaryButton();
        }
        
        if(_playerPosition != null) {
            _enemy.Rotate(_playerPosition.position);
            _enemy.Move(_playerPosition.position);
        }      
    }

    #endregion
}
