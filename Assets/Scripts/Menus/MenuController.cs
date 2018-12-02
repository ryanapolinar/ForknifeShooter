using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    protected int selection;
    protected int finalSelection;
    protected List<string> options;

    // Use this for initialization
    virtual protected void Start () {
        selection = 0;
        finalSelection = -1;
	}
	
	// Update is called once per frame
	virtual protected void Update () {
        MoveCursor();
        SelectOption();
	}

    virtual protected void MoveCursor()
    {
        bool upInput = Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W);
        bool downInput = Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S);
        

        if (upInput)
        {
            selection -= 1;
            if (selection < 0)
            {
                selection = options.Count - 1;
            }
            Debug.Log(options[selection]);
        } else if (downInput)
        {
            
            selection += 1;
            if (selection >= options.Count)
            {
                selection = 0;
            }
            Debug.Log(options[selection]);
        }
    }

    virtual protected void SelectOption()
    {
        bool selectInput = Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return);

        if (selectInput)
        {
            finalSelection = selection;
        }
    }

    public IEnumerator ChangeScene(string sceneName)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }

}
