using UnityEngine;
using System.Collections;

// Attached to empty parent of Kart model parts
public class KartController : MonoBehaviour {

	public float CurrentSpeed;	// Speed we are going right now
	public float MaxSpeed;		// Maximum allowed speed
	public float Acceleration;	// Speed increase per second
	public float Braking;		// Speed decrease per second while breaking
	public float Deceleration;	// Speed decrease per second. Value should be obtained from TrackMaterial.
	[Range (1.0f, 5.0f)]
	public float TurnSpeed;		// Turn radius of kart. Final turn radius adjusted by speed
	public float GroundedDistance;

	public float BounceBack;

	public LayerMask Track;		// Layermask to determain what is ground
	public LayerMask Walls;

	int numJoysticks;
	
	bool isGrounded;			// Is the Kart on the ground
	bool isGas;
	bool isBraking;
	bool isTurning;

	float accel;
	float decel;
	float brakeAmount;
	float turnAmount;

	TrackMaterial trackMaterial;
	
	void Awake () {
		LogJoysticks ();
	}

	void Update () {

		isGrounded = CheckIfGrounded ();
		GetInput ();
		MoveKart ();

		#region Debug Vectors
		Debug.DrawLine (transform.position, transform.position + transform.right, Color.red);
		Debug.DrawLine (transform.position, transform.position + transform.up, Color.green);
		Debug.DrawLine (transform.position, transform.position + transform.forward, Color.blue);
		#endregion

	}

	void LogJoysticks () {
		string[] joysticks = Input.GetJoystickNames ();
		for (int i = 0; i < joysticks.Length; i++) {
			Debug.Log (joysticks[i]);
			numJoysticks = i;
		}
	}

	bool CheckIfGrounded () {
		bool grounded = false;
		Ray ray = new Ray (transform.position + 0.1f * transform.up, -transform.up);
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, Track)) {
			if (hit.distance < GroundedDistance) {
				transform.position = hit.point;
				Quaternion rot = Quaternion.FromToRotation (transform.up, hit.normal);
				transform.rotation = rot * transform.rotation;

				trackMaterial = hit.collider.gameObject.GetComponent<TrackMaterial> ();
				return (true);
			} else {
				Quaternion rot = Quaternion.FromToRotation (transform.up, Vector3.up);
				transform.rotation = rot * transform.rotation;
			}
			return (false);
		}

		return (grounded);
	}

	void GetInput () {
		if (isGrounded) {
			isGas = Input.GetKey (KeyCode.Space);
			isBraking = Input.GetKey (KeyCode.S);
			accel = Acceleration * trackMaterial.Grip * Time.deltaTime;
			decel = Deceleration * trackMaterial.Grip * Time.deltaTime;
			brakeAmount = Braking * trackMaterial.Grip * Time.deltaTime;
			turnAmount = TurnSpeed * Input.GetAxis ("Horizontal") * Time.deltaTime;
		}
	}

	void MoveKart () {
		if (isGrounded) {
			if (isGas) {
				if (CurrentSpeed < MaxSpeed) {
					CurrentSpeed += accel;
				}
			} else {
				if (CurrentSpeed > 0) {
					CurrentSpeed -= decel;
				}
				if (CurrentSpeed < 0) {
					CurrentSpeed += decel;
				}
			}
			
			if (isBraking) {
				if (CurrentSpeed > 0) {
					CurrentSpeed -= brakeAmount;
				} else if (CurrentSpeed < 0) {
					CurrentSpeed -= accel;
				}
			}

			CurrentSpeed = Mathf.Clamp (CurrentSpeed, -MaxSpeed / 5, MaxSpeed);
			transform.Rotate (transform.up, turnAmount * CurrentSpeed);
		}
		transform.Translate (Vector3.forward * CurrentSpeed * Time.deltaTime);
	}
}
