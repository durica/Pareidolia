using UnityEngine;
using System.Collections;

public class TouchPoint : MonoBehaviour {

	public Color touchedColor = Color.red;
	private Color originColor = Color.green;
	public bool isControlling = false;
	public bool canControl = false;
	private GameObject player;
	private GameObject controlDot;
	private float controlRadius = 0.0f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		controlDot = GameObject.FindGameObjectWithTag("controlDot");
		controlRadius = gameObject.GetComponent<CircleCollider2D>().radius;
	}
	
	// Update is called once per frame
	void Update () {

		if(!Input.GetMouseButton(0))
		{
			isControlling = false;
			canControl = false;
		}

		if(canControl && Input.GetMouseButton(0))
		{
			isControlling = true;

			Vector3 newpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			newpos.z = 0;
			Vector3 distance =  newpos - gameObject.transform.position;

			if(distance.magnitude > controlRadius*2)
			{
				distance = distance.normalized * controlRadius*2;
				newpos = gameObject.transform.position + distance;
			}
			controlDot.transform.position = newpos;
			controlDot.renderer.material.color = touchedColor;
		}
		else
		{
			controlDot.transform.position = gameObject.transform.position;
			controlDot.renderer.material.color = originColor;
		}
	}
	
	void OnMouseOver(){
		canControl = true;
	}

}
