using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SmoothFollow : MonoBehaviour {

	public Transform player;
	public float inLimit;
	public float outLimit;
	public float speed;	
	
	Vector3 desiredPosition;
	
	
	
	public Vector3 distance;
	public Vector3 targetVelocity;
	public Vector3 error;
	public Vector3 force;
	
	public float distanceFactor = 2.5f;
	public float maxVelocity = 15.0f;
	public float maxForce = 40.0f;
	public float gain = 5f;
	
	void Update () {
	}
		
	void LateUpdate () {
		desiredPosition = player.position;									
		
		distance = desiredPosition - transform.position;
		targetVelocity = Vector3.ClampMagnitude(distanceFactor * distance, maxVelocity);
		error = targetVelocity - rigidbody.velocity;
		force = Vector3.ClampMagnitude(gain * error, maxForce);
		rigidbody.AddForce(force);   
																														
																													
//		if (Vector3.Distance(transform.position, desiredPosition) > inLimit) {
//			if (Vector3.Distance(transform.position, desiredPosition) > outLimit){
//				transform.position = Lerp(transform.position, desiredPosition, speed);
//			}
//		}					
	}
	
	public static Vector3 Lerp(Vector3 start, Vector3 finish, float percentage)
	{
		//Make sure percentage is in the range [0.0, 1.0]
		percentage = Mathf.Clamp01(percentage);
		
		//(finish-start) is the Vector3 drawn between 'start' and 'finish'
		Vector3 startToFinish = finish - start;
		
		//Multiply it by percentage and set its origin to 'start'
		return start + startToFinish * percentage;
	}
}