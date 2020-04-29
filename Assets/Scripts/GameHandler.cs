using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    UIHandler uiHandler;
    PlayerHandler playerHandler;

    public int startPlayerIndex { get; private set; } = 0;
    public int originalStarPlayerIndex { get; private set; } = 0;
    private int currentPlayerIndex = int.MaxValue;
    private int inputScore;
    public int scoreToWin { get; private set; } = 13;

    private void Awake() {
        playerHandler = new PlayerHandler(this);

        Debug.Log(scoreToWin);

        uiHandler = GetComponentInChildren<UIHandler>();
        uiHandler.Initialize(this);

        //finalRoundPlayers = playerHandler.GetFinalRoundPlayers();
        OpenAddPlayersWindow();
    }

    private void UpdateUI() {
        uiHandler.UpdateUI(inputScore, currentPlayerIndex);
    }

    public void StartGame() {
        if (GetPlayers().Count > 1) {
            uiHandler.OpenGameWindow();
        }

        currentPlayerIndex = startPlayerIndex;
        inputScore = 0;

        UpdateUI();
    }

    public void AddToInputScore(int bumpAmount) {
        inputScore = int.Parse(inputScore.ToString() + bumpAmount.ToString());
        UpdateUI();
    }

    public void RemoveLastDigitFromInputScore() {
        string scoreString = inputScore.ToString();
        if (scoreString.Length > 1) {
            inputScore = int.Parse(scoreString.Substring(0, scoreString.Length - 1));
        }
        else {
            inputScore = 0;
        }
        UpdateUI();
    }

    public void NextPlayer() {
        playerHandler.AddScoreToPlayer(inputScore, currentPlayerIndex);

        inputScore = 0;
        //CheckForGameOver();

        CheckForGameOver();

        UpdateUI();

        //if (!finalRound) {
        //    if (playerHandler.GetNextPlayerIndex(currentPlayerIndex) == startPlayerIndex) {
        //        List<int> playersInLeadIndeces;
        //        int playersInLead = playerHandler.PlayersInLead(out playersInLeadIndeces);
        //        if (playersInLead > 1) {
        //            //start finalRound
        //        }
        //        else if (playersInLead == 1) {
        //            Debug.Log("pili: " + playersInLeadIndeces[0]);
        //            SetWinner(playersInLeadIndeces[0]);
        //        }
        //        else {
        //            currentPlayerIndex = playerHandler.GetNextPlayerIndex(currentPlayerIndex);
        //        }
        //    }
        //    else {
        //        currentPlayerIndex = playerHandler.GetNextPlayerIndex(currentPlayerIndex);
        //    }
        //}
    }

    private void CheckForGameOver() {
        //if (!finalRound) {
        if (playerHandler.GetNextPlayerIndex(currentPlayerIndex) == startPlayerIndex) {
            List<int> playersInLeadIndices;
            int playersInLead = playerHandler.PlayersInLead(out playersInLeadIndices);
            if (playersInLead == 1) {
                SetWinner(playersInLeadIndices[0]);
            }
            else if (playersInLead > 1) {
                //finalRound = true;
                playerHandler.SetFinalRoundPlayers(playersInLeadIndices);
                startPlayerIndex = currentPlayerIndex = playerHandler.GetNextPlayerIndex(currentPlayerIndex);
            }
            else {
                currentPlayerIndex = playerHandler.GetNextPlayerIndex(currentPlayerIndex);
            }
        }
        else {
            currentPlayerIndex = playerHandler.GetNextPlayerIndex(currentPlayerIndex);
        }
        //}
    }

    private void SetWinner(int winnerIndex) {
        originalStarPlayerIndex = startPlayerIndex = currentPlayerIndex = winnerIndex;
        playerHandler.AddWin(currentPlayerIndex);
        playerHandler.ResetRound();
        //ResetData();
    }

    //private void CheckForGameOver() {

    //    if (!finalRound) {
    //        if (currentPlayerIndex == startPlayerIndex) {
    //            //if (more than one player with the same score above 13)
    //            //    finalRound = true;
    //            //    Save indeces for remaining players
    //            //else if (one player above 13)
    //            //    Set player as winner
    //        }
    //    }
    //    else {
    //        //if (more than one player has the highest score)
    //        //    Save those players in a list and play through them.
    //    }
    //    //    if (currentPlayerIndex == startPlayerIndex) {
    //    //        if (finalRound) {
    //    //            return true;
    //    //        }
    //    //        else {
    //    //            playerHandler.HasPlayersReachedLimit();
    //    //        }
    //    //    }
    //    //    return false;
    //}

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
