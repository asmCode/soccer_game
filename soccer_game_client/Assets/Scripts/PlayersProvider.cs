using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersProvider
{
    public List<Player> GetPlayers(Match match)
    {
        var players = new List<Player>();

        GetPlayersFromTeam(players, GameObject.Find("Teams/Team1").transform, 0, match);
        GetPlayersFromTeam(players, GameObject.Find("Teams/Team2").transform, 1, match);

        return players;
    }

    private void GetPlayersFromTeam(List<Player> players, Transform teamContainer, byte team, Match match)
    {
        for (int i = 0; i < teamContainer.childCount; i++)
        {
            var child = teamContainer.GetChild(i);
            var playerView = child.GetComponent<PlayerView>();
            var direction = team == 0 ? PlayerDirection.Up : PlayerDirection.Bottom;

            var ballTakeoverCollider = playerView.GetBallTakeoverCollider();

            var data = new Player(playerView, team, (byte)i, direction, ballTakeoverCollider, match);
            players.Add(data);
        }
    }
}
