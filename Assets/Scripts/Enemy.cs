﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Unit {

    Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();      
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    
}
