using UnityEngine;
using System.Collections;

// Attached to empty parent of Kart model parts
public class KartController : MonoBehaviour {
	
	public float MaxSpeed;
	public float Acceleration;	// Speed increase per second
	public float Breaking;		// Speed decrease per second while breaking
	public float Deceleration;	// Speed decrease per second

	public float TurnSpeed;		// Turn radius of kart. Final turn radius adjusted by speed
	public float SlideRotation;

	public float Bank;			// Amount to rotate chasis during turns
	public float BounceBack;

	public LayerMask Track;		// Layermask to determain what is ground
	public LayerMask Walls;

	// References to parts of Kart
	public Transform Chasis;
	public Transform FrontRightWheel;
	public Transform FrontLeftWheel;
	public GameObject BreakLightRight;
	public GameObject BreakLightLeft;
	public Transform Player;

	public ParticleSystem WheelParticleRight;
	public ParticleSystem WheelParticleLeft;
	public ParticleSystem EngineParticleRight;
	public ParticleSystem EngineParticleLeft;

	// Reference to spawped materials
	public Material BreakLightOn;
	public Material BreakLightOff;
	public Material BreakLightReverse;

	//[HideInInspector]
	public float CurrentSpeed;		// Amount Kart will move forward this frame
	bool isBreaking;				// Is the Kart slowing down
	bool isGrounded;				// Is the Kart on the ground
	bool isSliding;
	Color groundColor;
	
	// Cached references for performance
	Rigidbody rb;
	MeshRenderer breakLightRight;
	MeshRenderer breakLightLeft;
	ParticleSystem wheelParticleRight;
	ParticleSystem wheelParticleLeft;
	ParticleSystem engineParticleRight;
	ParticleSystem engineParticleLeft;
	
	void Awake () {
		rb = GetComponent<Rigidbody> ();
		breakLightRight = BreakLightRight.GetComponent<MeshRenderer> ();
		breakLightLeft = BreakLightLeft.GetComponent<MeshRenderer> ();
		wheelParticleRight = WheelParticleRight.GetComponent<ParticleSystem> ();
		wheelParticleLeft = WheelParticleLeft.GetComponent<ParticleSystem> ();
		engineParticleRight = EngineParticleRight.GetComponent<ParticleSystem> ();
		engineParticleLeft = EngineParticleLeft.GetComponent<ParticleSystem> ();
	}

	void FixedUpdate () {
		// Temp variables cached for performance
		float _gas = Input.GetAxis ("Vertical");
		float _turning = Input.GetAxis ("Horizontal");

		isGrounded = CheckGrounded ();
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

		// Turn break lights on
		if (isBreaking) {
			breakLightLeft.material = BreakLightOn;
			breakLightRight.material = BreakLightOn;
		} else if (CurrentSpeed < 0) {
			breakLightLeft.material = BreakLightReverse;
			breakLightRight.material = BreakLightReverse;
		} else {
			breakLightLeft.material = BreakLightOff;
			breakLightRight.material = BreakLightOff;
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

		if ((CurrentSpeed >= 0.9 * MaxSpeed && _turning >= 0.9 && Input.GetKey (KeyCode.LeftShift)) && !isSliding && isGrounded) {
			isSliding = true;
			transform.Rotate (0, 30, 0);
			SlideRotation = 30;
		}
		if ((CurrentSpeed >= 0.9 * MaxSpeed && _turning <= -0.9 && Input.GetKey (KeyCode.LeftShift)) && !isSliding && isGrounded) {
			isSliding = true;
			transform.Rotate (0, -30, 0);
			SlideRotation = -30;
		}
		if (isSliding && ( (!(CurrentSpeed >= 0.9 * MaxSpeed && (_turning <= -0.9 || _turning >= 0.9))) || !isGrounded || !(Input.GetKey (KeyCode.LeftShift))))  {
			isSliding = false;
			transform.Rotate (0, -SlideRotation, 0);
		}
		if (isSliding && _turning > 0)
			transform.Translate (transform.right * -CurrentSpeed * Time.deltaTime, Space.World);
		if (isSliding && _turning < 0)
			transform.Translate (transform.right * CurrentSpeed * Time.deltaTime, Space.World);

		// Take care of Kart animations
		Chasis.localRotation = Quaternion.Euler (0, 0, _turning * Bank * Mathf.Abs(CurrentSpeed));
		Player.localRotation = Quaternion.Euler (0, 0, _turning * -Bank * Mathf.Abs(CurrentSpeed));
		FrontRightWheel.localRotation = isSliding ? Quaternion.Euler (-_turning * SlideRotation, 0, 0) : Quaternion.Euler (_turning * TurnSpeed, 0, 0);
		FrontLeftWheel.localRotation = isSliding ? Quaternion.Euler (_turning * SlideRotation, 0, 0) : Quaternion.Euler (_turning * TurnSpeed, 0, 0);
		
		// Particle system controls
		if (isGrounded) {
			wheelParticleRight.startSpeed = Mathf.Abs(CurrentSpeed);
			wheelParticleRight.emissionRate = (int)Mathf.Abs(CurrentSpeed);
			wheelParticleRight.gravityModifier = Mathf.Abs(CurrentSpeed) * 0.1f;
			wheelParticleRight.startColor = groundColor;
			
			wheelParticleLeft.startSpeed = Mathf.Abs(CurrentSpeed);
			wheelParticleLeft.emissionRate = (int)Mathf.Abs(CurrentSpeed);
			wheelParticleLeft.gravityModifier = Mathf.Abs(CurrentSpeed) * 0.1f;
			wheelParticleLeft.startColor = groundColor;
		} else {
			wheelParticleRight.startSpeed = 0;
			wheelParticleRight.emissionRate = 0;
			wheelParticleRight.gravityModifier = 0;
			wheelParticleLeft.startSpeed = 0;
			wheelParticleLeft.emissionRate = 0;
			wheelParticleLeft.gravityModifier = 0;
		}
		engineParticleRight.emissionRate = Mathf.Abs (10 + CurrentSpeed);
		engineParticleLeft.emissionRate = Mathf.Abs (10 + CurrentSpeed);

		#region Debug Vectors
		Debug.DrawLine (transform.position, transform.position + transform.right * 3, Color.red);
		Debug.DrawLine (transform.position, transform.position + transform.up * 3, Color.green);
		Debug.DrawLine (transform.position, transform.position + transform.forward * 3, Color.blue);
		#endregion

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
	bool CheckGrounded () {
		//Turned this off temporarily to stop bugs with new track
		/* 
		Ray ray = new Ray ( transform.position + transform.up * 0.1f, -transform.up);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1.0f, Track)) {
			transform.position = hit.point;
			Quaternion rot = Quaternion.FromToRotation (transform.up, hit.normal);
			transform.rotation = rot * transform.rotation;

			groundColor = hit.collider.gameObject.GetComponent<MeshRenderer> ().material.color;

			return true;
		} else {
			// Have Kart face World.Up when in the air
			Quaternion rot = Quaternion.FromToRotation (transform.up, Vector3.up);
			transform.rotation = rot * transform.rotation;
		}
		return false;*/
		return true;
	}
}
