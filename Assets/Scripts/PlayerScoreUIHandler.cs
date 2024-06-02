using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerScoreUIHandler : MonoBehaviour
{
    TextMeshProUGUI playerNameText, playerWinsText, playerScoreText;
    Image background;

    public void Initialize() {
        TextMeshProUGUI[] tmpTexts = GetComponentsInChildren<TextMeshProUGUI>();
        playerNameText = tmpTexts[0];
        playerWinsText = tmpTexts[1];
        playerScoreText = tmpTexts[2];

        background = GetComponent<Image>();
    }

    public void UpdateTextsAndBackground(PlayerData player, bool isCurrentPlayer, bool isStartingPlayer) {
        UpdateTexts(player);
        UpdateBackground(player, isCurrentPlayer, isStartingPlayer);
    }

    private void UpdateTexts(PlayerData player) {
        playerNameText.text = player.name;
        playerWinsText.text = player.wins.ToString();
        playerScoreText.text = player.score.ToString();
    }

    private void UpdateBackground(PlayerData player, bool isCurrentPlayer, bool isStartingPlayer) {
        Color newColor = new Color();
        float textOpacity = 1f;

        if (isStartingPlayer) {
            if (!player.isPlaying) {
                newColor = new Color(.7f, .3f, .3f, .2f);
                textOpacity = .2f;
            }
            else if (isCurrentPlayer) {
                newColor = new Color(1f, .2f, .2f, .6f);
            }
            else {
                newColor = new Color(1f, .2f, .2f, .2f);
            }
        }
        else {
            if (!player.isPlaying) {
                newColor = new Color(.3f, .3f, .3f, .2f);
                textOpacity = .2f;
            }
            else if (isCurrentPlayer) {
                newColor = new Color(1f, 1f, .5f, .8f);
            }
            else {
                newColor = new Color(1f, 1f, 1f, .2f);
            }
        }

        SetBackgroundColorAndTextOpacity(newColor, textOpacity);
    }

    private void SetBackgroundColorAndTextOpacity(Color color, float textOpacity) {
        background.color = color;
        Color newColor = playerNameText.color;
        newColor.a = textOpacity;
        playerNameText.color = newColor;
        playerWinsText.color = newColor;
        playerScoreText.color = newColor;
    }
}
