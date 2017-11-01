using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	private bool loaded;
	// Use this for initialization
	void Start () {
		loaded = false;
		GameObject check = GameObject.Find("__app");
		if (check==null && !loaded){ 
			SceneManager.LoadSceneAsync("_preload");
			loaded = true;
		}
		SceneManager.LoadScene ("GameScene");
		SceneManager.SetActiveScene (SceneManager.GetSceneByName("GameScene"));

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
