using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverviewUIHandler : MonoBehaviour
{
    ScoreButtonUIHandler scoreButtonUIHandler;
    TextMeshProUGUI inputScoreText;
    Button nextPlayerButton;

    public void Initialize(GameHandler gameHandler) {
        scoreButtonUIHandler = GetComponentInChildren<ScoreButtonUIHandler>();
        scoreButtonUIHandler.Initialize(gameHandler);

        TextMeshProUGUI[] tmpTexts = GetComponentsInChildren<TextMeshProUGUI>(); // Title, inputScore ...
        inputScoreText = tmpTexts[1];

        Button[] tmpButtons = GetComponentsInChildren<Button>();
        nextPlayerButton = tmpButtons[tmpButtons.Length - 1];

        nextPlayerButton.onClick.AddListener(() => {
            gameHandler.NextPlayer();
        });
    }

    public void UpdateUI(int inputScore) {
        inputScoreText.text = inputScore.ToString();
    }
}
