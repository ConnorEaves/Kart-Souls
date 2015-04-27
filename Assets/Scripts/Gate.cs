using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {
	bool isDown;
	float upY;
	float downY;
	public float speed;

	// Use this for initialization
	void Start () {
		//InvokeRepeating ("GateToggle", 3.0f, 5.0f);
		isDown = true;
		upY = 25;
		downY = 15;
	}
	
	// Update is called once per frame
	void Update () {
		// if up, go down
		if (isDown) {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, upY, transform.position.z), speed);
		// if down, go up
		} else {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, downY, transform.position.z), speed);
		}

		// are we all the way up or down?
		if (Vector3.Distance (transform.position, new Vector3 (transform.position.x, upY, transform.position.z)) < 0.25f) {
			isDown = false;
		}
		if (Vector3.Distance (transform.position, new Vector3 (transform.position.x, downY, transform.position.z)) < 0.25f) {
			isDown = true;
		}
	}

}
