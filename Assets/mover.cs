using UnityEngine;
using System.Collections;

public class mover : MonoBehaviour {

	public float acceleration = 3f;
	public float maxSpeed = 8f;
	public float deceleration = 0.1f;
	public float jumpStrength = 15f;
	protected Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator> ();
	}

	bool isTouchingGround() {
		RaycastHit2D hit;
		hit = Physics2D.Raycast (transform.position, new Vector2 (0, -1), 2);
		Debug.Log (hit.distance);
		return (hit.distance!=0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(rigidbody2D.velocity.x) < maxSpeed && isTouchingGround()) {
			this.rigidbody2D.AddForce(new Vector2 (Input.GetAxis("Horizontal")*acceleration,0));
		}
		if(Mathf.Abs(rigidbody2D.velocity.x) - Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f  && isTouchingGround()) {
			this.rigidbody2D.AddForce(new Vector2 (Mathf.Sign(rigidbody2D.velocity.x)*(-1* deceleration),0));
		}

		if(Input.GetButtonDown("Jump")) {
			rigidbody2D.AddForce(new Vector2(0f,jumpStrength));
		}
		if (isTouchingGround ()) {
			anim.SetFloat ("totalGroundSpeed", Mathf.Abs (rigidbody2D.velocity.x));
		}

		GetComponentInChildren<Transform>().localScale =  new Vector3 (Mathf.Sign(rigidbody2D.velocity.x),1, 1);

	}
}
