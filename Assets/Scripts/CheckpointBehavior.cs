using UnityEngine;
using System.Collections;

public class CheckpointBehavior : MonoBehaviour {

	void OnTriggerEnter(Collider coll){
		KartLapTimer lapTimer = coll.GetComponent<KartLapTimer> ();

		if (this.gameObject.GetComponent<BoxCollider>() == lapTimer.checkpoint[lapTimer.checkpointCounter])
			lapTimer.HitCheckpoint ();
	}
}
