using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    List<PlayerData> players;
    List<PlayerScoreUIHandler> playerScoreUIHandlers;
    GameObject playerPrefab;
    VerticalLayoutGroup playerLayout;
    int playerCount;
    GameHandler gameHandler;

    public void Initialize(GameHandler gameHandler) {
        this.gameHandler = gameHandler;

        players = gameHandler.GetPlayers();
        playerCount = players.Count;
        playerPrefab = (GameObject)Resources.Load("PlayerUIPrefab");
        playerLayout = GetComponentInChildren<VerticalLayoutGroup>();

        playerScoreUIHandlers = new List<PlayerScoreUIHandler>();
    }

    public void UpdateUI(int currentPlayerIndex) {


        if (players.Count > playerCount) {
            for (int i = playerCount; i < players.Count; i++) {
                CreatePlayerObject(players[i]);
            }
            playerCount = players.Count;
        }
        else if (players.Count < playerCount) {
            Debug.Log("Players.Count: " + players.Count + " - " + "playerCount: " + playerCount);
            for (int i = 0; i < playerCount - players.Count; i++) {
                DestroyPlayerObject(i);
            }
            playerCount = players.Count;
            print("new playerCount: " + playerCount);
        }

        List<int> mostWinsPlayerIndeces = gameHandler.GetPlayerIndecesWithMostWins();
        bool showRemovePlayerButtons = gameHandler.IsManagingPlayers();
        for (int i = 0; i < playerScoreUIHandlers.Count; i++) {
            bool isCurrentPlayer = (i == currentPlayerIndex);
            bool isStartingPlayer = (i == gameHandler.originalStartPlayerIndex);
            bool playerIsLeader = mostWinsPlayerIndeces.Contains(i);

            playerScoreUIHandlers[i].UpdateData(players[i], playerIsLeader, isCurrentPlayer, isStartingPlayer, showRemovePlayerButtons);
        }
    }

    private void CreatePlayerObject(PlayerData player) {
        GameObject newObject = Instantiate(playerPrefab, playerLayout.transform);
        newObject.GetComponent<PlayerScoreUIHandler>().Initialize(gameHandler, player);
        playerScoreUIHandlers.Add(newObject.GetComponent<PlayerScoreUIHandler>());
    }

    private void DestroyPlayerObject(int playerIndex) {
        GameObject objectToRemove = playerScoreUIHandlers[playerIndex].gameObject;
        playerScoreUIHandlers.RemoveAt(playerIndex);
        Destroy(objectToRemove);
    }

    public void DisableScoreEditing() {
        foreach (PlayerScoreUIHandler playerScoreUIHandler in playerScoreUIHandlers) {
            playerScoreUIHandler.DisableScoreEditing();

        }
    }

    public void EnableScoreEditing() {
        foreach (PlayerScoreUIHandler playerScoreUIHandler in playerScoreUIHandlers) {
            playerScoreUIHandler.EnableScoreEditing();

        }
    }
}
