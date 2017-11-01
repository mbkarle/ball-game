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
	}
}
