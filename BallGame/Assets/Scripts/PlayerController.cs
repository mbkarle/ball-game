using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour { //Motion tutorial class; not relevant to final product

	public float speed;
	private Rigidbody rb;
	private Vector3 startCoords;
	//private GameObject enclosure;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		startCoords = gameObject.transform.position;
		Debug.Log (gameObject.transform.position);
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
//		float moveX = Input.GetAxis ("Horizontal");
//		float moveZ = Input.GetAxis ("Vertical");
//		//Debug.Log (moveX);
//		float moveY = 0f;
		if (Input.GetKey ("space")) {
			rb.AddForce (new Vector3 (0f, -30f, 0f));
			//Debug.Log ("Jumping");
		}
		//Debug.Log (gameObject.transform.position);

//		Vector3 movement = new Vector3 (moveX, moveY, moveZ);
//		rb.AddForce (movement * speed);
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
		Debug.Log ("Triggered by " + other.gameObject.transform.parent.transform.parent.name);
		if (other.gameObject.transform.parent.transform.parent.name.Equals ("Enclosure")) {
			KillPlayer ();
		} else {
			
			char[] name = other.gameObject.transform.parent.transform.parent.name.ToCharArray ();
			string tag = "" + name [name.Length - 1];
			//Debug.Log (tag);
			int nametag = int.Parse ("" + (name [name.Length - 1]));
			if (nametag != 0) {
				Debug.Log ("Building");
				gameObject.transform.parent.GetComponent<BuildWorld> ().BuildChunk (nametag, nametag - 1);
				GameObject.Find ("Enclosure").GetComponent<EnclosureController> ().setScore (nametag);
//			Vector3 curr_position = gameObject.transform.position;
//			gameObject.transform.position = new Vector3 (curr_position.x, curr_position.y, 0);
			}
		}
	}

	void KillPlayer(){
		GameObject world = gameObject.transform.parent.gameObject;
		world.GetComponent<BuildWorld> ().setContentRotation (0, 0, 0);
		gameObject.transform.parent.GetComponent<BuildWorld> ().BuildChunk (4, 3);
		gameObject.transform.position = startCoords;
		gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		GameObject.Find ("Enclosure").GetComponent<EnclosureController> ().reset ();
	}
			
}
