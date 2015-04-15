using UnityEngine;
using System.Collections;

public class KartLapTimer : MonoBehaviour {
	public Collider[] checkpoint;
	public int checkpointCounter;
	private bool firstLap;
	private float[] lapTime = new float[3];
	private float startLap;
	private int lapCounter;
	// Use this for initialization
	void Start () {
		checkpointCounter = 0;
		firstLap = true;
		lapCounter = 0;
		StartLap ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void HitCheckpoint() {
		if (checkpointCounter == 0 && !firstLap)
			FinishLap ();
		if (checkpointCounter == checkpoint.Length - 1) {
			checkpointCounter = 0;
			firstLap = false;
		}
		else
			checkpointCounter ++;
	}

	void StartLap(){
		startLap = Time.realtimeSinceStartup;
	}

	void FinishLap(){
		lapTime [lapCounter] = Time.realtimeSinceStartup - startLap;
		Debug.Log (lapTime [lapCounter]);
		lapCounter++;
		StartLap ();

	}
}
