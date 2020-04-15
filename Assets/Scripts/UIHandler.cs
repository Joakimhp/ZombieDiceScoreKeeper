using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    MenuUIHandler menuUIHandler;
    GameOverviewUIHandler gameOverviewUIHandler;
    PlayerUIHandler playerUIHandler;


    public void Initialize(GameHandler gameHandler) {
        menuUIHandler = GetComponentInChildren<MenuUIHandler>();
        menuUIHandler.Initialize(gameHandler);

        gameOverviewUIHandler = GetComponentInChildren<GameOverviewUIHandler>();
        gameOverviewUIHandler.Initialize();

        playerUIHandler = GetComponentInChildren<PlayerUIHandler>();
        playerUIHandler.Initialize(gameHandler);
    }

    public void UpdateUI() {
        playerUIHandler.UpdateUI();
    }

    public void OpenGameWindow() {
        menuUIHandler.gameObject.SetActive(false);
        gameOverviewUIHandler.gameObject.SetActive(true);
    }

    public void OpenMenuOverviewWindow() {
        menuUIHandler.gameObject.SetActive(true);
        gameOverviewUIHandler.gameObject.SetActive(false);
    }
}
