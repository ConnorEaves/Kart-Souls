using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDLapCounter : MonoBehaviour {

	Text LapCounterText;
	KartLapTimer playerKartLapTimer;

	void Awake () {
		LapCounterText = GetComponent<Text> ();
	}

	void Update () {
		// Get player controlled kart KartLapTimer
		if (playerKartLapTimer == null) {
			GameObject[] karts = GameObject.FindGameObjectsWithTag("kart");
			foreach (GameObject kart in karts){
				if (kart.GetComponent<KartController>().playerController){
					playerKartLapTimer = kart.GetComponent<KartLapTimer>();
					break;
				}
			}
			
		}
		int lap = playerKartLapTimer.lapCounter;

		// Display the proper lap counter
		LapCounterText.text = "Lap: " + (lap + 1) + "/3";
	}
}
