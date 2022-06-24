using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetManager : NetworkManager
{
    GameObject ball;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        GameObject player = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnStartServer()
    {
        base.OnStartHost();
        ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "MovPlat"));
        ball.name = "MovingPlatform";
        Debug.Log(ball);
        NetworkServer.Spawn(ball);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // destroy ball
        if (ball != null)
            NetworkServer.Destroy(ball);

        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);
    }
}

