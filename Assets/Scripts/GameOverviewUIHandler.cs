using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverviewUIHandler : MonoBehaviour
{
    ScoreButtonUIHandler scoreButtonUIHandler;
    TextMeshProUGUI inputScoreText;

    public void Initialize(GameHandler gameHandler) {
        scoreButtonUIHandler = GetComponentInChildren<ScoreButtonUIHandler>();
        scoreButtonUIHandler.Initialize(gameHandler);

        TextMeshProUGUI[] tmpTexts = GetComponentsInChildren<TextMeshProUGUI>(); // Title, inputScore ...
        inputScoreText = tmpTexts[1];
    }

    public void UpdateUI(int inputScore) {
        inputScoreText.text = inputScore.ToString();
    }
}
