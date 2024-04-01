using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SniperEntity : LongRangeEntity, IAttackable, IReloadable {

    [HideInInspector] public LineRenderer _lineRenderer;
    
    #region Setup

    public override void Awake() {
        base.Awake();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.01f;
        _lineRenderer.endWidth = 0.01f;
    }

    #endregion

    #region Shooting

    [HideInInspector] public bool hideSniperLaser = false;

    public override void PrimaryButton() {
        if (HUDManager.Instance.scopeActivated) {
            Shoot(secondaryFireRate, secondaryBullet, secondaryDamage, 0);
        }
        else base.PrimaryButton();
    }

    public override void SecondaryButton() {
        HUDManager.Instance.ManageScope();
        _lineRenderer.enabled = !HUDManager.Instance.scopeActivated; //if scope activated hide lineRenderer
    }

    public void showSniperLaser(Vector2 focusedPosition) {
        if(hideSniperLaser == false) {
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, new Vector2(bulletSpawn.position.x, bulletSpawn.position.y));
            _lineRenderer.SetPosition(1, focusedPosition);
        }
    }

    #endregion
}
