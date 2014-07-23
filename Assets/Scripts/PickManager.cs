using UnityEngine;
using System.Collections;

public class PickManager : MonoBehaviour {
	static Camera s_camera;
	static GameObject s_gameObject;
	static GameObject s_preciseMover;
	static GameObject s_preciseRotator;
	static SpringJoint s_springJoint;
	static float s_drag = 10.0f;
	static float s_angularDrag = 5.0f;
    static int s_pickRange;
	static bool s_isKenematic;
    static bool s_useGravity;
    static bool s_freezeRotation;

    public static void PickManagerConfigure(Camera mainCamera, int range = 100)
    {
        s_camera = mainCamera;
        s_pickRange = range;
        //Debug.Log("You have configure the pick manager.");
    }

    public static GameObject StartPick(Vector2 mousePosition, string tag = null)
    {
        if (s_camera == null)
        {
            Debug.LogError("You havent set the camera yet.");
            return null;
        }

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Rigidbody picker");
        if (gameObjects.Length != 0)
        {
            Debug.LogError("You have a picker in the scene.");
            return null;
        }

        RaycastHit hit;
        s_gameObject = null;
        if (!Physics.Raycast(s_camera.ScreenPointToRay(mousePosition), out hit, s_pickRange))
        {
            return null;
        }
        else if (!hit.rigidbody)
        {
            return null;
        }
        else if (tag != null && !tag.Equals(hit.rigidbody.tag))
        {
            return null;
        }    
        else
        {
            GameObject dragger = new GameObject("Rigidbody dragger");
            dragger.tag = "Rigidbody picker";
            s_springJoint = (SpringJoint)dragger.AddComponent("SpringJoint");
            dragger.rigidbody.isKinematic = true;
            s_isKenematic = hit.rigidbody.isKinematic;
            hit.rigidbody.isKinematic = false;
            s_useGravity = hit.rigidbody.useGravity;
            hit.rigidbody.useGravity = false;
            s_freezeRotation = hit.rigidbody.freezeRotation;
            hit.rigidbody.freezeRotation = true;
            s_springJoint.transform.position = hit.point;
            s_springJoint.anchor = Vector3.zero;
            s_springJoint.spring = 50.0f;
            s_springJoint.damper = 5.0f;
            s_springJoint.maxDistance = 0.2f;
            s_springJoint.connectedBody = hit.rigidbody;
            s_gameObject = hit.rigidbody.gameObject;
            Debug.Log("You have start picking.");
            return s_gameObject;
        }
    }

    public static void StopPick()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Rigidbody picker");
        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
            
        if (s_gameObject)
        {
            s_gameObject.rigidbody.angularDrag = 0;
            s_gameObject.rigidbody.drag = 0;
            s_gameObject.rigidbody.isKinematic = s_isKenematic;
            s_gameObject.rigidbody.useGravity = s_useGravity;
            s_gameObject.rigidbody.freezeRotation = s_freezeRotation;
        }
        s_gameObject = null;
        s_springJoint = null;
        Debug.Log("You have stop picking.");
    }

    public static void MovePick(Vector2 m_mousePosition)
    {
        if (s_gameObject == null)
        {
            //Debug.LogError("You havent chosen an object to be moved.");
            return;
        }
        if (s_springJoint == null)
        {
            //Debug.LogError("The sprintJoint hasn't being initialized.");
            return;
        }
        s_springJoint.connectedBody.drag = s_drag;
        s_springJoint.connectedBody.angularDrag = s_angularDrag;
        s_springJoint.transform.position = s_camera.ScreenPointToRay(m_mousePosition).GetPoint(15);
    }

	public static GameObject StartPreciseRotate(Vector2 mousePosition, string tag=null)
	{
		if (s_camera == null) 
        {
			Debug.LogError("You havent set the camera yet.");
			return null;
		}
		
		GameObject[] gameObjects =  GameObject.FindGameObjectsWithTag ("Precise rotator");
		if (gameObjects.Length != 0) 
        {
			Debug.LogError("You have a Precise rotator in the scene.");
			return null;
		}
		
		RaycastHit hit;
		s_gameObject = null;
		if (!Physics.Raycast (s_camera.ScreenPointToRay (mousePosition), out hit, s_pickRange))
        {
            return null;
        }	
		else if (!hit.rigidbody)
        {
            return null;
        }
		else if (tag != null && !tag.Equals (hit.rigidbody.tag))
        {
            return null;
        }	
		else 
        {
			s_preciseRotator = Instantiate(Resources.Load("PreciseRotator")) as GameObject;
			s_preciseRotator.transform.position = hit.rigidbody.transform.position;
            PreciseRotatorScript rotatorScript = (PreciseRotatorScript)s_preciseRotator.GetComponent("PreciseRotatorScript");
			rotatorScript.Init(hit.rigidbody.gameObject,s_camera);
			s_gameObject = hit.rigidbody.gameObject;
			s_isKenematic = hit.rigidbody.isKinematic;
			hit.rigidbody.isKinematic=true;
            s_useGravity = hit.rigidbody.useGravity;
            hit.rigidbody.useGravity = false;
            s_freezeRotation = hit.rigidbody.freezeRotation;
            hit.rigidbody.freezeRotation = true;
			return hit.rigidbody.gameObject;
		}
	}

    public static void StopPreciseRotate()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Precise rotator");
        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
            
        if (s_gameObject)
        {
            s_gameObject.rigidbody.isKinematic = s_isKenematic;
            s_gameObject.rigidbody.useGravity = s_useGravity;
            s_gameObject.rigidbody.freezeRotation = s_freezeRotation;
        }     
        s_gameObject = null;
        s_preciseMover = null;
        Debug.Log("You have stop picking.");
    }

	public static void PreciseMoveRotate(Vector2 mousePosition)
	{
		if (s_gameObject == null) 
        {
			//Debug.LogError("You havent chosen an object to be roated.");
			return;
		}
		if (s_preciseRotator == null) 
        {
			//Debug.LogError("The preciseRotator hasn't being initialized.");
			return;
		}
		PreciseRotatorScript rotatorScript = (PreciseRotatorScript)s_preciseRotator.GetComponent("PreciseRotatorScript");
		rotatorScript.Rotate (mousePosition);
	}

	public static GameObject StartPrecisePick(Vector2 mousePosition, string tag=null)
	{
		if (s_camera == null) 
        {
			Debug.LogError("You havent set the camera yet.");
			return null;
		}
		
		GameObject[] gameObjects =  GameObject.FindGameObjectsWithTag ("Precise mover");
		if (gameObjects.Length != 0) 
        {
			Debug.LogError("You have a precise picker in the scene.");
			return null;
		}
		
		RaycastHit hit;
		s_gameObject = null;
		if (!Physics.Raycast (s_camera.ScreenPointToRay (mousePosition), out hit, s_pickRange))
        {
			return null;
        }
		else if (!hit.rigidbody || hit.rigidbody.isKinematic)
        {
			return null;
        }
		else if (tag != null && !tag.Equals (hit.rigidbody.tag))
        {
			return null;
        }
		else 
        {
			s_preciseMover = Instantiate(Resources.Load("PreciseMover")) as GameObject;
            PrecisePickScript pickScript = (PrecisePickScript)s_preciseMover.GetComponent("PrecisePickScript");
			pickScript.m_gameObject = hit.rigidbody.gameObject;
			pickScript.Init(s_camera);
			s_gameObject = hit.rigidbody.gameObject;
			s_isKenematic = hit.rigidbody.isKinematic;
			hit.rigidbody.isKinematic=true;
            s_useGravity = hit.rigidbody.useGravity;
            hit.rigidbody.useGravity = false;
            s_freezeRotation = hit.rigidbody.freezeRotation;
            hit.rigidbody.freezeRotation = true;
			return hit.rigidbody.gameObject;
		}
	}

    public static void StopPrecisePick()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Precise mover");
        for (var i = 0; i < gameObjects.Length; i++)
        { 
            Destroy(gameObjects[i]);
        }

        if (s_gameObject)
        {
            s_gameObject.rigidbody.isKinematic = s_isKenematic;
            s_gameObject.rigidbody.useGravity = s_useGravity;
            s_gameObject.rigidbody.freezeRotation = s_freezeRotation;
        }
            
        s_gameObject = null;
        s_preciseMover = null;
        Debug.Log("You have stop pick.");
    }

	public static void PreciseMovePick(Vector2 mousePosition)
	{
		if (s_gameObject == null) 
        {
			//Debug.LogError("You havent chosen an object to be moved.");
			return;
		}
		if (s_preciseMover == null) 
        {
			//Debug.LogError("The preciseMover hasn't being initialized.");
			return;
		}
        PrecisePickScript pickScript = (PrecisePickScript)s_preciseMover.GetComponent("PrecisePickScript");
		pickScript.Drag (mousePosition);
	}

}
