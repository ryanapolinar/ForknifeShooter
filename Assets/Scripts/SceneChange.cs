﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    bool triggered = false;

    public Image blackScreen;

	// Use this for initialization
	void Start () {
        triggered = false;
        Color blackScreenColor = blackScreen.color;
        blackScreenColor.a = 1.0f;
        blackScreen.color = blackScreenColor;
        blackScreen.CrossFadeAlpha(0.0f, 1.0f, false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Change the scene when player enters
        if (collision.tag == "Player")
        {
            // Fade to black
            blackScreen.CrossFadeAlpha(1.0f, 1.0f, false);

            StartCoroutine(ChangeScene());
        }
        
    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
