using UnityEngine;
using System.Collections;

public class PreciseRotatorScript2 : MonoBehaviour {
	public GameObject[] axies;
	public GameObject controlledObject;

	public void Init(GameObject controlledObject, Camera camera)
	{
		foreach (GameObject axie in axies) 
		{
			AxiesRotatorScript rotatorScript = (AxiesRotatorScript)axie.GetComponent("AxiesRotator");
			this.controlledObject = controlledObject;
			rotatorScript.m_controlledObject = controlledObject;
			rotatorScript.SetCamera(camera);
		}
		axies[0].rigidbody.transform.rotation = Quaternion.LookRotation (controlledObject.transform.forward);
		axies[1].rigidbody.transform.rotation = Quaternion.LookRotation (controlledObject.transform.up);
		axies[2].rigidbody.transform.rotation = Quaternion.LookRotation (controlledObject.transform.right);
	}

	public void Rotate(Vector2 mousePosition)
	{
		foreach (GameObject axie in axies) 
		{
			AxiesRotatorScript rotatorScript = (AxiesRotatorScript)axie.GetComponent("AxiesRotator");
			rotatorScript.Rotate(mousePosition);
		}
	}
}
