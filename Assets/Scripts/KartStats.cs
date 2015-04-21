using UnityEngine;
using System.Collections;

// Displays Kart stats on screen
public class KartStats : MonoBehaviour {

	public GameObject Kart;
	public Font GUIFont;

	KartController kartController;
	GUIStyle style;

	void GetKart () {

		GameObject[] karts = GameObject.FindGameObjectsWithTag ("kart");
		foreach (GameObject kart in karts) {
			if (kart.GetComponent<KartController> ().playerController) {
				Kart = kart;
				break;
			}
			
		}
		if (Kart == null) {
			Kart = karts [0];
		}

		kartController = Kart.GetComponentInParent<KartController> ();

		style = new GUIStyle ();
		style.normal.textColor = Color.yellow;
		style.font = GUIFont;
	}

}
