using UnityEngine;
using System.Collections;

public class PhysicsController : MonoBehaviour {

	public float leftH;
	public float leftV;
	
	public Vector2 input;
	public float maxStrafeSpeed;
	public float strafeThrust;

	public float maxForwardSpeed;
	public float forwardThrust;

	public float currentSpeed;
	Vector3 lastPosition;

	void FixedUpdate () {
		//measure speed
		currentSpeed = (transform.position - lastPosition).magnitude;
		lastPosition = transform.position;		
		
		leftH = CrossPlatformInput.GetAxis ("Horizontal");
		leftV = CrossPlatformInput.GetAxis ("Vertical");
		
		input = new Vector2( leftH, leftV );

		Vector3 strafeDir = new Vector3 (input.x, input.y, 0f);

		if (Mathf.Abs(GetComponent<Rigidbody>().velocity.x) > maxStrafeSpeed || Mathf.Abs(GetComponent<Rigidbody>().velocity.y) > maxStrafeSpeed) {
			return;
		} else {
		GetComponent<Rigidbody>().AddRelativeForce(strafeDir * strafeThrust);			
		}

		if (GetComponent<Rigidbody>().velocity.z > maxForwardSpeed) {
			return;
		}else {
			GetComponent<Rigidbody>().AddRelativeForce(forwardThrust * Vector3.forward);
		}
	}
}
