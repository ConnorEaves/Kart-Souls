using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {
	bool gateDown;
	float startY;
	// Use this for initialization
	void Start () {
		//InvokeRepeating ("GateToggle", 3.0f, 5.0f);
		gateDown = true;
		startY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (transform.position.y);
		if (transform.position.y < startY && gateDown) {
			GateToggle ();
		} else if (transform.position.y > (startY + 8.0f) && !gateDown) {
			GateToggle ();
		}

		if (gateDown){
			transform.Translate(Vector3.forward * Time.deltaTime * 3);
		}
		else if (!gateDown){
			transform.Translate(- Vector3.forward * Time.deltaTime * 3);
		}
	}

	void GateToggle()
	{
		gateDown = !gateDown;
	}

}
