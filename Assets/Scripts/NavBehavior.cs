using UnityEngine;
using System.Collections;

public class NavBehavior : MonoBehaviour {
	private AIController ai;

	// trigger for next AI waypoint
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.GetComponentInParent<AIController> ().enabled) {
			ai = coll.gameObject.GetComponentInParent<AIController> ();
			if (ai != null)
				ai.HitNav (this.gameObject);
		}
	}
}
