using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour {
    public int currentImageIndex = 0;
    public GameObject [] cutsceneImages;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < cutsceneImages.Length; i++)
        {
            cutsceneImages[i].SetActive(false);
        }

        currentImageIndex = 0;
        cutsceneImages[0].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
        {
            currentImageIndex++;
            if (currentImageIndex >= cutsceneImages.Length)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            } else
            {
                for (int i = 0; i < cutsceneImages.Length; i++)
                {
                    cutsceneImages[i].SetActive(false);
                }

                cutsceneImages[currentImageIndex].SetActive(true);
            }
            
        }
    }
}
