using UnityEngine;
using System.Collections;

public class PIDCameraFollow : MonoBehaviour {

	public GameObject target;

	Vector3 targetPosition;

	public float maxForce;
	public float pGain;
	public float iGain;
	public float dGain;

	Vector3 integrator;
	Vector3 lastError;
	Vector3 force;

	void FixedUpdate () {
		targetPosition = target.transform.position;

		float distance = (targetPosition - transform.position).magnitude;

		Vector3 error = targetPosition - transform.position; // generate the error signal
		integrator += error * Time.deltaTime; // integrate error
		Vector3 differential = (error - lastError)/ Time.deltaTime; // differentiate error
		lastError = error;
		// calculate the force summing the 3 errors with respective gains:
		force = error * pGain + integrator * iGain + differential * dGain;
		// clamp the force to the max value available
		force = Vector3.ClampMagnitude(force, maxForce);
		// apply the force to accelerate the rigidbody:
		GetComponent<Rigidbody>().AddForce(force);
	}
}