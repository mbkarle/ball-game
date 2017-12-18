using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AdjustVolume : MonoBehaviour {

	public Slider slider;

	void Start() {
		DataController.control.Load ();
	}

	public void AdjustVol () {
		this.GetComponent<AudioSource> ().volume = slider.value;
		DataController.control.experience += 2;
		DataController.control.Save ();
	}

}
