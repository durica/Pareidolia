using UnityEngine;
using System.Collections;

public class NetworkObject : MonoBehaviour 
{
    public Transform m_target;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_target)
        {
            m_target.position = Vector3.Lerp(m_target.position, transform.position, Time.deltaTime * 10);
        }
	}
}
