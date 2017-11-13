using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AdjustVolume : MonoBehaviour {

	public Slider slider;
	public void AdjustVol () {
		this.GetComponent<AudioSource> ().volume = slider.value;
	}

}
