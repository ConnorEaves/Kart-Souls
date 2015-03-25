using UnityEngine;
using System.Collections;

// Attached to empty parent of Kart model parts
public class KartController : MonoBehaviour {
	
	public float MaxSpeed;
	public float Acceleration;	// Speed increase per second
	public float Deceleration;	// Speed decrease per second

	public float TurnSpeed;		// Turn radius of kart. Final turn radius adjusted by speed

	public float Bank;			// Amount to rotate chasis during turns
	public float WheelTurning;	// Amount to rotate wheels. Should be tweaked to match TurnSpeed


	public LayerMask Track;		// Layermask to determain what is ground [NOT IMPLEMENTED]

	// References to parts of Kart
	public Transform Chasis;
	public Transform FrontRightWheel;
	public Transform FrontLeftWheel;
	public GameObject BreakLightRight;
	public GameObject BreakLightLeft;
	public Transform Player;

	// Reference to spawped materials
	public Material BreakLightOn;
	public Material BreakLightOff;

	float currentSpeed;			// Amount Kart will move forward this frame
	bool isBreaking;			// Is the Kart slowing down

	MeshRenderer breakLightRight;	// Cached references for performance
	MeshRenderer breakLightLeft;
	
	void Awake () {
		breakLightRight = BreakLightRight.GetComponent<MeshRenderer> ();
		breakLightLeft = BreakLightLeft.GetComponent<MeshRenderer> ();
	}

	void Update () {
		// Temp variables cached for performance
		float _gas = Input.GetAxisRaw ("RightTrigger360");
		float _turning = Input.GetAxisRaw ("Horizontal");

		// Only set isBreaking if we really are breaking
		isBreaking = false;

		if (_gas > 0) {	// Are we accelerating?
			currentSpeed += Acceleration * Time.deltaTime;
		} else {		// If we're not
			if (currentSpeed >= 0) {
				isBreaking = true;
				currentSpeed -= Deceleration * Time.deltaTime;
			}
		}

		if (isBreaking) {
			breakLightLeft.material = BreakLightOn;
			breakLightRight.material = BreakLightOn;
		} else {
			breakLightLeft.material = BreakLightOff;
			breakLightRight.material = BreakLightOff;
		}

		// Disallow going in reverse (for now)
		currentSpeed = Mathf.Clamp (currentSpeed, 0.0f, MaxSpeed);
	
		// Take care of Kart animations
		Chasis.localRotation = Quaternion.Euler (0, 0, _turning * Bank * currentSpeed);
		FrontRightWheel.localRotation = Quaternion.Euler (_turning * WheelTurning, 0, 0);
		FrontLeftWheel.localRotation = Quaternion.Euler (_turning * WheelTurning, 0, 0);
		Player.localRotation = Quaternion.Euler (0, 0, _turning * -Bank * currentSpeed);

		// Actually perform the movement / rotations
		transform.Rotate (transform.up, _turning * TurnSpeed * currentSpeed * Time.deltaTime);
		transform.Translate (transform.forward * currentSpeed * Time.deltaTime, Space.World);

		#region Debug Vectors
		Debug.DrawLine (transform.position, transform.position + transform.right * 3, Color.red);
		Debug.DrawLine (transform.position, transform.position + transform.up * 3, Color.green);
		Debug.DrawLine (transform.position, transform.position + transform.forward * 3, Color.blue);
		#endregion

	}
	
}
