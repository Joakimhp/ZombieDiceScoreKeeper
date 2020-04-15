using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreUIHandler : MonoBehaviour
{
    TextMeshProUGUI playerNameText, playerWinsText, playerScoreText;


    public void Initialize() {
        TextMeshProUGUI[] tmpTexts = GetComponentsInChildren<TextMeshProUGUI>();
        playerNameText = tmpTexts[0];
        playerWinsText = tmpTexts[1];
        playerScoreText = tmpTexts[2];
    }

    public void UpdateTexts(PlayerData player) {
        playerNameText.text = player.name;
        playerWinsText.text = player.wins.ToString();
        playerScoreText.text = player.score.ToString();
    }
}
