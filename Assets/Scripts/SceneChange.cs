using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    bool triggered = false;

	// Use this for initialization
	void Start () {
        triggered = false;
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
