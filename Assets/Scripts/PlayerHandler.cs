using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler
{
    List<PlayerData> players;

    public PlayerHandler() {
        players = new List<PlayerData>();
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
        Debug.Log("Added player: " + players[players.Count - 1].name + " with the score: " + players[players.Count - 1].score);
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

    public List<PlayerData> GetPlayers() {
        return players;
    }
}

public class PlayerData
{
    public string name;
    public int score;
    public int wins;

    public PlayerData(string name, int score) {
        this.name = name;
        this.score = score;
        wins = 0;
    }
}
