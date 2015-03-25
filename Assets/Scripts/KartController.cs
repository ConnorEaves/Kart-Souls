using UnityEngine;
using System.Collections;

// Attached to empty parent of Kart model parts
public class KartController : MonoBehaviour {
	
	public float MaxSpeed;
	public float Acceleration;	// Speed increase per second
	public float Breaking;		// Speed decrease per second while breaking
	public float Deceleration;	// Speed decrease per second

	public float TurnSpeed;		// Turn radius of kart. Final turn radius adjusted by speed

	public float Bank;			// Amount to rotate chasis during turns

	public LayerMask Track;		// Layermask to determain what is ground [NOT IMPLEMENTED]

	// References to parts of Kart
	public Transform Chasis;
	public Transform FrontRightWheel;
	public Transform FrontLeftWheel;
	public GameObject BreakLightRight;
	public GameObject BreakLightLeft;
	public Transform Player;

	public ParticleSystem WheelParticleRight;
	public ParticleSystem WheelParticleLeft;

	// Reference to spawped materials
	public Material BreakLightOn;
	public Material BreakLightOff;

	//[HideInInspector]
	public float CurrentSpeed;		// Amount Kart will move forward this frame
	bool isBreaking;				// Is the Kart slowing down
	bool isGrounded;				// Is the Kart on the ground

	// Cached references for performance
	MeshRenderer breakLightRight;
	MeshRenderer breakLightLeft;

	ParticleSystem wheelParticleRight;
	ParticleSystem wheelParticleLeft;
	
	void Awake () {
		breakLightRight = BreakLightRight.GetComponent<MeshRenderer> ();
		breakLightLeft = BreakLightLeft.GetComponent<MeshRenderer> ();
		wheelParticleRight = WheelParticleRight.GetComponent<ParticleSystem> ();
		wheelParticleLeft = WheelParticleLeft.GetComponent<ParticleSystem> ();
	}

	void FixedUpdate () {
		// Temp variables cached for performance
		float _gas = Input.GetAxis ("Vertical");
		float _turning = Input.GetAxis ("Horizontal");

		isGrounded = CheckGrounded ();

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
			} else {
				isBreaking = true;
			}
		}

		// Turn break lights on
		if (isBreaking) {
			breakLightLeft.material = BreakLightOn;
			breakLightRight.material = BreakLightOn;
		} else {
			breakLightLeft.material = BreakLightOff;
			breakLightRight.material = BreakLightOff;
		}

		// Set max reverse speed to 1/5 MaxSpeed
		Debug.Log (CurrentSpeed);
		CurrentSpeed = Mathf.Clamp (CurrentSpeed, -MaxSpeed/5.0f, MaxSpeed);
	
		// Take care of Kart animations
		Chasis.localRotation = Quaternion.Euler (0, 0, _turning * Bank * Mathf.Abs(CurrentSpeed));
		Player.localRotation = Quaternion.Euler (0, 0, _turning * -Bank * Mathf.Abs(CurrentSpeed));
		FrontRightWheel.localRotation = Quaternion.Euler (_turning * TurnSpeed, 0, 0);
		FrontLeftWheel.localRotation = Quaternion.Euler (_turning * TurnSpeed, 0, 0);

		// Particle system controls
		if (isGrounded) {
			wheelParticleRight.startSpeed = Mathf.Abs(CurrentSpeed);
			wheelParticleRight.maxParticles = (int)Mathf.Abs(CurrentSpeed) * 10;
			wheelParticleRight.gravityModifier = Mathf.Abs(CurrentSpeed) * 0.01f;
			wheelParticleLeft.startSpeed = Mathf.Abs(CurrentSpeed);
			wheelParticleLeft.maxParticles = (int)Mathf.Abs(CurrentSpeed) * 10;
			wheelParticleLeft.gravityModifier = Mathf.Abs(CurrentSpeed) * 0.01f;
		} else {
			wheelParticleRight.startSpeed = 0;
			wheelParticleRight.maxParticles = 0;
			wheelParticleRight.gravityModifier = 0;
			wheelParticleLeft.startSpeed = 0;
			wheelParticleLeft.maxParticles = 0;
			wheelParticleLeft.gravityModifier = 0;
		}

		// Actually perform the movement / rotations
		transform.Rotate (transform.up, _turning * TurnSpeed * CurrentSpeed * Time.deltaTime);
		transform.Translate (transform.forward * CurrentSpeed * Time.deltaTime, Space.World);

		#region Debug Vectors
		Debug.DrawLine (transform.position, transform.position + transform.right * 3, Color.red);
		Debug.DrawLine (transform.position, transform.position + transform.up * 3, Color.green);
		Debug.DrawLine (transform.position, transform.position + transform.forward * 3, Color.blue);
		#endregion

	}

	// Checks to see if we're on the ground, and if we are, orients the Kart the proper way
	bool CheckGrounded () {
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
