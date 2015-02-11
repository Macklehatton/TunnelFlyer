using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {

	public GameObject target;
	public float followDist;

	public float heightOffset;
	public Vector3 targetPos;

	public float toVel = 2.5f;
	public float maxVel = 15.0f;
	public float maxForce = 40.0f;
	public float gain = 5f;

	void LateUpdate() {

		targetPos = new Vector3 (target.transform.position.x, target.transform.position.y - heightOffset, target.transform.position.z - followDist);

		Vector3 dist = targetPos - transform.position;
		// calc a target vel proportional to distance (clamped to maxVel)
		Vector3 tgtVel = Vector3.ClampMagnitude(toVel * dist, maxVel);
		// calculate the velocity error
		Vector3 error = tgtVel - rigidbody.velocity;
		// calc a force proportional to the error (clamped to maxForce)
		Vector3 force = Vector3.ClampMagnitude(gain * error, maxForce);
		rigidbody.AddForce(force);

	}
}