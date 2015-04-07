using UnityEngine;
using System.Collections;

public class CheckpointBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll){
		KartLapTimer lapTimer = coll.GetComponent<KartLapTimer> ();

		if (this.gameObject.GetComponent<BoxCollider>() == lapTimer.checkpoint[lapTimer.checkpointCounter])
			lapTimer.HitCheckpoint ();
	}
}
