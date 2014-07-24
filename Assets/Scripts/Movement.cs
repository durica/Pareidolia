using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float m_zShift;
	public float m_xShift;
	int x;

	
	// Use this for initialization
	void Start () 
	{
		x=0;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (x < 3000)
		{
			m_zShift = transform.position.z - 0.01f;
			m_xShift = transform.position.x;
			x++;
		}
		if (x > 3000)
		{
			m_zShift = transform.position.z + 0.01f;
			m_xShift = transform.position.x + 0.01f;
		}

		transform.position = new Vector3(m_xShift,transform.position.y,m_zShift);
		x++;

	}
}

