using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour {

    public Button level1;
    public Button level2;
    public Button level3;
    public Button level4;
    public GameObject UIss;
	// Use this for initialization
	void Start () {
        level1.onClick.AddListener(TaskOnClick); //change taskonclick to taskonclick1
        level2.onClick.AddListener(TaskOnClick); //taskonclick2
        level3.onClick.AddListener(TaskOnClick); //taskonclick3
        level4.onClick.AddListener(TaskOnClick); //taskonclick4
    }
	
	// Update is called once per frame
	void Update () {

	}

    void TaskOnClick()
    {
        Time.timeScale = 1;
        Debug.Log("Button Clicked");
        UIss.SetActive(false);
    }
}
