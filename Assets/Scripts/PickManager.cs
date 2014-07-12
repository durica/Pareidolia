using UnityEngine;
using System.Collections;

public class PickManager : MonoBehaviour {
	static Camera camera;
	static GameObject gameObject;
	static int pickRange;
	static SpringJoint springJoint;
	static float drag = 10.0f;
	static float angularDrag = 5.0f;
	
	public static void PickManagerConfigure(Camera mainCamera, int range = 100){
		camera = mainCamera;
		pickRange = range;
		Debug.Log("You have configure the pick manager.");
	}
	
	public static GameObject StartPick(Vector2 mousePosition, string tag=null){
		if (camera == null) {
			Debug.LogError("You havent set the camera yet.");
			return null;
		}
		
		GameObject[] gameObjects =  GameObject.FindGameObjectsWithTag ("Rigidbody picker");
		if (gameObjects.Length != 0) {
			Debug.LogError("You have a picker in the scene.");
			return null;
		}
		
		RaycastHit hit;
		gameObject = null;
		if (!Physics.Raycast (camera.ScreenPointToRay (mousePosition), out hit, pickRange))
			return null;
		else if (!hit.rigidbody || hit.rigidbody.isKinematic)
			return null;
		else if (tag != null && !tag.Equals (hit.rigidbody.tag))
			return null;
		else {
			GameObject dragger = new GameObject("Rigidbody dragger");
			dragger.tag = "Rigidbody picker";
			springJoint = (SpringJoint)dragger.AddComponent ("SpringJoint");
			dragger.rigidbody.isKinematic = true;
			
			springJoint.transform.position = hit.point;
			springJoint.anchor = Vector3.zero;
			springJoint.spring = 50.0f;
			springJoint.damper = 5.0f;
			springJoint.maxDistance = 0.2f;
			springJoint.connectedBody = hit.rigidbody;
			gameObject = hit.rigidbody.gameObject;
			Debug.Log("You have start picking.");
			return gameObject;
		}
	}
	
	public static void StopPick(){
		GameObject[] gameObjects =  GameObject.FindGameObjectsWithTag ("Rigidbody picker");
		for(var i = 0 ; i < gameObjects.Length ; i ++)
			Destroy(gameObjects[i]);
		gameObject.rigidbody.angularDrag = 0;
		gameObject.rigidbody.drag = 0;
		gameObject = null;
		springJoint = null;
		Debug.Log("You have stop picking.");
	} 
	
	public static void MovePick(Vector2 mousePosition){
		if (gameObject == null) {
			Debug.LogError("You havent chosen an object to be moved.");
			return;
		}
		if (springJoint == null) {
			Debug.LogError("The sprintJoint hasn't being initialized.");
			return;
		}
		springJoint.connectedBody.drag = drag;
		springJoint.connectedBody.angularDrag = angularDrag;
		springJoint.transform.position = camera.ScreenPointToRay(mousePosition).GetPoint(15);
	}
}
