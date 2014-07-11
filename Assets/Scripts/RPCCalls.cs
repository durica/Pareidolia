using UnityEngine;
using System.Collections;

public class RPCCalls : MonoBehaviour 
{
    [RPC]
    void SpawnObject(string objectName, NetworkViewID viewId, Vector3 position, Quaternion rotation)
    {
        Debug.Log("Server: Spawn Object");
        GameObject newObj = (GameObject)Object.Instantiate(Resources.Load(objectName), position, rotation);
        NetworkView netView = newObj.GetComponent<NetworkView>();
        netView.viewID = viewId;
        newObj.transform.localScale = new Vector3(1, 1, 1);
    }
}
