using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour {

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
        Debug.Log("Works");
        if (other.tag == "Player")
        {
            PauseGame();
            UI.SetActive(true);
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        //pausePanel.SetActive(true);

    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        //pausePanel.SetActive(false);
    }
}
