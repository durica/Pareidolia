using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ServerNetworkManager : MonoBehaviour 
{
    public List<ServerObject> m_networkObjects;
    public static ServerNetworkManager Instance = null;

    private bool m_hasClientConnection = false;
    private NetworkPlayer? m_clientConnection = null;


    void Awake()
    {
        if(ServerNetworkManager.Instance == null)
        {
            ServerNetworkManager.Instance = this;
            Network.InitializeServer(1, 9898, false);
        }
        else
        {
            Debug.LogError("Multiple ServerNetworkManagers in the scene.");
        }
    }

	void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Client connected to server.");
        m_hasClientConnection = true;
        m_clientConnection = player;

        foreach(ServerObject obj in m_networkObjects)
        {
            NetworkView netView = obj.GetComponent<NetworkView>();
            networkView.RPC("SpawnObject", player, obj.m_clientPrefab.name, netView.viewID, obj.transform.position, obj.transform.rotation);
        }
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("Client disconnected from server.");
        m_hasClientConnection = false;
        m_clientConnection = null;
    }

    void OnServerInitialized()
    {
        Debug.Log("Server initialized and ready");
    }
}
