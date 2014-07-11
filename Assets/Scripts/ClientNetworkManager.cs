using UnityEngine;
using System.Collections;

public class ClientNetworkManager : MonoBehaviour 
{
    private bool m_isConnected = false;

    void OnGUI()
    {
        if(!m_isConnected)
        {
            if (GUI.Button(new Rect(0, 0, 400, 200), "Connect to server"))
            {
                Network.Connect("127.0.0.1", 9898);
            }
        }
        else if(GUI.Button(new Rect(0, 0, 400, 200), "Disconnect from server"))
        {
            Network.Disconnect();
        }
    }

    void OnConnectedToServer()
    {
        m_isConnected = true;
    }

    void OnDisconnectedFromServer()
    {
        m_isConnected = false;
        Application.LoadLevel(Application.loadedLevel);
    }  
}
