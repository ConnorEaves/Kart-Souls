using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour {

	public GameObject[] Karts;

	Text countdownText;

	void Start () {
		Karts = GameObject.FindGameObjectsWithTag ("kart");
		countdownText = GetComponent<Text> ();
		StartCoroutine ("StartRace");
		Debug.Log ("Countdown Ended");

	}

	public IEnumerator StartRace () {

		foreach (GameObject kart in Karts) {
			kart.GetComponent<KartController> ().enabled = false;
		}

		countdownText.text = "3";
		yield return new WaitForSeconds (1.0f);
		countdownText.text = "2";
		yield return new WaitForSeconds (1.0f);
		countdownText.text = "1";
		yield return new WaitForSeconds (1.0f);
		foreach (GameObject kart in Karts) {
			kart.GetComponent<KartController> ().enabled = true;
		}
		countdownText.text = "GO!";
		yield return new WaitForSeconds (2.0f);
		countdownText.text = "";
		
	}

}
