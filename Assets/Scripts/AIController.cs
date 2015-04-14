using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {
	private KartController kartController;
	// Use this for initialization
	void Start () {
		kartController = gameObject.GetComponent<KartController> ();
	}
	
	// Update is called once per frame
	void Update () {
		kartController.gameInput (1, 0);
	}
}
