using UnityEngine;
using System.Collections;

// Displays Kart stats on screen
public class KartStats : MonoBehaviour {

	public GameObject Kart;
	public Font GUIFont;

	KartController kartController;
	GUIStyle style;

	void Awake () {
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

	void OnGUI () {
		string maxSpeed = new string ('|', (int)kartController.MaxSpeed);
		string speed = new string ('|', Mathf.Abs((int)kartController.CurrentSpeed));
		string acceleration = new string ('|', (int)kartController.Acceleration);
		string handling = new string ('|', (int)kartController.TurnSpeed * 10);

		GUILayout.Label ("MaxSpeed:     " + maxSpeed, style);
		GUILayout.Label ("Speed:        " + speed, style);
		GUILayout.Label ("Acceleration: " + acceleration, style);
		GUILayout.Label ("Handling:     " + handling, style);
	}
}
