using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicsKartController : MonoBehaviour {

	public List <Axel> Axels;
	public float MaxMotorTorque;
	public float MaxBrakeTorque;
	public float MaxSteeringAngle;

	Rigidbody rb;

	void Awake () {

		rb = GetComponent<Rigidbody> ();
		Debug.Log ("Original Center of mass: " + rb.centerOfMass);

		rb.centerOfMass = transform.forward * 0.5f - transform.up * 0.2f;
		Debug.Log ("New Center of mass: " + rb.centerOfMass);

	}

	void FixedUpdate () {
		float motor = MaxMotorTorque * Input.GetAxis ("Vertical");
		float steering = MaxSteeringAngle * Input.GetAxis ("Horizontal");
		float braking = Input.GetKey (KeyCode.Space) ? MaxBrakeTorque : 0.0f;

		foreach(Axel a in Axels) {
			if (a.Steering) {
				a.LeftWheel.steerAngle = steering;
				a.RightWheel.steerAngle = steering;
			}
			if (a.Motor) {
				a.LeftWheel.motorTorque = motor;
				a.RightWheel.motorTorque = motor;
				a.LeftWheel.brakeTorque = braking;
				a.RightWheel.brakeTorque = braking;
			}
		}
	}
}

[System.Serializable]
public class Axel {
	public WheelCollider LeftWheel;
	public WheelCollider RightWheel;
	public bool Motor;
	public bool Steering;
}
