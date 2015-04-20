using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDSpedometer : MonoBehaviour {

	public GameObject PlayerKart;

	KartController playerKartController;

	void Awake () {
		playerKartController = PlayerKart.GetComponent<KartController> ();
	}

	void Update () {
		float speed = playerKartController.CurrentSpeed;
		transform.rotation = Quaternion.Euler (0, 0, -Mathf.Abs (speed) * 2.0f);
	}

}
