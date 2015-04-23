using UnityEngine;
using System.Collections;

// Attached to empty parent of Kart model parts
public class KartController : MonoBehaviour {
	public string kartName;
	public float MaxSpeed;
	public float Acceleration;	// Speed increase per second
	public float Breaking;		// Speed decrease per second while breaking
	public float Deceleration;	// Speed decrease per second

	public float TurnSpeed;		// Turn radius of kart. Final turn radius adjusted by speed
	public float SlideRotation;

	public LayerMask Track;		// Layermask to determain what is ground
	public LayerMask Walls;
	public bool playerController;

	private float _gas;
	private float _brake;
	private float _turning;
	// References to parts of Kart
	public Transform Player;

	public ParticleSystem WheelParticleRight;
	public ParticleSystem WheelParticleLeft;
	public ParticleSystem EngineParticleRight;
	public ParticleSystem EngineParticleLeft;

	//reference to animator
	public Animator anim;
	//for tracking animation
	private int animationIsPlaying;


	//[HideInInspector]
	public float CurrentSpeed;		// Amount Kart will move forward this frame
	bool isGrounded;				// Is the Kart on the ground
	bool isSliding;
	Color groundColor;

	
	// Cached references for performance
	Rigidbody rb;
	MeshRenderer breakLightLeft;
	ParticleSystem wheelParticleRight;
	ParticleSystem wheelParticleLeft;
	ParticleSystem engineParticleRight;
	ParticleSystem engineParticleLeft;
	
	void Awake () {
		wheelParticleRight = WheelParticleRight.GetComponent<ParticleSystem> ();
		wheelParticleLeft = WheelParticleLeft.GetComponent<ParticleSystem> ();
		engineParticleRight = EngineParticleRight.GetComponent<ParticleSystem> ();
		engineParticleLeft = EngineParticleLeft.GetComponent<ParticleSystem> ();
		//initialize animationIsPlaying
		animationIsPlaying = 0;
	}
	void Update (){
		if (playerController) {
			_turning = Input.GetAxis("Horizontal");
			if (Input.GetButtonDown("Fire1")){
				_gas = 1.0f;
			}
			if (Input.GetButtonUp("Fire1")){
				_gas = 0.0f;
			}
			if (Input.GetButtonDown("Fire2")){
				_brake = -1.0f;
			}
			if (Input.GetButtonUp("Fire2")){
				_brake = 0.0f;
			}
			_gas = _gas + _brake;

		}
	}

	void LateUpdate(){
		if (playerController) {
			_turning = Input.GetAxis ("Horizontal");
			
			//Determine which turning animation to play
			if (_turning < 0) {
				if (animationIsPlaying != 1) {
					anim.SetTrigger ("LeftTurn");
					animationIsPlaying = 1;
					
				}
			}
			if (_turning > 0) {
				if (animationIsPlaying != 2) {
					anim.SetTrigger ("RightTurn");
					animationIsPlaying = 2;
				}
			}
			if (_turning == 0) {
				anim.SetTrigger ("Idle");
				animationIsPlaying = 0;
			}
		} else {
			anim.SetTrigger ("Idle");
			animationIsPlaying = 0;}
	}

	void FixedUpdate () {
		// Temp variables cached for performance



		isGrounded = CheckGrounded ();




	
		if (_gas > 0 && isGrounded) {			// Are we accelerating?
			CurrentSpeed += Acceleration * Time.deltaTime;
		} else if (_gas < 0 && isGrounded && CurrentSpeed > 0) {	// Are we breaking?
			CurrentSpeed -= Breaking * Time.deltaTime;
		} else if (_gas < 0 && isGrounded && CurrentSpeed <= 0){	// Reverse if we are stopped
			CurrentSpeed -= Breaking * Time.deltaTime;
		}else {									// If we're not
			if (CurrentSpeed > 0) {
				CurrentSpeed -= Deceleration * Time.deltaTime;
			} else if(CurrentSpeed < 0){
				CurrentSpeed -= -Deceleration * Time.deltaTime;
			} else {
			}
		}

		// Set max reverse speed to 1/5 MaxSpeed
		CurrentSpeed = Mathf.Clamp (CurrentSpeed, -MaxSpeed/5.0f, MaxSpeed);

		//Stop kart completely if it is near 0 speed
		if (CurrentSpeed <= 0.01f && CurrentSpeed >= -0.01f)
			CurrentSpeed = 0;

		// Actually perform the movement / rotations
		if (isGrounded)
			transform.Rotate (transform.up, _turning * TurnSpeed * CurrentSpeed * Time.deltaTime);
		transform.Translate (transform.forward * CurrentSpeed * Time.deltaTime, Space.World);

		//Sliding Algorithm
		// Controllable sliding with Left Shift

		if ((CurrentSpeed >= 0.9 * MaxSpeed && _turning >= 0.9 && Input.GetButton("Fire3")) && !isSliding && isGrounded && playerController) {
			isSliding = true;
			transform.Rotate (0, 30, 0);
			SlideRotation = 30;
		}
		if ((CurrentSpeed >= 0.9 * MaxSpeed && _turning <= -0.9 && Input.GetButton("Fire3")) && !isSliding && isGrounded && playerController) {
			isSliding = true;
			transform.Rotate (0, -30, 0);
			SlideRotation = -30;
		}
		if (isSliding && ( (!(CurrentSpeed >= 0.9 * MaxSpeed && (_turning <= -0.9 || _turning >= 0.9))) || !isGrounded || !(Input.GetButton("Fire3"))))  {
			isSliding = false;
			transform.Rotate (0, -SlideRotation, 0);
		}
		if (isSliding && _turning > 0)
			transform.Translate (transform.right * -CurrentSpeed * Time.deltaTime, Space.World);
		if (isSliding && _turning < 0)
			transform.Translate (transform.right * CurrentSpeed * Time.deltaTime, Space.World);

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

	//Reduce speed based on angle of impact with a wall. Up to a maximum of 100%
	void OnCollisionEnter(Collision coll){
		float angle = 0;
		if (coll.collider.tag == "wall") {
			Ray ray = new Ray (transform.position + transform.up * 0.5f, transform.forward);
			Debug.DrawRay(transform.position + transform.up * 0.5f, transform.forward);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 5.0f)){
				angle = Vector3.Angle(hit.normal,transform.forward);
			}
			CurrentSpeed -= (CurrentSpeed * (angle - 90)/90);
		}
		//The raycast would often go through the bars, the kart will always hit the bars at a near 90 degree angle so we just stop it completely on collision
		if (coll.collider.tag == "bars") {
			CurrentSpeed = 0.0f;
		}
	}


	// Checks to see if we're on the ground, and if we are, orients the Kart the proper way
	bool CheckGrounded () {

		Ray ray = new Ray ( transform.position + transform.up * 0.1f + transform.forward * -0.5f, -transform.up);
		Ray ray1 = new Ray ( transform.position + transform.up * 0.1f + transform.forward * 0.5f, -transform.up);
		RaycastHit hit;
		RaycastHit hit1;
		if (Physics.Raycast (ray, out hit, 1.0f, Track)) {
			if (Physics.Raycast (ray1, out hit1, 1.0f, Track)) {
				if (hit.normal == hit1.normal){
				//transform.position = hit.point;
				Quaternion rot = Quaternion.FromToRotation (transform.up, hit.normal);
				transform.rotation = rot * transform.rotation;
				}
			}
			return true;
		} else {
			// Have Kart face World.Up when in the air
			Quaternion rot = Quaternion.FromToRotation (transform.up, Vector3.up);
			transform.rotation = rot * transform.rotation;
		}
		return false;
	}

	public void gameInput(float gas, float turn){
		_gas = gas;
		_turning = turn;
	}
}
