using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
    
    public void PickUpItem() {
        
    }

    [Header("Ability")]

    public float abilityDuration;

    public float abilityCooldown;

    [HideInInspector] public float nextTimeToAbility = 0f;

    [HideInInspector] public bool isAbilityOnCooldown = true;

    public virtual void Ability() { 
        //will be overwritten anyways
    }
}
