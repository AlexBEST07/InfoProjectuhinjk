using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SniperPlayer : Player
{
    #region Setup

    SniperEntity sniperEntity;

    public override void Awake() {
        base.Awake();
        sniperEntity = GetComponent<SniperEntity>();
    }

    #endregion

    private void Update() {
        sniperEntity.showSniperLaser(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
    }

    public override void Ability() {
        transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    }

}
