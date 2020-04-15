using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    UIHandler uiHandler;
    PlayerHandler playerHandler;

    int startPlayer = 0;
    int currentPlayer;
    int inputScore;

    private void Awake() {
        playerHandler = new PlayerHandler();

        uiHandler = GetComponentInChildren<UIHandler>();
        uiHandler.Initialize(this);

        OpenAddPlayersWindow();
    }

    private void UpdateUI() {
        uiHandler.UpdateUI(inputScore);
    }

    public void StartGame() {
        if (GetPlayers().Count > 1) {
            uiHandler.OpenGameWindow();
        }

        currentPlayer = startPlayer;
        inputScore = 0;
    }
    
    public void AddToInputScore(int bumpAmount) {
        inputScore = int.Parse(inputScore.ToString() + bumpAmount.ToString());
        UpdateUI();
    }

    public void RemoveLastDigitFromInputScore() {
        string scoreString = inputScore.ToString();
        if(scoreString.Length > 1) {
        inputScore = int.Parse(scoreString.Substring(0, scoreString.Length - 1));
        }
        else {
            inputScore = 0;
        }
        UpdateUI();
    }

    public void OpenAddPlayersWindow() {
        uiHandler.OpenMenuOverviewWindow();
    }

    public void AddPlayer(string playerName) {
        playerHandler.AddPlayer(playerName);
        UpdateUI();
    }

    public List<PlayerData> GetPlayers() {
        return playerHandler.GetPlayers();
    }
}
