using UnityEngine;
using System.Collections;

public class ShadowMovement : MonoBehaviour {

	private Vector3 m_currentObjectPosition; 

	public GameObject m_shadow;
	private Vector3 m_currentShadowPosition;
	private Vector3 m_newShadowPosition;

	private Vector3 m_currentShadowScale;
	private Vector3 m_newShadowScale;

	void Start () 
	{
		/* Initializing the position of the object along with 
		 * the position and scale of the shadow */
		m_currentObjectPosition = transform.position; 
		m_currentShadowPosition = m_shadow.transform.position;
		m_currentShadowScale = m_shadow.transform.localScale;
	}

	void Update () 
	{
		float shift_x, shift_y;

		/* Determining the shift in position in between frames to apply to the shadow */
		shift_x = transform.position.x - m_currentObjectPosition.x;
		shift_y = transform.position.y - m_currentObjectPosition.y;

		/* Determining the factor by which the shadow size should be scaled 
		 * based on how much the object has moved along the z-axis */
		m_newShadowScale.x = m_currentShadowScale.x * ( m_currentObjectPosition.z / transform.position.z );
		m_newShadowScale.y = m_currentShadowScale.y * ( m_currentObjectPosition.z / transform.position.z );

		/* Using the shift factor to provide the new shadow position */
		m_newShadowPosition.x = m_currentShadowPosition.x + shift_x;
		m_newShadowPosition.y = m_currentShadowPosition.y + shift_y;
		m_newShadowPosition.z = m_shadow.transform.position.z;

		/* Assigning the object's current position to the variable */
		m_currentObjectPosition = transform.position;

		/* Assiging the shadow's current position & scale to the respective variables */
		m_currentShadowPosition = m_newShadowPosition;
		m_shadow.transform.position = m_currentShadowPosition;

		m_currentShadowScale = m_newShadowScale;
		m_shadow.transform.localScale = m_currentShadowScale;
	}
}
