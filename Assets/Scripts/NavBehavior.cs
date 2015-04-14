using UnityEngine;
using System.Collections;

public class NavBehavior : MonoBehaviour {
	private AIController ai;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll){
		ai = coll.gameObject.GetComponent<AIController> ();
		if (ai != null)
			ai.HitNav ();
	}
}
