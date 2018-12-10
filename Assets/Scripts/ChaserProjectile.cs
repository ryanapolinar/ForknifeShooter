using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserProjectile : EnemyProjectile {

    override protected void Start()
    {
        base.Start();
        totalSpeed = 6f;
    }
}
