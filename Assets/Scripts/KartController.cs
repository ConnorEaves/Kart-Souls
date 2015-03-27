using UnityEngine;
using System.Collections;

// Attached to empty parent of Kart model parts
public class KartController : MonoBehaviour {

	public float CurrentSpeed;	// Speed we are going right now
	public float MaxSpeed;		// Maximum allowed speed
	public float Acceleration;	// Speed increase per second
	public float Breaking;		// Speed decrease per second while breaking
	public float Deceleration;	// Speed decrease per second. Value should be obtained from TrackMaterial.

	public float TurnSpeed;		// Turn radius of kart. Final turn radius adjusted by speed
	public float SlideRotation;

	public float BounceBack;

	public LayerMask Track;		// Layermask to determain what is ground
	public LayerMask Walls;

	Rigidbody rb;

	bool isBreaking;				// Is the Kart slowing down
	bool isGrounded;				// Is the Kart on the ground
	bool isDrifting;
	
	void Awake () {
	}

	void FixedUpdate () {

		// Input variables
		float _gas = Input.GetAxis ("Vertical");
		float _turning = Input.GetAxis ("Horizontal");
		bool _drifting = Input.GetKey (KeyCode.LeftShift);

		isGrounded = CheckIfGrounded ();
		if (isGrounded) {
			EnforceMaxSpeed ();
		}
		CheckCollision ();

		// Only set isBreaking if we really are breaking
		isBreaking = false;
	
		if (_gas > 0 && isGrounded) {			// Are we accelerating?
			CurrentSpeed += Acceleration * Time.deltaTime;
		} else if (_gas < 0 && isGrounded && CurrentSpeed > 0) {	// Are we breaking?
			isBreaking = true;
			CurrentSpeed -= Breaking * Time.deltaTime;
		} else if (_gas < 0 && isGrounded && CurrentSpeed <= 0){	// Reverse if we are stopped
			isBreaking = false;
			CurrentSpeed -= Breaking * Time.deltaTime;
		}else {									// If we're not
			if (CurrentSpeed > 0) {
				CurrentSpeed -= Deceleration * Time.deltaTime;
			} else if(CurrentSpeed < 0){
				CurrentSpeed -= -Deceleration * Time.deltaTime;
			} else {
				isBreaking = true;
			}
		}

		// Set max reverse speed to 1/5 MaxSpeed
		CurrentSpeed = Mathf.Clamp (CurrentSpeed, -MaxSpeed/5.0f, MaxSpeed);

		//Stop kart completely if it is near 0 speed
		if (CurrentSpeed <= 0.1f && CurrentSpeed >= -0.1f)
			CurrentSpeed = 0;

		// Actually perform the movement / rotations
		if (isGrounded)
			transform.Rotate (transform.up, _turning * TurnSpeed * CurrentSpeed * Time.deltaTime);
		transform.Translate (transform.forward * CurrentSpeed * Time.deltaTime, Space.World);

		//Sliding Algorithm
		// Controllable sliding with Left Shift

		if ((CurrentSpeed >= 0.9 * MaxSpeed && _turning >= 0.9 && Input.GetKey (KeyCode.LeftShift)) && !isDrifting && isGrounded) {
			isDrifting = true;
			transform.Rotate (0, 30, 0);
			SlideRotation = 30;
		}
		if ((CurrentSpeed >= 0.9 * MaxSpeed && _turning <= -0.9 && Input.GetKey (KeyCode.LeftShift)) && !isDrifting && isGrounded) {
			isDrifting = true;
			transform.Rotate (0, -30, 0);
			SlideRotation = -30;
		}
		if (isDrifting && ( (!(CurrentSpeed >= 0.9 * MaxSpeed && (_turning <= -0.9 || _turning >= 0.9))) || !isGrounded || !(Input.GetKey (KeyCode.LeftShift))))  {
			isDrifting = false;
			transform.Rotate (0, -SlideRotation, 0);
		}
		if (isDrifting && _turning > 0)
			transform.Translate (transform.right * -CurrentSpeed * Time.deltaTime, Space.World);
		if (isDrifting && _turning < 0)
			transform.Translate (transform.right * CurrentSpeed * Time.deltaTime, Space.World);

		#region Debug Vectors
		Debug.DrawLine (transform.position, transform.position + transform.right * 3, Color.red);
		Debug.DrawLine (transform.position, transform.position + transform.up * 3, Color.green);
		Debug.DrawLine (transform.position, transform.position + transform.forward * 3, Color.blue);
		#endregion

	}

	void EnforceMaxSpeed () {
		if (CurrentSpeed > MaxSpeed) {
			CurrentSpeed -= Deceleration;
		}
	}

	void CheckCollision () {
		Ray ray = new Ray (transform.position + transform.up * 0.1f, transform.forward);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1.5f, Walls)) {
			CurrentSpeed = 0.0f;
			rb.AddForce (new Vector3(0, 0, -BounceBack), ForceMode.Impulse);
			Debug.Log ("Hit the wall");
		}
	}

	// Checks to see if we're on the ground, and if we are, orients the Kart the proper way
	bool CheckIfGrounded () {
		Ray ray = new Ray ( transform.position + transform.up * 0.1f, -transform.up);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1.0f, Track)) {
			transform.position = hit.point;
			Quaternion rot = Quaternion.FromToRotation (transform.up, hit.normal);
			transform.rotation = rot * transform.rotation;
			return true;
		} else {
			// Have Kart face World.Up when in the air
			Quaternion rot = Quaternion.FromToRotation (transform.up, Vector3.up);
			transform.rotation = rot * transform.rotation;
		}
		return false;
	}
}
