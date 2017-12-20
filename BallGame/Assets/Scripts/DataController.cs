using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataController : MonoBehaviour {

	public static DataController control; //can be called from anywhere with "DataController.control"

	public float health;
	public float experience;

	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != this) {
			Destroy (gameObject);
		}
	}

	void Start() {
		control.Load ();
	}

	void OnGUI () { //just displays the "XP" in the top left
		GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
		myButtonStyle.fontSize = 30;
		Font myFont = (Font)Resources.Load("Fonts/comic", typeof(Font));
		myButtonStyle.font = myFont;
		myButtonStyle.normal.textColor = Color.black;
		GUI.Label (new Rect (10, 10, 100, 30), "XP: " + experience, myButtonStyle);
	}

	public void Save() { //we can have multiple files for each player but this just has one
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat"); //data extension can be anything

		PlayerData data = new PlayerData();
		//data.health = health;
		data.experience = experience;

		bf.Serialize (file, data); //you can save this data to all sorts of stuff (e.g. internet)
		//file is in binary so unless they're a l33t h4x0r nobody can edit these
		file.Close ();
	}

	public void Load() {
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData) bf.Deserialize (file);
			file.Close ();

			//health = data.health;
			experience = data.experience;
		}
	}
}

[Serializable]
class PlayerData { //we can add constructers and stuff but this is just the outline

	//public float health;
	public float experience;

}