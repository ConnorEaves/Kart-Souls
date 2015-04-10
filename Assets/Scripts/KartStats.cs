using UnityEngine;
using System.Collections;

// Displays Kart stats on screen
public class KartStats : MonoBehaviour {

	public GameObject Kart;
	public Font GUIFont;

	KartControllerV2 kartController;
	GUIStyle style;

	void Awake () {
		kartController = Kart.GetComponentInParent<KartControllerV2> ();

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
