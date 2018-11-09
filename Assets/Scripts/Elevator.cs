using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    private GameObject pausePanel;
    public GameObject UI;
	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Detected");
        if (other.tag == "Player")
        {
            PauseGame();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ContinueGame();
        }
    }

    private void PauseGame()
    {
        //Time.timeScale = 0;
        UI.SetActive(true);
        //pausePanel.SetActive(true);

    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        UI.SetActive(false);
        //pausePanel.SetActive(false);
    }
}
