using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterPresenter : MonoBehaviour {

	public Canvas CharSelectCanvas;

	void Update () {
		if(CharSelectCanvas.enabled) {
			gameObject.SetActive(true);
		} else {
			gameObject.SetActive (false);
		}
	}
}
