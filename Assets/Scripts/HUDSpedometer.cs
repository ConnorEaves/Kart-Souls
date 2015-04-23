using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// TOUCH THIS CODE AND I MURDER YOU

public class HUDSpedometer : MonoBehaviour {

	KartController playerKartController;
	public float Sensitivity;


	void Update () {
		if (playerKartController == null) {
			GameObject[] karts = GameObject.FindGameObjectsWithTag("kart");
			foreach (GameObject kart in karts){
				if (kart.GetComponent<KartController>().playerController){
					playerKartController = kart.GetComponent<KartController>();
					break;
				}
			}

		}
		float speed = playerKartController.CurrentSpeed;
		transform.rotation = Quaternion.Euler (0, 0, -Mathf.Abs (speed) * Sensitivity);
	}

}
