using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShooterPlayer : Player {

    ShooterEntity shooterEntity;
    
    public override void Awake() {
        base.Awake();
        shooterEntity = GetComponent<ShooterEntity>();
    }


    #region Abilitiy 

    [Header("Speed Ability")]

    [SerializeField] float addedSpeed;

    public override void Ability() {
        if(Time.time >= nextTimeToAbility) {
            StartCoroutine(ActivateSpeedBoost());
            nextTimeToAbility = Time.time + abilityCooldown;
        }
    }

    private IEnumerator ActivateSpeedBoost() {
        speedBoost = addedSpeed;
        yield return new WaitForSeconds(abilityDuration);
        speedBoost = 0;
    }

    #endregion
}


