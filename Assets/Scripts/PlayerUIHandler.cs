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

    public void Initialize(GameHandler gameHandler) {
        players = gameHandler.GetPlayers();
        playerCount = players.Count;
        playerPrefab = (GameObject)Resources.Load("PlayerUIPrefab");
        playerLayout = GetComponentInChildren<VerticalLayoutGroup>();

        playerScoreUIHandlers = new List<PlayerScoreUIHandler>();
    }

    public void UpdateUI() {

        if (players.Count > playerCount) {
            for (int i = 0; i < players.Count - playerCount; i++) {
                CreatePlayerObject();
            }
            playerCount = players.Count;
        }

        //if (players.Count != playerCount) {
        //    playerCount = players.Count;

        //    Debug.Log("Gotta update the ui");
        //    DestroyPlayerObjects();
        //    //players = gameHandler.GetPlayers();
        //    CreatePlayerObjects();
        //}

        

        for (int i = 0; i < playerScoreUIHandlers.Count; i++) {
            playerScoreUIHandlers[i].UpdateTexts(players[i]);
        }
    }

    private void CreatePlayerObject() {
        GameObject newObject = Instantiate(playerPrefab, playerLayout.transform);
        newObject.GetComponent<PlayerScoreUIHandler>().Initialize();
        playerScoreUIHandlers.Add(newObject.GetComponent<PlayerScoreUIHandler>());
    }

    //private void DestroyPlayerObjects() {
    //        for (int i = playerLayout.transform.childCount-1; i > 0; i--) {
    //            Destroy(playerLayout.transform.GetChild(i));
    //        }
    //    //while (playerLayout.transform.childCount > 0) {
    //    //    Destroy(playerLayout.transform.GetChild(0).gameObject);
    //    //    Debug.Log("childCount: " + playerLayout.transform.childCount);
    //    //    yield return new WaitForEndOfFrame();
    //    //}

    //}

    //private void CreatePlayerObjects() {
    //    for (int i = 0; i < players.Count; i++) {
    //        GameObject newObject = Instantiate(playerPrefab, playerLayout.transform);
    //        newObject.GetComponent<PlayerScoreUIHandler>().Initialize();
    //    }
    //}
}
