using UnityEngine;
using System.Collections;

public class BuildWorld : MonoBehaviour {

	private GameObject ground, player;
	private ContentCube groundCube;
	private ArrayList chunks;

	private Material ballColor;
	private PhysicMaterial surface;

	// Use this for initialization
	void Start () { //TODO: create classes for these objects
//		ground = new GameObject();
//		ContentCube.convert (ground, "ground", 0, 0, 0, 100, 1, 100);
//		ContentCube.addMaterial (ground.name, "SurfaceColor");
//		ground = Instantiate(Resources.Load("BasicStage", typeof(GameObject)) as GameObject);
//		ground.transform.SetParent(GameObject.Find("Content").transform);
//		ground = Instantiate(ContentObject.buildPrefab("BasicStage"));
//		ground.transform.parent = gameObject.transform;
	
		player = new GameObject ();
		ContentSphere.convert (player, "player", 10, 1.5f, 10, 3, 3, 3);
		ContentSphere.addMaterial (player.name, "BallColor");
		player.AddComponent<PlayerController> ();
		player.GetComponent<PlayerController> ().speed = 10;



		GameObject camera = GameObject.Find ("Main Camera");
		camera.GetComponent<CameraController> ().player = player;

		BuildChunk ();

	}
	
	// Update is called once per frame
	void LateUpdate () {
		
	}

	public void BuildChunk(){
		ArrayList chunksList = new ArrayList ();
		if (!(chunks == null)) {
			foreach (GameObject go in chunks) {
				Destroy (go);
			}
		}
		for (int i = 0; i < 10; i++) {
			GameObject obj = Instantiate(ContentObject.buildPrefab ("BasicStage"));
			chunksList.Add (obj);
			ContentObject.setTransform (obj, "chunk" + i, 0, 0, obj.transform.localScale.z* i, obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z);
			if (i == 9) {
				obj.GetComponent<Collider>().isTrigger = true;
			}
		}
		chunks = chunksList;
//		Debug.Log (chunksList.Count);
//		Debug.Log (chunksList.Capacity);
//		for (int i = 0; i < chunksList.Count; i++) {
//			Debug.Log (chunksList [i]);
//		}
			
	}


}
