using UnityEngine;
using System.Collections;

public class PreciseRotatorScript : MonoBehaviour
{
    public GameObject[] m_axies;
    public GameObject m_controlledObject;

    public void Init(GameObject controlledObject, Camera camera)
    {
        this.m_controlledObject = controlledObject;
        foreach (GameObject axie in m_axies)
        {
            AxiesRotatorScript rotatorScript = (AxiesRotatorScript)axie.GetComponent("AxiesRotatorScript");
            rotatorScript.m_controlledObject = controlledObject;
            rotatorScript.SetCamera(camera);
        }
        m_axies[0].rigidbody.transform.rotation = Quaternion.LookRotation(controlledObject.transform.forward);
        m_axies[1].rigidbody.transform.rotation = Quaternion.LookRotation(controlledObject.transform.up);
        m_axies[2].rigidbody.transform.rotation = Quaternion.LookRotation(controlledObject.transform.right);
    }

    public void Rotate(Vector2 mousePosition)
    {
        foreach (GameObject axie in m_axies)
        {
            AxiesRotatorScript rotatorScript = (AxiesRotatorScript)axie.GetComponent("AxiesRotatorScript");
            rotatorScript.Rotate(mousePosition);
        }
    }
}
