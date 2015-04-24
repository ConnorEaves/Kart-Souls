using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour {

	public GameObject[] Karts;
	public AudioClip count;
	public AudioClip go;

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
		AudioSource.PlayClipAtPoint (count, Camera.main.transform.position);
		yield return new WaitForSeconds (1.0f);
		countdownText.text = "2";
		AudioSource.PlayClipAtPoint (count, Camera.main.transform.position);
		yield return new WaitForSeconds (1.0f);
		countdownText.text = "1";
		AudioSource.PlayClipAtPoint (count, Camera.main.transform.position);
		yield return new WaitForSeconds (1.0f);
		foreach (GameObject kart in Karts) {
			kart.GetComponent<KartController> ().enabled = true;
		}
		countdownText.text = "GO!";
		AudioSource.PlayClipAtPoint (go, Camera.main.transform.position);
		yield return new WaitForSeconds (2.0f);
		countdownText.text = "";
		
	}

}
