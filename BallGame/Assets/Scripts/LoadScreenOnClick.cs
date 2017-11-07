using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScreenOnClick : MonoBehaviour {

	public void LoadByIndex(int index) {
		SceneManager.LoadScene (index);
	}

}
