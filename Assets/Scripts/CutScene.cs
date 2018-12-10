using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour {
    public Image image;
    public Image [] cutsceneImages;
	// Use this for initialization
	void Start () {
        cutsceneImages[i].enabled = true;
        //image.enabled = true;
        image = gameObject.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < cutsceneImages.Length; i++)
        {
            if (Input.anyKey)
            {
                cutsceneImages[i] = false;
                cutsceneImages[i + 1] = true;
            }
            
        }
        if (Input.anyKey)
            SceneManager.LoadScene("testscene");
    }
    /* // singular
    void Upate()
    {
        if (Input.anyKey)
        {
            image.enabled = false;
            if (Input.anyKey)
                SceneManager.LoadScene("testscene");
        }
        */
}
