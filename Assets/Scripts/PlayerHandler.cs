using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler
{
    List<PlayerData> players;
    GameHandler gameHandler;

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
            if (player.score < lowestValue) {
                lowestValue = player.score;
            }
        }

        return lowestValue;
    }

    public void AddScoreToPlayer(int score, int playerIndex) {
        players[playerIndex].score += score;
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

    //public bool HasPlayersReachedLimit() {
    //    foreach (PlayerData player in players) {
    //        if (player.score >= 13) {
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    public List<PlayerData> GetPlayers() {
        return players;
    }

    public int PlayersInLead(out List<int> playersInLeadIndeces) {
        //List<int> playerIndeces = new List<int>();
        playersInLeadIndeces = new List<int>();
        int playersInLead = 0;
        int currentMax = 0;
        for (int i = 0; i < players.Count; i++) {
            if (players[i].score >= gameHandler.scoreToWin) {
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
        //foreach (PlayerData player in players) {
        //    if (player.score >= 13) {
        //        if (player.score > currentMax) {
        //            currentMax = player.score;
        //            playersInLead = 1;
        //        }
        //        else if (player.score == currentMax) {
        //            playersInLead++;
        //        }
        //    }
        //}
        //playersInLeadIndeces = playerIndeces;
        return playersInLead;
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
