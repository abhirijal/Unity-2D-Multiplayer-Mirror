using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LvlOneNetManager : NetworkManager
{
    GameObject ball;
    public GameObject blockCollid;
    [Scene] [SerializeField] public string lvloneScene;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        GameObject player = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnStartServer()
    {
        base.OnStartHost();
        ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "BlockGrid"));
        // ball.name = "BlockGrid";
        Debug.Log(ball);
        NetworkServer.Spawn(ball);
        blockCollid = GameObject.Find("Block");
        // OnClickStartScene();
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        // destroy ball
        if (ball != null)
            NetworkServer.Destroy(ball);
    }

    public void OnClickStartScene() {
        ServerChangeScene(lvloneScene);
    }
}


