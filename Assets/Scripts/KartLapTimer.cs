using UnityEngine;
using System.Collections;

public class KartLapTimer : MonoBehaviour {
	public Collider[] checkpoint;
	public int checkpointCounter;
	public float finalTime;
	public bool finished;
	private bool firstLap;
	private float[] lapTime = new float[3];
	private float startLap;
	private int lapCounter;
	private Canvas EndRace;

	void Awake () {
		checkpointCounter = 0;
		firstLap = true;
		lapCounter = 0;
		StartLap ();

		EndRace = GameObject.FindGameObjectWithTag ("endRaceCanvas").GetComponent<Canvas>();
		EndRace.enabled = false;

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
		startLap = Time.time;
	}

	void FinishLap(){
		lapTime [lapCounter] = Time.time - startLap;
		lapCounter++;
		StartLap ();

		if (lapCounter >= 3){
			finished = true;
			if (gameObject.GetComponent<KartController> ().playerController) {
				gameObject.GetComponent<KartController> ().playerController = false;
				gameObject.GetComponent<AIController>().enabled = true;

				EndRace.enabled = true;
			}
			foreach (float lap in lapTime){
				finalTime += lap;
			}
			GameObject.FindGameObjectWithTag("LapTimeManager").GetComponent<LapTimeManager>().AddTimeForDisplay(finalTime,gameObject.GetComponent<KartController>().kartName);
			gameObject.GetComponent<KartController>().MaxSpeed = 30;
			//Debug.Log(finalTime);
		}
	}
}
