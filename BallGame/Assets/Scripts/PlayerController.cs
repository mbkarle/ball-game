using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour { //Motion tutorial class; not relevant to final product

	public float speed;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveX = Input.GetAxis ("Horizontal");
		float moveZ = Input.GetAxis ("Vertical");
		//Debug.Log (moveX);
		float moveY = 0f;
		if (Input.GetKey ("space")) {
			moveY = 3f;
			//Debug.Log ("Jumping");
		}

		Vector3 movement = new Vector3 (moveX, moveY, moveZ);
		rb.AddForce (movement * speed);

		//tilt controls that act directly on ball
		float xAccel = Input.acceleration.x;
		float zAccel = -Input.acceleration.z;
		float jumpAccel = 0f;

		if (Input.touchCount > 0) {
			jumpAccel = 3f;
		}

		Vector3 movementTilt = new Vector3 (xAccel - gameObject.transform.parent.GetComponent<BuildWorld> ().xAccelOffset, jumpAccel, zAccel - gameObject.transform.parent.GetComponent<BuildWorld> ().zAccelOffset);
		rb.AddForce (movementTilt * 15);
			
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("Triggered");
		char[] name = other.gameObject.name.ToCharArray();
		if (name [name.Length - 1].Equals ('9')) {
			Debug.Log ("On last");
			gameObject.transform.parent.GetComponent<BuildWorld> ().BuildChunk ();
			Vector3 curr_position = gameObject.transform.position;
			gameObject.transform.position = new Vector3 (curr_position.x, curr_position.y, 0);
		}
	}
			
}
