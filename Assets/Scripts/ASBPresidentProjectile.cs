using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASBPresidentProjectile : EnemyProjectile
{

    // Use this for initialization
    override protected void Start()
    {
        base.Start();
        totalSpeed = 10f;
    }

}
