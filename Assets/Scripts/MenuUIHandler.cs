using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    TMP_InputField inputName;
    Button submitButton;
    Button playButton;

    public void Initialize(GameHandler gameHandler) {
        inputName = GetComponentInChildren<TMP_InputField>();

        Button[] tmpButtons = GetComponentsInChildren<Button>();

        submitButton = tmpButtons[0];
        playButton = tmpButtons[1];

        submitButton.onClick.AddListener(() => {
            string text = inputName.text;
            if(!string.IsNullOrWhiteSpace(text)) {
                gameHandler.AddPlayer(inputName.text);
                inputName.text = "";
            }
        });

        playButton.onClick.AddListener(() => {
            gameHandler.StartGame();
        });
    }
}
