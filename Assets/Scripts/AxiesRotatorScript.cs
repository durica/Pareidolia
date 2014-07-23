using UnityEngine;
using System.Collections;

public class AxiesRotatorScript : MonoBehaviour
{
    public GameObject m_controlledObject;
    Camera m_camera;    
    Vector3 m_startPosition;
    bool m_start = false;

    public void Rotate(Vector2 mousePosition)
    {
        RaycastHit hit;
        Physics.Raycast(m_camera.ScreenPointToRay(mousePosition), out hit, 200);
        if (hit.rigidbody == null || hit.rigidbody.gameObject != this.gameObject)
        {
            m_start = true;
            return;
        }
        if (m_start)
        {
            m_start = false;
            m_startPosition = hit.point;
        }
        else
        {
            Vector3 endPosition = hit.point;
            Vector3 startLine = m_startPosition - m_controlledObject.rigidbody.transform.position;
            Vector3 endLine = endPosition - m_controlledObject.rigidbody.transform.position;
            startLine = startLine - Vector3.Dot(startLine, gameObject.rigidbody.transform.forward) * gameObject.rigidbody.transform.forward.normalized;
            endLine = endLine - Vector3.Dot(endLine, gameObject.rigidbody.transform.forward) * gameObject.rigidbody.transform.forward.normalized;
            float angle = Vector3.Angle(startLine, endLine);
            if (Vector3.Dot(gameObject.rigidbody.transform.forward, Vector3.Cross(startLine, endLine)) < 0)
                angle = -1 * angle;
            m_controlledObject.rigidbody.transform.RotateAround(m_controlledObject.rigidbody.transform.position, gameObject.rigidbody.transform.forward, angle);
            m_startPosition = endPosition;
        }
    }

    public void SetCamera(Camera camera)
    {
        this.m_camera = camera;
    }
}


