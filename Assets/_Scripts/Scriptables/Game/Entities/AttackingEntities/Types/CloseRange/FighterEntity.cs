using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEntity : CloseRangeEntity, IAttackable {
    
    #region Setup

    public override void Awake() {
        base.Awake();
        Debug.Log("Hallo"); 
    }   

    #endregion

}
