using System.Collections;
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
            Scene currentScene = SceneManager.GetActiveScene();
            if (!triggered)
            {
                triggered = true;

                // Fade to black
                blackScreen.CrossFadeAlpha(1.0f, 1.0f, false);

                // Change the scene
                switch (currentScene.name)
                {
                    case "level1":
                        StartCoroutine(ChangeScene("hallMonitor"));
                        break;
                    case "hallMonitor":
                        // @TODO: Transition to Scientist scene for upgrades
                        StartCoroutine(ChangeScene("level2"));
                        break;
                    case "level2":
                        StartCoroutine(ChangeScene("ASBPresident"));
                        break;
                    case "ASBPresident":
                        // @TODO: Transition to Scientist scene for upgrades
                        Debug.Log("TODO: Stay tuned for the next episode of Dodgeball Z!");
                        break;
                }
            }

        }
    }

    public IEnumerator ChangeScene(string sceneName)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}
