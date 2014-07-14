﻿using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour {

	public Color mouseOverColor = Color.blue;

	public float maxSpeedx = 5.0f;
	public float maxSpeedy = 5.0f;
	bool facingRight = true;
	public bool onGround = false;
	private Vector3 oriDotPos = new Vector3();
	private BoxCollider2D myCollider = new BoxCollider2D();
	private Vector3 spawnPos = new Vector3();

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		oriDotPos = GameObject.FindGameObjectWithTag("controlDot").transform.position;
		myCollider = gameObject.GetComponent<BoxCollider2D>();
		spawnPos = transform.position;
	}

	void OnCollisionEnter2D(Collision2D col){
		onGround = true;
	}

	void OnCollisionExit2D(Collision2D col){
		onGround = false;
	}



	void Update(){

		bool needmove = false;

		Vector3 newDotPos = GameObject.FindGameObjectWithTag("controlDot").transform.position;
		Vector2 control = new Vector2(newDotPos.x - oriDotPos.x , newDotPos.y - oriDotPos.y);

		if(control.magnitude > 0.2)
		{
			needmove = true;
		}
		else 
		{
			control = Vector2.zero;
		}

		
		// when control.y < 0, player is crouching, disable the box collider, and rotate character
		if(control.y < 0)
		{
			if(!myCollider.isTrigger)
				transform.Rotate(Vector3.forward,90.0f,Space.World);
			myCollider.isTrigger = true;
		}
		else
		{
			if(myCollider.isTrigger)
				transform.Rotate(Vector3.forward,-90.0f,Space.World);
			myCollider.isTrigger = false;
		}

		if(!onGround)
			control.y = 0;
		else
			control.y = control.y > 0.5 ? 1 : control.y;


		Debug.Log(myCollider.isTrigger);

		anim.SetFloat("speed",Mathf.Abs(control.x));
		if(needmove)
		{
			float upSpeed = rigidbody2D.velocity.y + control.y * maxSpeedy;
			upSpeed = upSpeed > maxSpeedy ? maxSpeedy : upSpeed;
			rigidbody2D.velocity = new Vector2(control.x * maxSpeedx , upSpeed);

			if(control.x>0 && !facingRight)
				flip();
			else if (control.x<0 && facingRight)
				flip();

			if(control.y > 0)
			{
				onGround = false;
			}
		}

		// people was dead, temporary use
		if(rigidbody2D.velocity.y < -15)
		{
			transform.position = spawnPos;
			rigidbody2D.velocity = new Vector2(0,0);
		}

	}

	void flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}