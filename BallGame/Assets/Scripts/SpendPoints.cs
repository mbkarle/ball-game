using UnityEngine;
using System.Collections;

public class SpendPoints : MonoBehaviour {

	public void Spend (int cost) {
		DataController.control.experience -= cost;
		if (DataController.control.experience < 0) {
			DataController.control.experience = 0;
		}
		DataController.control.Save ();
	}
}
