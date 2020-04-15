using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    UIHandler uiHandler;
    PlayerHandler playerHandler;

    int startPlayer = 0;
    int currentPlayer;

    private void Awake() {
        playerHandler = new PlayerHandler();

        uiHandler = GetComponentInChildren<UIHandler>();
        uiHandler.Initialize(this);

        OpenAddPlayersWindow();
    }

    private void UpdateUI() {
        uiHandler.UpdateUI();
    }

    public void StartGame() {
        if (GetPlayers().Count > 1) {
            uiHandler.OpenGameWindow();
        }

        currentPlayer = startPlayer;
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
