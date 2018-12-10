using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenController : MenuController
{

    public Text startGameText, quitText;
    AudioSource music;

    override protected void Start()
    {
        base.Start();
        this.options = new List<string>() { "Start Game", "Quit" };
        UpdateSelector();

        music = GetComponent<AudioSource>();
        music.Play();
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
                    StartCoroutine(ChangeScene("IntroCutscene"));
                    break;
                case 1:
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
        startGameText.color = Color.black;
        quitText.color = Color.black;

        // Highlight the selected option
        switch(this.selection)
        {
            case 0:
                startGameText.color = Color.red;
                break;
            case 1:
                quitText.color = Color.red;
                break;
        }
    }
}
