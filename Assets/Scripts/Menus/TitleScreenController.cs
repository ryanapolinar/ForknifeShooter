using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenController : MenuController
{

    public Text startGameText, instructionsText, quitText;

    override protected void Start()
    {
        base.Start();
        this.options = new List<string>() { "Start Game", "Instructions", "Quit" };
        UpdateSelector();
    }

    protected override void Update()
    {
        base.Update();
        UpdateSelector();

        if (this.finalSelection != -1)
        {
            switch (this.finalSelection)
            {
                case 0:
                    // Start Game: move them to level 1
                    StartCoroutine(ChangeScene("level1"));
                    break;
                case 1:
                    // Instructions: display the instructions
                    Debug.Log("TODO: display instructions!");
                    break;
                case 2:
                    // Quit: exit the application
                    Application.Quit();
                    break;
            }
            finalSelection = -1;
        }
    }

    private void UpdateSelector()
    {
        // Reset the colors
        startGameText.color = Color.white;
        instructionsText.color = Color.white;
        quitText.color = Color.white;

        // Highlight the selected option
        switch(this.selection)
        {
            case 0:
                startGameText.color = Color.red;
                break;
            case 1:
                instructionsText.color = Color.red;
                break;
            case 2:
                quitText.color = Color.red;
                break;
        }
    }
}
