using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float shift;
	public float shiftx;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		shift = transform.position.z - 0.01f;
		shiftx = transform.position.x + 0.01f;
		transform.position = new Vector3(shiftx,0.0f,shift);
	}
}

