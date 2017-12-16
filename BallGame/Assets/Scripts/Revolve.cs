using UnityEngine;
using System.Collections;

public class Revolve : MonoBehaviour {

	public float speed, rotation;
	private bool rotating;
	private Vector3 initRotation;
	// Use this for initialization
	void Start () {
		rotation = gameObject.transform.eulerAngles.y;
		speed = 1;
		rotating = true;
		initRotation = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (rotating) {
			ContentObject.rotate (gameObject, initRotation.x, rotation + speed, initRotation.z);
			rotation += speed;
		} else if (!(rotation % 180 <= 10) ) {
			ContentObject.rotate (gameObject, initRotation.x, rotation + speed, initRotation.z);
			rotation += speed;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.transform.parent.gameObject.name == "player") {
			rotating = false;
		}
	}

	public void setInitRotation(Vector3 newRotation){
		initRotation = newRotation;
	}
}
