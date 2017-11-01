using UnityEngine;
using System.Collections;

public class BuildWorld : MonoBehaviour {

	private GameObject ground, player;
	private ContentCube groundCube;

	private Material ballColor;
	private PhysicMaterial surface;

	// Use this for initialization
	void Start () { //TODO: create classes for these objects
		ground = new GameObject();
		ContentCube.convert (ground, "ground", 0, 0, 0, 100, 1, 100);
		ContentCube.addMaterial (ground.name, "SurfaceColor");

	
		player = new GameObject ();
		ContentSphere.convert (player, "player", 10, 1.5f, 10, 3, 3, 3);
		ContentSphere.addMaterial (player.name, "BallColor");
		player.AddComponent<PlayerController> ();
		player.GetComponent<PlayerController> ().speed = 10;



		GameObject camera = GameObject.Find ("Main Camera");
		camera.GetComponent<CameraController> ().player = player;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
