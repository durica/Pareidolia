using UnityEngine;
using System.Collections;

public class AxiesPickScript : MonoBehaviour
{
    public GameObject m_precisePick;
    bool m_start;
    Vector3 m_startPosition;
    Camera m_camera;

    public void SetCamera(Camera camera)
    {
        m_camera = camera;
    }

    void Awake()
    {
        m_start = true;
    }

    public void Drag(Vector2 mousePosition)
    {
        PrecisePickScript pickScript = (PrecisePickScript)m_precisePick.GetComponent("PrecisePickScript");
        GameObject controlledGameObject = pickScript.ReturnGameObject();

        Vector3 camToObject = controlledGameObject.rigidbody.transform.position - Camera.main.transform.position;
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);
        Vector3 mouseVector = mouseRay.direction.normalized;
        Vector3 forward = gameObject.transform.forward;

        Vector3 perpendicular = Vector3.Cross(camToObject, forward).normalized;
        mouseVector = mouseVector - Vector3.Dot(perpendicular, mouseVector) * perpendicular;

        double angleForwardToObject = Vector3.Angle(forward, camToObject) / 180.0 * 3.1415926;
        double angleMouseForward = Vector3.Angle(mouseVector, forward) / 180.0 * 3.1415926;

        float distance = camToObject.magnitude / Mathf.Sin((float)angleMouseForward) * Mathf.Sin((float)angleForwardToObject);
        Vector3 destinationPoint = new Ray(Camera.main.transform.position, mouseVector.normalized).GetPoint(distance);

        if (m_start)
        {
            m_startPosition = destinationPoint;
            m_start = false;
        }
        else
        {
            Vector3 record = destinationPoint - m_startPosition;
            pickScript.ProcessMove(destinationPoint - m_startPosition);
            m_startPosition = destinationPoint;
        }
    }

    public bool IsChosen(Vector2 mousePosition)
    {
        PrecisePickScript pickScript = (PrecisePickScript)m_precisePick.GetComponent("PrecisePickScript");
        RaycastHit hit;
        if (!Physics.Raycast(m_camera.ScreenPointToRay(mousePosition), out hit, pickScript.m_pickRange) || hit.rigidbody == null || hit.rigidbody.gameObject != gameObject)
        {
            m_start = true;
            return false;
        }
        return true;
    }
}
