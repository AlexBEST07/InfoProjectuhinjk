using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingEntity : MonoBehaviour {
    
    public virtual void Awake() {
    
    }

    public virtual void PrimaryButton() {

    }

    public virtual void SecondaryButton() { 
    
    }
}

public interface IAttackable {
    void PrimaryButton();
    void SecondaryButton();
}

public interface IReloadable {
    IEnumerator Reload();
}
