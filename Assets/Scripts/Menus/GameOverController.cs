using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MenuController {

    public Text playAgainText, returnTitleScreenText;

	override protected void Start () {
        base.Start();
        this.options = new List<string>() { "Play Again", "Return to Title Screen" };
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
                    // Play Again: move them to level 1
                    StartCoroutine(ChangeScene("level1"));
                    break;
                case 1:
                    // Return to Title Screen: move them to the title screen
                    StartCoroutine(ChangeScene("titleScreen"));
                    break;
            }
            finalSelection = -1;
        }
    }

    private void UpdateSelector()
    {
        // Reset the colors
        playAgainText.color = Color.white;
        returnTitleScreenText.color = Color.white;

        // Highlight the selected option
        switch (this.selection)
        {
            case 0:
                playAgainText.color = Color.red;
                break;
            case 1:
                returnTitleScreenText.color = Color.red;
                break;
        }
    }

}
