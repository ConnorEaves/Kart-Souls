using UnityEngine;
using System.Collections;

public class KartLapTimer : MonoBehaviour {
	public GameObject[] checkpoint;
	public int checkpointCounter;
	public float finalTime;
	public bool finished;
	private bool firstLap;
	private float[] lapTime = new float[3];
	private float startLap;
	public int lapCounter;
	private Canvas EndRace;
	private Canvas HUD;

	void Awake () {
		checkpointCounter = 0;
		firstLap = true;
		lapCounter = 0;
		StartLap ();

		HUD = GameObject.FindWithTag ("HUDCanvas").GetComponent<Canvas> ();
		EndRace = GameObject.FindGameObjectWithTag ("endRaceCanvas").GetComponent<Canvas>();
		checkpoint = GameObject.FindGameObjectWithTag ("navPointsList").GetComponent<navPointsList> ().checkPointList;

	}
	
	// Update is called once per frame
	void Update () {

	}
	public void HitCheckpoint() {
		if (checkpointCounter == 0 && !firstLap  && !finished)
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
				HUD.enabled = false;
			}
			foreach (float lap in lapTime){
				finalTime += lap;
			}
			finalTime -= 3.0f;

			GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>().AddTimeForDisplay(finalTime,gameObject.GetComponent<KartController>().kartName);
			gameObject.GetComponent<KartController>().MaxSpeed = 30;
			//Debug.Log(finalTime);
		}
	}
}
