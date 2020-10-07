using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{
    private void InstantiatePlayer()
    {
        GameObject player = Instantiate(spawnPrefabs[0]);
        NetworkServer.Spawn(player);
    }
    public override void OnServerAddPlayer(NetworkConnection connection, short playerControllerId)
    {
        GameObject player = Instantiate(spawnPrefabs[0],new Vector3(0,0,0), Quaternion.identity);
        NetworkServer.AddPlayerForConnection(connection,player,playerControllerId);
    }
}
