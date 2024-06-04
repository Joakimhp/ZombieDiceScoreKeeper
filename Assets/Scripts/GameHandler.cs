using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Dynamic;

public class GameHandler : MonoBehaviour
{
    UIHandler[] uiHandlers;
    [SerializeField] PlayerHandler playerHandler;
    AudioHandler audioHandler;

    [HideInInspector] public int startPlayerIndex { get; private set; } = 0;
    [HideInInspector] public int originalStartPlayerIndex { get; private set; } = 0;
    private int currentPlayerIndex = int.MaxValue;
    private int inputScore;
    public int minimumRequiredScoreToWin { get; private set; } = 13;

    TextMeshProUGUI orientationText;
    public bool finalRound { get; private set; } = false;

    private void Awake() {
        playerHandler = new PlayerHandler(this);

        InitalizeComponentsInChildren();
        UpdateOrientation();

        OpenAddPlayersWindow();

        StartCoroutine(CheckScreenOrientation());
    }

    IEnumerator CheckScreenOrientation() {
        ScreenOrientation originalScreenOrientation = Screen.orientation;
        yield return new WaitUntil(() => originalScreenOrientation != Screen.orientation);
        UpdateOrientation();
        StartCoroutine(CheckScreenOrientation());
    }

    private void UpdateOrientation() {
        string orientationName = "N/A";
            if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight) {
                OpenLandscapeCanvas();
                orientationName = "Landscape";
            }
            else {
                OpenPortaitCanvas();
                orientationName = "Portrait";
            }

        orientationText.text = "Orientation: " + orientationName;
    }

    private void OpenPortaitCanvas() {
        uiHandlers[0].gameObject.SetActive(false);
        uiHandlers[1].gameObject.SetActive(true);
    }

    private void OpenLandscapeCanvas() {
        uiHandlers[0].gameObject.SetActive(true);
        uiHandlers[1].gameObject.SetActive(false);
    }

    private void InitalizeComponentsInChildren() {
        uiHandlers = GetComponentsInChildren<UIHandler>();
        foreach (UIHandler uiHandler in uiHandlers) {
            uiHandler.Initialize(this);
        }

        orientationText = GetComponentsInChildren<Canvas>()[GetComponentsInChildren<Canvas>().Length - 1].GetComponentInChildren<TextMeshProUGUI>();
    }

    private void UpdateUI() {
        foreach (UIHandler uiHandler in uiHandlers) {
            uiHandler.UpdateUI(inputScore, currentPlayerIndex);
        }
    }

    public void StartGame() {
        if (GetPlayers().Count > 1) {
            foreach (UIHandler uiHandler in uiHandlers) {
                uiHandler.OpenGameWindow();
            }
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

        if (playerHandler.GetPlayers()[currentPlayerIndex].score >= minimumRequiredScoreToWin) {
            finalRound = true;
        }

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
        originalStartPlayerIndex = startPlayerIndex = currentPlayerIndex = winnerIndex;
        playerHandler.AddWin(currentPlayerIndex);
        playerHandler.ResetRound();
        finalRound = false;

        string winnerName = playerHandler.GetPlayers()[winnerIndex].name;
        foreach (UIHandler uiHandler in uiHandlers) {
            if (uiHandler.gameObject.activeInHierarchy) {
                uiHandler.ShowWinner(winnerName);
            }
        }
        //ResetData();
    }

    public List<int> GetPlayerIndecesWithMostWins() {
        return playerHandler.GetPlayerIndecesWithMostWins();
    }

    public void OpenAddPlayersWindow() {
        foreach (UIHandler uiHandler in uiHandlers) {
            uiHandler.OpenMenuOverviewWindow();
        }

        UpdateUI();
    }

    public void AddPlayer(string playerName) {
        playerHandler.AddPlayer(playerName);
        UpdateUI();
    }

    public void RemovePlayer(PlayerData player) {
        playerHandler.RemovePlayer(player);

        if (CheckIfPlayerIsStartingPlayer(player)) {
            currentPlayerIndex = originalStartPlayerIndex = startPlayerIndex = playerHandler.GetNextPlayerIndex(startPlayerIndex);
        }

        UpdateUI();
    }

    public bool IsManagingPlayers() {
        foreach (UIHandler uiHandler in uiHandlers) {
            if (uiHandler.IsManagingPlayersWindowOpen()) {
                return true;
            }
        }
        return false;
    }

    public void EditPlayerScore(string playerName, int playerScore) {
        playerHandler.EditPlayerScore(playerName, playerScore);
    }

    public bool CheckIfPlayerIsStartingPlayer(PlayerData player) {
        return currentPlayerIndex == originalStartPlayerIndex;
    }

    public List<PlayerData> GetPlayers() {
        return playerHandler.GetPlayers();
    }
}
