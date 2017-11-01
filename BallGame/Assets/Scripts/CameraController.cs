using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour { //Camera motion and angle still undecided

	public GameObject player;
	Vector3 offset;
	bool attached;

	// Use this for initialization
	void Start () { //waits until sphere object is attached to calculate offset
		attached = false;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (attached) {
			transform.position = player.transform.position + offset;
			transform.position.Set (transform.position.x, 10f, transform.position.z);
		} else {
			if (!player.Equals(null)) {
				offset = transform.position - player.transform.position;
				attached = true;
				Debug.Log ("Attached");
			} else {
				Debug.Log ("L");
			}
		}
	}
}
