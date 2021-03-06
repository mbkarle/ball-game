﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BuildWorld : MonoBehaviour {

	public float xAccelOffset, zAccelOffset;
	public float speed;

	private GameObject ground, player;
	private ContentCube groundCube;
	private ArrayList chunks;
	public Dictionary<string, ArrayList> chunkDictionary = new Dictionary<string, ArrayList> ();

	private Material ballColor;
	private PhysicMaterial surface;
	private Vector3 contentRotation;
	public int worldTier;

	// Use this for initialization
	void Start () { 
//		chunkDictionary = new Dictionary<string, ArrayList> ();
//		chunkDictionary.Add("Stage", new ArrayList());
//		chunkDictionary["Stage"].Add(ContentObject.buildPrefab("BasicStage"));
//		for(int i = 0; i < chunkDictionary.Keys.ToList().Count; i++){
//			Debug.Log(chunkDictionary[chunkDictionary.Keys.ToList()[i]]);
//		}
//		Debug.Log (chunkDictionary);

		worldTier = 1;
		xAccelOffset = Input.acceleration.x;
		zAccelOffset = -Input.acceleration.z;
	
		player = new GameObject ();
		ContentSphere.convert (player, "player", .5f, 1.5f, 2, 3, 3, 3);
		ContentSphere.addMaterial (player.name, "ballTexture");
		player.AddComponent<PlayerController> ();
		player.GetComponent<PlayerController> ().speed = 10;
		GameObject playerTrigger = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		playerTrigger.transform.parent = player.transform;
		playerTrigger.transform.localScale = new Vector3 (1, 1, 1);
		playerTrigger.transform.localPosition = new Vector3 (0, 0, 0);
		playerTrigger.GetComponent<Collider> ().isTrigger = true;
		playerTrigger.GetComponent<MeshRenderer> ().enabled = false;
	//	player.GetComponent<Rigidbody> ().isKinematic = true;


		GameObject camera = GameObject.Find ("Main Camera");
		camera.GetComponent<CameraController> ().player = player;

		BuildChunk (6, null);

		GameObject enclosure = Instantiate (ContentObject.buildPrefab ("Enclosure") as GameObject);
		ContentObject.setTransform (enclosure, "Enclosure", 0, 0, 0, enclosure.transform.localScale.x, enclosure.transform.localScale.y, enclosure.transform.localScale.z, true);

//		foreach (GameObject go in GameObject.Find("Content").transform) {
//			Debug.Log ((go).name);
//		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float zMovement = Input.GetAxis ("Horizontal");
		float xMovement = Input.GetAxis ("Vertical");
		if (Mathf.Abs (contentRotation.x + speed * xMovement) < 55) {
			contentRotation.x += speed * xMovement;
		}
		contentRotation.z += speed * -zMovement;
		//Debug.Log (contentRotation);
		//Transform gameObj = gameObject.transform;

		transform.localRotation = Quaternion.AngleAxis (contentRotation.x, Vector3.right) * Quaternion.AngleAxis (contentRotation.z, Vector3.forward);
			
//		if (zMovement > 0 && gameObj.localRotation.eulerAngles.z <= 30 && gameObj.localRotation.eulerAngles.z >= -35) { working on settings angle limits
//			gameObj.localRotation = Quaternion.Euler (0.0f, 0.0f, gameObj.localRotation.eulerAngles.z + speed * -zMovement);
//		} else if (zMovement < 0 && gameObj.localRotation.eulerAngles.z >= -30 && gameObj.localRotation.eulerAngles.z <= 35) {
//			gameObj.localRotation = Quaternion.Euler (0.0f, 0.0f, gameObj.localRotation.eulerAngles.z + speed * -zMovement);
//		}

	//	Debug.Log ("" + gameObj.localRotation.eulerAngles.z + " " + gameObj.localRotation.eulerAngles.x);

		//float rotationX = Input.acceleration.x - xAccelOffset;
		//float rotationZ = -Input.acceleration.z - zAccelOffset;

//		Vector3 temp = Input.acceleration;
//		temp.x -= xAccelOffset;
//		temp.z -= zAccelOffset;

//		foreach (GameObject go in chunks) {
//			if (temp.z > 0 && go.transform.eulerAngles.z <= temp.z) {
//				go.transform.Rotate (0.0f, 0.0f, 1.0f);
//			} else if (temp.z < 0 && go.transform.eulerAngles.z >= temp.z) {
//				go.transform.Rotate (0.0f, 0.0f, -1.0f);
//			}
//		}
	}

	public ArrayList addByTier(ArrayList chunkNames, int tier){
		switch (tier) {
		case 1:
			chunkNames.Add ("Stage");
			chunkNames.Add ("RightProtectedPlat");
			chunkNames.Add ("LeftProtectedPlat");
			chunkNames.Add ("MidObstacleStage");
			chunkNames.Add ("HalfObstacleStage");
			chunkNames.Add ("RevHalfObstacleStage");
			//chunkNames.Add ("Tube");
			break;

		case 2:
			chunkNames.Add ("CurvedPlatform");
			chunkNames.Add ("RevCurvedPlatform");
			chunkNames.Add ("Rails");
			chunkNames.Add ("ScatteredObstacleStage");
			chunkNames.Add ("RevScatteredObstacleStage");
			break;

		case 3:
			chunkNames.Add ("CC Twisted Rails"); //counter clockwise
			chunkNames.Add("C Twisted Rails"); //clockwise
			chunkNames.Add ("RotStage");
			break;

		default:
			chunkNames.Add ("MidObstacleStage");
			chunkNames.Add ("HalfObstacleStage");
			chunkNames.Add ("RevHalfObstacleStage");
			chunkNames.Add ("ScatteredObstacleStage");
			chunkNames.Add ("RevScatteredObstacleStage");
			break;
		}
		return chunkNames;
	}

	public void BuildChunkDictionary (int tier){ //populates with appropriately difficult structures based on tier

//		for (int i = 0; i < chunkDictionary.Keys.ToList().Count; i++) {
//			chunkDictionary.Remove (chunkDictionary.Keys.ToList()[i]);
//		}
		chunkDictionary.Clear();

		ArrayList chunkNames = new ArrayList(); //arraylist and switch to avoid excessive redundancy

		do {
			addByTier (chunkNames, tier);
			tier--;
		} while(tier > 0);

		for (int i = 0; i < chunkNames.Count; i++) { //add to switch statement as more structures are created
			string chunkName = (string)(chunkNames[i]);
			switch (chunkName) {
			case "Stage":
				chunkDictionary.Add ("Stage", new ArrayList ());
				chunkDictionary ["Stage"].Add (Instantiate(ContentObject.buildPrefab ("OpenStage")));
				break;

			case "Rails":
				chunkDictionary.Add ("Rails", new ArrayList ());
				for (int j = 0; j < 60; j++) {
					chunkDictionary ["Rails"].Add (Instantiate(ContentObject.buildPrefab ("BasicRail")));
				}
				break;

			case "CC Twisted Rails":
				chunkDictionary.Add ("CC Twisted Rails", new ArrayList ());
				for (int j = 0; j < 60; j++) {
					GameObject rail = Instantiate(ContentObject.buildPrefab ("BasicRail"));
					ContentObject.rotate (rail, 0, 0, j);
					chunkDictionary ["CC Twisted Rails"].Add (rail);
				}
				break;

			case "C Twisted Rails":
				chunkDictionary.Add ("C Twisted Rails", new ArrayList ());
				for (int j = 0; j < 60; j++) {
					GameObject rail = Instantiate (ContentObject.buildPrefab ("BasicRail"));
					ContentObject.rotate (rail, 0, 0, -j);
					chunkDictionary ["C Twisted Rails"].Add (rail);
				}
				break;

			case"RightProtectedPlat":
				chunkDictionary.Add ("RightProtectedPlat", new ArrayList ());
				chunkDictionary ["RightProtectedPlat"].Add (Instantiate (ContentObject.buildPrefab ("HalfProtectedPlatform")));
				break;

			case"LeftProtectedPlat":
				chunkDictionary.Add ("LeftProtectedPlat", new ArrayList ());
				GameObject plat = Instantiate (ContentObject.buildPrefab ("HalfProtectedPlatform"));
				ContentObject.rotate (plat, 0, 180, 0);
				chunkDictionary ["LeftProtectedPlat"].Add (plat);
				break;

			case "CurvedPlatform":
				chunkDictionary.Add ("CurvedPlatform", new ArrayList ());
				GameObject platform = Instantiate (ContentObject.buildPrefab ("CurvedPlatform"));
				chunkDictionary["CurvedPlatform"].Add(platform);
				break;

			case "RevCurvedPlatform":
				chunkDictionary.Add ("RevCurvedPlatform", new ArrayList ());
				GameObject rev = Instantiate(ContentObject.buildPrefab("CurvedPlatform"));
				ContentObject.rotate(rev, 0, 180, 0);
				chunkDictionary["RevCurvedPlatform"].Add(rev);
				break;

			case "RotStage":
				chunkDictionary.Add ("RotStage", new ArrayList ());
				GameObject rotStage = Instantiate (ContentObject.buildPrefab ("OpenStage"));
				rotStage.AddComponent<Revolve> ();
				chunkDictionary ["RotStage"].Add (rotStage);
				break;

			case "Tube":
				chunkDictionary.Add ("Tube", new ArrayList ());
				chunkDictionary ["Tube"].Add (Instantiate (ContentObject.buildPrefab ("Tube")));
				break;

			case "MidObstacleStage":
				chunkDictionary.Add ("MidObstacleStage", new ArrayList ());
				chunkDictionary ["MidObstacleStage"].Add (Instantiate (ContentObject.buildPrefab ("MidObstacleStage")));
				break;

			case "HalfObstacleStage":
				chunkDictionary.Add ("HalfObstacleStage", new ArrayList ());
				chunkDictionary ["HalfObstacleStage"].Add (Instantiate (ContentObject.buildPrefab ("HalfObstacleStage")));
				break;

			case "RevHalfObstacleStage":
				chunkDictionary.Add ("RevHalfObstacleStage", new ArrayList ());
				GameObject revHalfObstacleStage = Instantiate (ContentObject.buildPrefab ("HalfObstacleStage"));
				ContentObject.rotate (revHalfObstacleStage, 0, 180, 0);
				chunkDictionary ["RevHalfObstacleStage"].Add (revHalfObstacleStage);
				break;

			case "ScatteredObstacleStage":
				chunkDictionary.Add ("ScatteredObstacleStage", new ArrayList ());
				chunkDictionary ["ScatteredObstacleStage"].Add (Instantiate (ContentObject.buildPrefab ("ScatteredObstacleStage")));
				break;

			case "RevScatteredObstacleStage":
				chunkDictionary.Add ("RevScatteredObstacleStage", new ArrayList ());
				GameObject scatteredObsStage = Instantiate (ContentObject.buildPrefab ("ScatteredObstacleStage"));
				ContentObject.rotate (scatteredObsStage, 0, 180, 0);
				chunkDictionary ["RevScatteredObstacleStage"].Add (scatteredObsStage);
				break;

			default:
				Debug.Log ("Structure name not recognized");
				break;
			}
		}
				
	}


	//TODO: smooth out by not destroying immediately; BUG: Not consistently working?
	public void BuildChunk(int num_chunks, int? removal_index){ //pass null for removal if building for the first time
		Vector3 curr_rotation = gameObject.transform.eulerAngles; //save current roation
		ContentObject.rotate (gameObject, 0, 0, 0); //set rotation to 0 for integration of new objects
		BuildChunkDictionary (worldTier); //get appropriate structures for tier
		ArrayList chunksList = new ArrayList ();
		float startingZ = 0;
		if (!(chunks == null)) {
			if (removal_index.HasValue) {
				float sum = 0f;
				for (int i = 0; i <= removal_index.Value; i++) { //remove all objects up to the removal index, inclusive
					GameObject toRemove = (chunks [i] as GameObject);
					foreach (Transform t in toRemove.transform) {
						sum += t.localScale.z;
					}
					GameObject.Destroy (toRemove);
				}

				//everything above removal index shifted back
				for (int i = removal_index.Value + 1; i < chunks.Count; i++) { 
					GameObject toChange = (chunks [i] as GameObject);
					char[] split = toChange.name.ToCharArray ();
					string indexString = "" + split [split.Length - 1];
					int index = int.Parse (indexString);
					toChange.name = "chunk" + (index - (removal_index + 1));
					Transform thisTransform = toChange.transform;
					thisTransform.position = new Vector3 (thisTransform.position.x, thisTransform.position.y, thisTransform.position.z - sum);
					foreach (Transform child in thisTransform) {
						startingZ += child.localScale.z;
					}
//					if (index - (removal_index + 1) == 0) { //destroy trigger on chunk0
//						foreach (Transform t in toChange.transform) {
//							Transform trigger = t.GetChild (t.childCount - 1);
//							GameObject.Destroy (trigger.gameObject);
//						}
//					}
					chunksList.Add (toChange); //add objects being kept to the replacement list
				}
				//shift player back to start
//				Debug.Log("Moving player");
				player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z - sum);
			}
		}

		for (int i = 0; i < num_chunks; i++) { //build new chunks ahead

			//randomly select chunk type from dictionary
			System.Random rand = new System.Random ();
			int size = chunkDictionary.Keys.ToList ().Count;
			int index = rand.Next(size);
			string key = chunkDictionary.Keys.ToList() [index]; 
			ArrayList selectedChunk = chunkDictionary [key];

			//create new empty parent object
			GameObject obj = new GameObject ();

			//create children from arraylist
			GameObject firstChild = Instantiate ((GameObject)(selectedChunk [0]));
			ContentObject.setTransform (firstChild, "child0", 0, 0, firstChild.transform.localScale.z/2, firstChild.transform.localScale.x, firstChild.transform.localScale.y, firstChild.transform.localScale.z, false);
			firstChild.transform.parent = obj.transform;
			ArrayList instantiatedComponents = new ArrayList();
			instantiatedComponents.Add(firstChild);
	
			for (int j = 1; j < selectedChunk.Count; j++) {
				GameObject child = Instantiate((GameObject)(selectedChunk [j]));
				child.transform.parent = obj.transform;
				GameObject prev = (GameObject)(instantiatedComponents [j - 1]);
				float zCoord = prev.transform.position.z + prev.transform.localScale.z;
				ContentObject.setTransform (child, "child" + j, 0, 0, zCoord, child.transform.localScale.x, child.transform.transform.localScale.y, child.transform.localScale.z, false);
				instantiatedComponents.Add(child);

			}
			//add new chunk to replacement list
			chunksList.Add (obj);

			//set position of new chunk
			float zLoc;
			int tag = (removal_index.HasValue) ? chunksList.Count - 1 : i;
//			Debug.Log ("tag: " + tag);
			if (tag == 0) {
				float sum = 0;
				foreach (Transform t in obj.transform) {
					Transform trigger = t.GetChild (t.childCount - 1);
					GameObject.Destroy (trigger.gameObject);
					sum += t.localScale.z;
				}
				zLoc = /*sum * 3 / 8*/ 0;
				//startingZ = zLoc;
				if (obj.transform.GetChild (0).gameObject.tag == "OffsetZ") {
					zLoc += obj.transform.GetChild (0).localScale.z/2;
				}	
			} else {//if chunk is not the very first, build based on previous in list
				float sum = 0;
				Transform parentTransform = ((GameObject)(chunksList [tag - 1])).transform;
				Transform finalChild = parentTransform.GetChild (parentTransform.childCount - 1);
				Transform child0 = parentTransform.GetChild(0);
				float zRot = 0;
				if(child0.eulerAngles.z != finalChild.eulerAngles.z || child0.eulerAngles.z != parentTransform.GetChild((int)(parentTransform.childCount/2)).eulerAngles.z){
					zRot = finalChild.eulerAngles.z;
				}
					else{
						zRot = parentTransform.eulerAngles.z;
					}
				foreach (Transform child in parentTransform) {
					sum += child.localScale.z;
				}
				zLoc = parentTransform.position.z + sum /*+ startingZ*/;
				ContentObject.rotate (obj, 0, 0, zRot);
				if (parentTransform./*transform.*/GetChild (0).gameObject.tag == "OffsetZ") {
					zLoc += parentTransform.GetChild (0).localScale.z / 2;
				}
				if (obj.transform.GetChild (0).gameObject.tag == "OffsetZ") {
					zLoc += obj.transform.GetChild (0).localScale.z / 2;
				}
			}
			
			ContentObject.setTransform (obj, "chunk" + tag, 0, 0, zLoc, obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z, true);

//			GameObject gem = Instantiate(ContentObject.buildPrefab ("Gem"));
//			gem.transform.parent = GameObject.Find ("Gems").transform;
//			gem.transform.position = new Vector3 (0, gem.transform.localScale.y * 2, zLoc + 10);
//			gem.AddComponent<Revolve> ();
//			gem.GetComponent<Revolve>().setInitRotation(new Vector3(45, 0, 45));
		}
		foreach (string al in chunkDictionary.Keys.ToList()) { //destroy all instantiated objects
			foreach (GameObject go in chunkDictionary[al]) {
				GameObject.Destroy (go);
			}
		}
		chunks = chunksList; //set instance field chunks to replacement chunkslist

		ContentObject.rotate(gameObject, curr_rotation.x, curr_rotation.y, curr_rotation.z); //rotate back into old rotation
	}

	public void setContentRotation(float x, float y, float z){
		contentRotation = new Vector3 (x, y, z);
		ContentObject.rotate (gameObject, x, y, z);
	}
}
