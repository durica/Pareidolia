using UnityEngine;
using System.Collections;

public class PrecisePickScript : MonoBehaviour
{
    public Rigidbody[] m_axies;
    public GameObject m_gameObject;
    public int m_pickRange = 100;

    public void Init(Camera camera)
    {
        m_axies[0].rigidbody.transform.rotation = Quaternion.LookRotation(m_gameObject.transform.forward);
        m_axies[1].rigidbody.transform.rotation = Quaternion.LookRotation(m_gameObject.transform.up);
        m_axies[2].rigidbody.transform.rotation = Quaternion.LookRotation(m_gameObject.transform.right);

        m_axies[0].rigidbody.transform.position = m_gameObject.transform.position + m_gameObject.transform.forward;
        m_axies[1].rigidbody.transform.position = m_gameObject.transform.position + m_gameObject.transform.up;
        m_axies[2].rigidbody.transform.position = m_gameObject.transform.position + m_gameObject.transform.right;

        SetCamera(camera);
    }

    void SetCamera(Camera camera)
    {
        foreach (Rigidbody axie in m_axies)
        {
            AxiesPickScript pickSciprt = (AxiesPickScript)axie.gameObject.GetComponent("AxiesPickScript");
            pickSciprt.SetCamera(camera);
        }
    }

    public void ProcessMove(Vector3 move)
    {
        foreach (Rigidbody axie in m_axies)
        {
            axie.rigidbody.gameObject.transform.position += move;
        }
        m_gameObject.transform.position += move;
    }

    public void Drag(Vector2 mousePosition)
    {
        foreach (Rigidbody axie in m_axies)
        {
            AxiesPickScript pickScript = (AxiesPickScript)axie.gameObject.GetComponent("AxiesPickScript");
            if (pickScript.IsChosen(mousePosition))
            {
                pickScript.Drag(mousePosition);
            }
        }
    }

    public GameObject ReturnGameObject()
    {
        return m_gameObject;
    }
}
