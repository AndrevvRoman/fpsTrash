using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CustomNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        var currentPlayerCount = NetworkServer.connections.Count;

        if(currentPlayerCount <= startPositions.Count)
        {
            GameObject pl = Instantiate(playerPrefab, startPositions[currentPlayerCount - 1].position, startPositions[currentPlayerCount - 1].rotation);
            if (currentPlayerCount == 1) //в сессии никого нет
            {
                pl.GetComponent<PlayerTeam>().SetTeam(Team.Blue);
                Debug.Log("start pos Blue");
                pl.GetComponent<PlayerTeam>().startPos = startPositions[currentPlayerCount - 1];
            }
            else if (currentPlayerCount == 2) //один игрок уже есть
            {
                pl.GetComponent<PlayerTeam>().SetTeam(Team.Red);
                Debug.Log("start pos red");
                pl.GetComponent<PlayerTeam>().startPos = startPositions[currentPlayerCount - 1];
            }
            NetworkServer.AddPlayerForConnection(conn, pl);
        }
        else
        {
            conn.Disconnect();
        }
    }
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
    }
}
