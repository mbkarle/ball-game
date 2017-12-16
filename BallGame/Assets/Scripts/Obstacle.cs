using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	public GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.name.Equals("player")){
			other.gameObject.GetComponent<PlayerController> ().KillPlayer ();
		}
	}
			
}
