using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersProvider
{
    public List<Player> GetPlayers()
    {
        var players = new List<Player>();

        GetPlayersFromTeam(players, GameObject.Find("Teams/Team1").transform, 0);
        GetPlayersFromTeam(players, GameObject.Find("Teams/Team2").transform, 1);

        return players;
    }

    private void GetPlayersFromTeam(List<Player> players, Transform teamContainer, byte team)
    {
        for (int i = 0; i < teamContainer.childCount; i++)
        {
            var child = teamContainer.GetChild(i);
            var playerView = child.GetComponent<PlayerView>();
            var direction = team == 0 ? PlayerDirection.Up : PlayerDirection.Bottom;

            var ballTakeoverCollider = playerView.GetBallTakeoverCollider();

            var data = new Player(playerView, team, (byte)i, direction, ballTakeoverCollider);
            players.Add(data);
        }
    }
}
