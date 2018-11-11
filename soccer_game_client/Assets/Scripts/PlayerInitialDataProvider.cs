using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitialDataProvider
{
    public List<PlayerInitialData> GetPlayers()
    {
        var players = new List<PlayerInitialData>();

        GetPlayersFromTeam(players, GameObject.Find("Teams/Team1").transform, 0);
        GetPlayersFromTeam(players, GameObject.Find("Teams/Team2").transform, 1);

        return players;
    }

    private void GetPlayersFromTeam(List<PlayerInitialData> players, Transform teamContainer, byte team)
    {
        for (int i = 0; i < teamContainer.childCount; i++)
        {
            var child = teamContainer.GetChild(i);
            var playerView = child.GetComponent<PlayerView>();

            var data = new PlayerInitialData();
            data.Team = team;
            data.Index = (byte)i;
            data.Position = playerView.transform.position;
            data.Direction = team == 0 ? PlayerDirection.Up : PlayerDirection.Bottom;

            players.Add(data);
        }
    }
}
