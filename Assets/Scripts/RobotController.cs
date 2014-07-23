using UnityEngine;
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
	bool isControlling = false;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

		myCollider = gameObject.GetComponent<BoxCollider2D>();
		spawnPos = transform.position;
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "ladders") {
			rigidbody2D.gravityScale = 0;
		} else {
			onGround = true;
		}
	}

	void OnCollisionExit2D(Collision2D col){
		if(col.gameObject.tag == "ladders") {
			rigidbody2D.gravityScale = 1;
		}
		onGround = false;
	}



	void Update(){

		bool needmove = false;
		Vector3 newDotPos = GameObject.FindGameObjectWithTag("controlDot").transform.position;

		// people was dead, temporary use
		if(rigidbody2D.velocity.y < -15) {
			transform.position = spawnPos;
			rigidbody2D.velocity = new Vector2(0,0);
		}


		if(newDotPos.x<-90) {
			if(isControlling) {
				isControlling = false;
				StandUp();
			}
			anim.SetFloat("speed",Mathf.Abs(rigidbody2D.velocity.x));
			return;
		} else {
			if(!isControlling) {
				oriDotPos = newDotPos;
			}
			isControlling = true;
		}


		Vector2 control = new Vector2(newDotPos.x - oriDotPos.x , newDotPos.y - oriDotPos.y);
		if(control.magnitude > 0.2) {
			needmove = true;
		} else {
			control = Vector2.zero;
		}
		
		// when control.y < 0, player is crouching, disable the box collider, and rotate character
		if(control.y < -0.5) {
			ChrouchDown();
		} else {
			StandUp();
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
		}

		// If stand on ground, enable jump, set control.y = 1
		if(!onGround) {
			control.y = 0;
		} else {
			control.y = control.y > 0.5 ? 1 : 0;
		}


		anim.SetFloat("speed",Mathf.Abs(control.x));
		if(needmove)
		{
			float upSpeed = rigidbody2D.velocity.y + control.y * maxSpeedy;
			upSpeed = upSpeed > maxSpeedy ? maxSpeedy : upSpeed;
			rigidbody2D.velocity = new Vector2(control.x * maxSpeedx , upSpeed);

			if(control.x>0 && !facingRight) {
				flip();
			} else if (control.x<0 && facingRight) {
				flip();
			}

			if(control.y > 0) {
				onGround = false;
			}
		}

	}

	void flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void ChrouchDown(){
		if(!myCollider.isTrigger) {
			transform.Rotate(Vector3.forward,90.0f,Space.World);
		}
		myCollider.isTrigger = true;
	}

	void StandUp(){
		if(myCollider.isTrigger) {
			transform.Rotate(Vector3.forward,-90.0f,Space.World);
		}
		myCollider.isTrigger = false;
	}

}
