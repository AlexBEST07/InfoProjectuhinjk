using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemy : Enemy
{
    #region Setup

    SniperEntity sniperEntity;

    public override void Awake() {
        base.Awake();
        sniperEntity = GetComponent<SniperEntity>();    
    }

    #endregion

    private void Update() {
        if (_playerPosition != null) sniperEntity.showSniperLaser(_playerPosition.position);
    }
}
