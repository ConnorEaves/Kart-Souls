using UnityEngine;
using System.Collections;

public class NavBehavior : MonoBehaviour {
	private AIController ai;

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.GetComponent<AIController> ().enabled) {
			ai = coll.gameObject.GetComponent<AIController> ();
			if (ai != null)
				ai.HitNav (this.gameObject);
		}
	}
}
