using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class LapTimeManager : MonoBehaviour {
	public Text panelText;
	private string panelTextString;

	// Use this for initialization
	void Awake () {
		//panelText = gameObject.GetComponent<Text> ();
	}

	void Update () {
		if (GameObject.FindGameObjectWithTag("endRaceCanvas").GetComponent<Canvas>().enabled) {
			panelText.text = panelTextString;
		}
	}

	public void AddTimeForDisplay(float time, string name){
		panelTextString += name + ":  " + time + "\n";
	}

}
