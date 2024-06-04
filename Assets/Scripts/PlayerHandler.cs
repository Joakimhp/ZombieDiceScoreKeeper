using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerHandler
{
    [SerializeField]private List<PlayerData> players;
    private GameHandler gameHandler;

    public PlayerHandler(GameHandler gameHandler) {
        players = new List<PlayerData>();
        this.gameHandler = gameHandler;
    }

    public void AddPlayer(string playerName) {
        foreach (PlayerData player in players) {
            if (player.name == playerName) {
                return;
            }
        }

        PlayerData newPlayer;
        if (players.Count == 0) {
            newPlayer = new PlayerData(playerName, 0);
        }
        else {
            newPlayer = new PlayerData(playerName, GetLowestPlayerScore());
        }
        players.Add(newPlayer);
    }

    private int GetLowestPlayerScore() {
        int lowestValue = int.MaxValue;

        foreach (PlayerData player in players) {
            if (player.isPlaying && player.score < lowestValue) {
                lowestValue = player.score;
            }
        }

        return lowestValue;
    }

    public void RemovePlayer(PlayerData player) {
        players.Remove(player);
    }

    public void AddScoreToPlayer(int score, int playerIndex) {
        players[playerIndex].score += score;
    }

    public void EditPlayerScore(string playerName, int newScore) {
        foreach (PlayerData player in players) {
            if(player.name == playerName) {
                player.score = newScore;
            }
        }
    }

    public int GetNextPlayerIndex(int currentIndex) {
        int index = currentIndex;
        if (currentIndex != players.Count - 1) {
            index = GetNextPlayingPlayerIndex(currentIndex + 1, players.Count);
            if (index == 0) {
                index = GetNextPlayingPlayerIndex(0, currentIndex);
            }
        }
        else {
            index = GetNextPlayingPlayerIndex(0, players.Count);
        }

        return index;
    }

    private int GetNextPlayingPlayerIndex(int startIndex, int endIndex) {
        for (int i = startIndex; i < endIndex; i++) {
            if (players[i].isPlaying) {
                return i;
            }
        }

        return 0;
    }

    public List<int> GetPlayerIndecesWithMostWins() {
        int maxWins = 0;
        List<int> maxValueIndeces = new List<int>();
        for (int i = 0; i < players.Count; i++) {
            if (players[i].wins < maxWins || players[i].wins == 0) {
                continue;
            }

            if (players[i].wins > maxWins) {
                maxValueIndeces.Clear();
                maxWins = players[i].wins;
            }
            maxValueIndeces.Add(i);
        }
        return maxValueIndeces;
    }

    public List<PlayerData> GetPlayers() {
        return players;
    }

    public int PlayersInLead(out List<int> playersInLeadIndeces) {
        playersInLeadIndeces = new List<int>();
        int playersInLead = 0;
        int currentMax = 0;
        for (int i = 0; i < players.Count; i++) {
            if (players[i].score >= gameHandler.minimumRequiredScoreToWin) {
                if (players[i].score > currentMax) {
                    currentMax = players[i].score;
                    playersInLead = 1;
                    playersInLeadIndeces.Clear();
                    playersInLeadIndeces.Add(i);
                }
                else if (players[i].score == currentMax) {
                    playersInLead++;
                    playersInLeadIndeces.Add(i);
                }
            }
        }

        return playersInLead;
    }

    public int GetPlayerIndex(PlayerData player) {
        for (int i = 0; i < players.Count; i++) {
            if(players[i] == player) {
                return i;
            }
        }

        return 0;
    }

    public void SetFinalRoundPlayers(List<int> finalRoundPlayerIndeces) {
        foreach (PlayerData player in players) {
            player.isPlaying = false;
        }
        for (int finalPlayerIndex = 0; finalPlayerIndex < finalRoundPlayerIndeces.Count; finalPlayerIndex++) {
            players[finalRoundPlayerIndeces[finalPlayerIndex]].isPlaying = true;
        }
    }

    public void AddWin(int winnerIndex) {
        players[winnerIndex].wins++;
    }

    public void ResetRound() {
        foreach (PlayerData player in players) {
            player.score = 0;
            player.isPlaying = true;
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public string name;
    public int score;
    public int wins;
    public bool isPlaying;

    public PlayerData(string name, int score) {
        this.name = name;
        this.score = score;
        wins = 0;
        isPlaying = true;
    }
}
