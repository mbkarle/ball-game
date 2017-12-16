using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour { //Camera motion and angle still undecided

	public GameObject player;
	Vector3 offset;
	bool attached;
	public Transform content;

	// Use this for initialization
	void Start () { //waits until sphere object is attached to calculate offset
		attached = false;
		content = GameObject.Find ("Content").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (attached) {
			Vector3 upraise = (content.rotation.eulerAngles.x < 90) ? new Vector3 (0, content.rotation.eulerAngles.x / 5) : new Vector3 (0, 0);
			transform.position = player.transform.position + offset + upraise;
			float xRot = (content.rotation.eulerAngles.x < 90 && content.rotation.eulerAngles.x > 30) ? content.rotation.eulerAngles.x - 30 : 0;
			transform.rotation = Quaternion.AngleAxis (20 + xRot/2, Vector3.right);
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
