using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustVolume : MonoBehaviour {

	public Slider slider;

	void Update() {
		this.GetComponent<AudioSource> ().volume = slider.value;
	}

}
