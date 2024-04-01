using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEntity : LongRangeEntity, IAttackable, IReloadable
{
    public override void Awake() {
        base.Awake();
    }
}
