﻿using UnityEngine;
using System.Collections;

public class EnclosureController : MonoBehaviour {

	private int nextScoreMagnitude, worldTier, initWorldTier;
	public int score;
	private Vector3 startCoords;
	private Transform ball;
	private float offset;
	private GameObject world, scoreText;

	// Use this for initialization
	void Start () {
		world = GameObject.Find ("Content");
		startCoords = gameObject.transform.position;
		ball = GameObject.Find("player").transform;
		offset = ball.position.z;
		score = 0;
		nextScoreMagnitude = 1;
		scoreText = gameObject.transform.GetChild (1).GetChild(0).gameObject;
		setScore (0);
		worldTier = world.GetComponent<BuildWorld> ().worldTier;
		initWorldTier = worldTier;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float zMovement = ball.position.z - offset;
		gameObject.transform.localPosition = new Vector3 (startCoords.x, startCoords.y, startCoords.z + zMovement); 
		//Debug.Log (gameObject.name + ", " + gameObject.transform.localPosition);
		Vector3 v = world.transform.eulerAngles;
		ContentObject.rotate (scoreText, -92, 0, -v.z);


	}

	public void setScore(int increase){
		score += increase;
		scoreText.GetComponent<TextMesh> ().text = score.ToString ();
		float magnitude = Mathf.Log10 (score);
		if (Mathf.Floor(magnitude) == nextScoreMagnitude) {
			nextScoreMagnitude++;
			scoreText.GetComponent<TextMesh> ().fontSize -= 20;
		}

		if (worldTier > 0 && worldTier < 3) {
//			Debug.Log ("Tier can be set");
			if (Mathf.Floor(score/10) > worldTier-1) {
				Debug.Log ("Raising tier");
				worldTier++;
				world.GetComponent<BuildWorld> ().worldTier = worldTier;
			}
		}
	}

	public void reset(){
		DataController.control.experience += score;
		DataController.control.Save ();
		gameObject.transform.position = startCoords;
		setScore (-score);
		scoreText.GetComponent<TextMesh> ().fontSize = 84;
		worldTier = initWorldTier;
		world.GetComponent<BuildWorld> ().worldTier = worldTier;
		nextScoreMagnitude = 1;
	}
}
