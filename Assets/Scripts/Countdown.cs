using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour {

	public GameObject[] Karts;

	Text countdownText;
	public AudioClip count;
	public AudioClip go;

	void Start () {
		Karts = GameObject.FindGameObjectsWithTag ("kart");
		countdownText = GetComponent<Text> ();
		StartCoroutine ("StartRace");
		Debug.Log ("Countdown Ended");

	}

	public IEnumerator StartRace () {

		// Disable all karts
		foreach (GameObject kart in Karts) {
			kart.GetComponent<KartController> ().enabled = false;
		}

		// Display Countdown
		countdownText.text = "3";
		AudioSource.PlayClipAtPoint (count, Camera.main.transform.position);
		yield return new WaitForSeconds (1.0f);
		countdownText.text = "2";
		AudioSource.PlayClipAtPoint (count, Camera.main.transform.position);
		yield return new WaitForSeconds (1.0f);
		countdownText.text = "1";
		AudioSource.PlayClipAtPoint (count, Camera.main.transform.position);
		yield return new WaitForSeconds (1.0f);
		// ReEnable karts
		foreach (GameObject kart in Karts) {
			kart.GetComponent<KartController> ().enabled = true;
		}
		countdownText.text = "GO!";
		AudioSource.PlayClipAtPoint (go, Camera.main.transform.position);
		yield return new WaitForSeconds (2.0f);
		countdownText.text = "";
		
	}

}
