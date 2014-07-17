using UnityEngine;
using System.Collections;

public class TouchPoint : MonoBehaviour {

	public Color touchedColor = Color.red;
	private Color originColor = Color.green;
	public bool isControlling = false;
	private GameObject controlDot;
	private float controlRadius = 0.0f;

	const int FADE_TIME = 1;
	private float fadeTime = 0.0f;

	// Use this for initialization
	void Start () {
		controlDot = GameObject.FindGameObjectWithTag("controlDot");
		controlRadius = gameObject.GetComponent<CircleCollider2D>().radius;
		transform.position = new Vector3(-100,-100,0);
		controlDot.transform.position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		FadeEffect();

		if(!Input.GetMouseButton(0))
		{
			isControlling = false;
			transform.position = new Vector3(-100,-100,0);
			controlDot.transform.position = transform.position;
		}

		if(Input.GetMouseButton(0))
		{
			Vector3 newpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			newpos.z = 0;
			if(!isControlling)
			{
				transform.position = newpos;
				controlDot.transform.position = newpos;
				isControlling = true;
			}


			Vector3 distance =  newpos - gameObject.transform.position;

			if(distance.magnitude > controlRadius*2)
			{
				distance = distance.normalized * controlRadius*2;
				newpos = gameObject.transform.position + distance;
			}
			controlDot.transform.position = newpos;
		}
		else
		{
			controlDot.transform.position = gameObject.transform.position;
		}
	}

	
	void FadeEffect(){
		if(isControlling && fadeTime < FADE_TIME){
			fadeTime += 0.03f;
			Color newColor = gameObject.renderer.material.color;
			gameObject.renderer.material.color = new Color(newColor.r, newColor.g, newColor.b, fadeTime);
			controlDot.renderer.material.color = new Color(touchedColor.r, touchedColor.g, touchedColor.b, fadeTime/2);
		}
		if(!isControlling && fadeTime > 0)
		{
			fadeTime -= 0.03f;
			Color newColor = gameObject.renderer.material.color;
			gameObject.renderer.material.color = new Color(newColor.r, newColor.g, newColor.b, fadeTime);
			controlDot.renderer.material.color = new Color(originColor.r, originColor.g, originColor.b, fadeTime/2);
		}
	}

}
