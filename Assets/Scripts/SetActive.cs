using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour {
    public GameObject UIs;
    // Use this for initialization
    void Start () {
        //this.SetActive(false);
        UIs.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
