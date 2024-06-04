using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    MenuUIHandler menuUIHandler;
    GameOverviewUIHandler gameOverviewUIHandler;
    PlayerUIHandler playerUIHandler;
    WinnerUIHandler winnerUIHandler;

    public void Initialize(GameHandler gameHandler) {
        InitializeComponentsInChildren(gameHandler);
    }

    private void InitializeComponentsInChildren(GameHandler gameHandler) {
        menuUIHandler = GetComponentInChildren<MenuUIHandler>();
        menuUIHandler.Initialize(gameHandler);

        gameOverviewUIHandler = GetComponentInChildren<GameOverviewUIHandler>();
        gameOverviewUIHandler.Initialize(gameHandler);

        playerUIHandler = GetComponentInChildren<PlayerUIHandler>();
        playerUIHandler.Initialize(gameHandler);

        winnerUIHandler = GetComponentInChildren<WinnerUIHandler>();
        winnerUIHandler.Initialize();
    }

    public void UpdateUI(int inputScore, int currentPlayerIndex) {
        playerUIHandler.UpdateUI(currentPlayerIndex);
        gameOverviewUIHandler.UpdateUI(inputScore);
    }

    public void OpenGameWindow() {
        menuUIHandler.gameObject.SetActive(false);
        gameOverviewUIHandler.gameObject.SetActive(true);

        playerUIHandler.DisableScoreEditing();
    }

    public void OpenMenuOverviewWindow() {
        menuUIHandler.gameObject.SetActive(true);
        gameOverviewUIHandler.gameObject.SetActive(false);
        
        playerUIHandler.EnableScoreEditing();
    }

    public bool IsManagingPlayersWindowOpen() {
        return menuUIHandler.gameObject.activeInHierarchy;
    }

    public void ShowWinner(string winnerName) {
        winnerUIHandler.ShowWinner(winnerName);
    }
}
