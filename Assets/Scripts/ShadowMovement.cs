using UnityEngine;
using System.Collections;

public class ShadowMovement : MonoBehaviour {

	private Vector3 m_currentObjectPosition;

	public GameObject m_shadow;
	private Vector3 m_currentShadowPosition;
	private Vector3 m_newShadowPosition;

	private Vector3 m_currentShadowScale;
	private Vector3 m_newShadowScale;

	// Use this for initialization
	void Start () 
	{
		m_currentObjectPosition = transform.position;
		m_currentShadowPosition = m_shadow.transform.position;
		m_currentShadowScale = m_shadow.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float shift_x, shift_y;

		shift_x = transform.position.x - m_currentObjectPosition.x;
		shift_y = transform.position.y - m_currentObjectPosition.y;

		m_newShadowScale.x = m_currentShadowScale.x * ( m_currentObjectPosition.z / transform.position.z );
		m_newShadowScale.y = m_currentShadowScale.y * ( m_currentObjectPosition.z / transform.position.z );
		
		m_currentObjectPosition = transform.position;

		m_newShadowPosition.x = m_currentShadowPosition.x + shift_x;
		m_newShadowPosition.y = m_currentShadowPosition.y + shift_y;
		m_newShadowPosition.z = m_shadow.transform.position.z;

		m_shadow.transform.position = m_newShadowPosition;
		m_currentShadowPosition = m_shadow.transform.position;

		m_currentShadowScale = m_newShadowScale;
		m_shadow.transform.localScale = m_currentShadowScale;

	}
}
