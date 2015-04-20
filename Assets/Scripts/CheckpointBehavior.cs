using UnityEngine;
using System.Collections;

public class CheckpointBehavior : MonoBehaviour {

	void OnTriggerEnter(Collider coll){
		KartLapTimer lapTimer = coll.GetComponentInParent<KartLapTimer> ();

		if (this.gameObject == lapTimer.checkpoint[lapTimer.checkpointCounter])
			lapTimer.HitCheckpoint ();
	}
}
