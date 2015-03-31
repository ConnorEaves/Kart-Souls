using UnityEngine;
using System.Collections;

// Attached to camera we want to do the following
public class CameraFollow : MonoBehaviour {

	// The Transform we are following
	public Transform Target;

	// Controls how quickly the camera catches up with targetPos. Larger values decrease movement time
	public float MoveSnapiness;
	public float LookSnapiness;

	// Specifies where camera targetPos is. SerializeField allows private vars to be seen in inspector
	[SerializeField]
	float camDistY;
	[SerializeField]
	float camDistZ;

	void LateUpdate () {
	
		Vector3 targetPos = Target.position + Target.up * camDistY - Target.forward * camDistZ;

		// Look forward at all times
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (Target.forward, Target.up), LookSnapiness * Time.deltaTime);

		// Move towards targetPos
		transform.position = Vector3.Lerp (transform.position, targetPos, MoveSnapiness * Time.deltaTime);

		// Just helps to see what is going on
		#region Debug Vectors
		Debug.DrawLine (targetPos, targetPos - Target.up * camDistY, Color.cyan);
		Debug.DrawLine (Target.position, Target.position - Target.forward * camDistZ, Color.cyan);
		Debug.DrawLine (Target.position, targetPos, Color.yellow);
		Debug.DrawLine (targetPos, transform.position, Color.magenta);
		#endregion

	}
}
