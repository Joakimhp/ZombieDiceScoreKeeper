using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverviewUIHandler : MonoBehaviour
{
    ScoreButtonUIHandler scoreButtonUIHandler;
    TextMeshProUGUI inputScoreText;
    Button managePlayersButton;
    Button nextPlayerButton;

    public void Initialize(GameHandler gameHandler) {
        InitializeComponentsInChildren(gameHandler);

        TextMeshProUGUI[] tmpTexts = GetComponentsInChildren<TextMeshProUGUI>(); // Title, inputScore ...
        inputScoreText = tmpTexts[1];

        Button[] tmpButtons = GetComponentsInChildren<Button>();
        managePlayersButton = tmpButtons[0];
        nextPlayerButton = tmpButtons[tmpButtons.Length - 1];

        managePlayersButton.onClick.AddListener(() => {
            gameHandler.OpenAddPlayersWindow();
        });

        nextPlayerButton.onClick.AddListener(() => {
            gameHandler.NextPlayer();
        });
    }

    private void InitializeComponentsInChildren(GameHandler gameHandler ) {
        scoreButtonUIHandler = GetComponentInChildren<ScoreButtonUIHandler>();
        scoreButtonUIHandler.Initialize(gameHandler);
    }

    public void UpdateUI(int inputScore) {
        inputScoreText.text = inputScore.ToString();
    }
}
