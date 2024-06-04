using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class PlayerScoreUIHandler : MonoBehaviour
{
    private GameHandler gameHandler;

    private TextMeshProUGUI playerNameText, playerWinsText;
    private TMP_InputField playerScoreText;
    private Image background;
    private Image firstPlayerTokenImage;
    private Image leaderImage;
    private Button removePlayerButton;

    public void Initialize(GameHandler gameHandler, PlayerData playerData) {
        this.gameHandler = gameHandler;

        InitializeComponentsInChildren(playerData);
    }

    private void InitializeComponentsInChildren(PlayerData player) {
        removePlayerButton = GetComponentInChildren<Button>();

        removePlayerButton.onClick.AddListener(() => {
            print("I have been clicked" + player.name);
            gameHandler.RemovePlayer(player);
        });

        TextMeshProUGUI[] tmpTexts = GetComponentsInChildren<TextMeshProUGUI>();
        playerNameText = tmpTexts[0];
        playerWinsText = tmpTexts[1];

        TMP_InputField tmpInputField = GetComponentInChildren<TMP_InputField>();
        playerScoreText = tmpInputField;

        playerScoreText.onDeselect.AddListener((string value) => {
            gameHandler.EditPlayerScore(playerNameText.text, int.Parse(value));
        });


        Image[] imagesInChildren = GetComponentsInChildren<Image>();
        background = imagesInChildren[0];
        firstPlayerTokenImage = imagesInChildren[1];
        leaderImage = imagesInChildren[2];
    }

    public void UpdateData(PlayerData player, bool isLeader, bool isCurrentPlayer, bool isStartingPlayer, bool showPlayer) {
        UpdateTexts(player);
        UpdateBackground(player, isCurrentPlayer, isStartingPlayer);
        UpdateLeaderImage(isLeader);
        ShowHideRemovePlayerButton(showPlayer);
    }

    private void UpdateTexts(PlayerData player) {
        UpdateRemovePlayerButton(player);
        playerNameText.text = player.name;
        playerWinsText.text = player.wins.ToString();
        playerScoreText.text = player.score.ToString();
    }

    private void UpdateRemovePlayerButton(PlayerData player) {
        removePlayerButton.onClick.RemoveAllListeners();
        removePlayerButton.onClick.AddListener(() => {
            gameHandler.RemovePlayer(player);
        });
    }

    private void UpdateBackground(PlayerData player, bool isCurrentPlayer, bool isStartingPlayer) {
        Color newBackgroundColor = new Color();
        float firstPlayerAlphaValue = 1f; ;
        float textOpacity = 1f;

        if (isStartingPlayer) {
            if (!player.isPlaying) {
                firstPlayerAlphaValue = .5f;
            }
        }
        else {
            firstPlayerAlphaValue = 0f;

        }

        if (gameHandler.finalRound) {
            if (!player.isPlaying) {
                newBackgroundColor = new Color(.3f, .3f, .3f, .2f);
                textOpacity = .2f;
            }
            else if (isCurrentPlayer) {
                newBackgroundColor = new Color(1f, .2f, .2f, .8f);
            }
            else {
                newBackgroundColor = new Color(1f, .2f, .2f, .2f);
            }
        }
        else {
            if (!player.isPlaying) {
                newBackgroundColor = new Color(.3f, .3f, .3f, .2f);
                textOpacity = .2f;
            }
            else if (isCurrentPlayer) {
                newBackgroundColor = new Color(.5f, 1f, .5f, .8f);
            }
            else {
                newBackgroundColor = new Color(1f, 1f, 1f, .2f);
            }
        }

        SetBackgroundColorAndTextOpacity(newBackgroundColor, textOpacity, firstPlayerAlphaValue);
    }

    private void UpdateLeaderImage(bool isLeader) {
        leaderImage.gameObject.SetActive(isLeader);
    }

    private void ShowHideRemovePlayerButton(bool showButton) {
        removePlayerButton.gameObject.SetActive(showButton);
    }

    private void SetBackgroundColorAndTextOpacity(Color color, float textOpacity, float firstPlayerAlphaValue) {
        background.color = color;
        Color newColor = playerNameText.color;
        newColor.a = textOpacity;
        playerNameText.color = newColor;
        playerWinsText.color = newColor;
        playerScoreText.textComponent.color = newColor;

        Color newFirstPlayerTokenColor = firstPlayerTokenImage.color;
        newFirstPlayerTokenColor.a = firstPlayerAlphaValue;
        firstPlayerTokenImage.color = newFirstPlayerTokenColor;
    }

    public void DisableScoreEditing() {
        playerScoreText.interactable = false;
    }

    public void EnableScoreEditing() {
        playerScoreText.interactable = true;
    }
}
