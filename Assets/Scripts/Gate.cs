using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {
	bool gateDown;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("GateToggle", 3.0f, 5.0f);
		gateDown = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GateToggle()
	{
		if (gateDown) {
			transform.Translate (-Vector3.forward * 8.0f);
		}
		else {
			transform.Translate (Vector3.forward * 8.0f);
		}
		gateDown = !gateDown;
	}

}
