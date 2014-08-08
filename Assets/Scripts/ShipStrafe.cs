using UnityEngine;
using System.Collections;

public class ShipStrafe : MonoBehaviour {
	
	public float forwardSpeed;	
	float speed;
	
	public float followDist;
	
	public float leftH;
	public float leftV;
	public float vertOffset;
	
	public Camera playerCam;
	
	public Vector3 screenPosition;
	public Vector3 desiredMove;
	public Vector3 distance;
	public Vector3 targetVelocity;
	public Vector3 error;
	public Vector3 force;
	
	public float distanceFactor = 2.5f;
	public float maxVelocity = 15.0f;
	public float maxForce = 40.0f;
	public float gain = 5f;
	
	
	void FixedUpdate () {				
		leftH = CrossPlatformInput.GetAxis ("Horizontal");
		leftV = CrossPlatformInput.GetAxis ("Vertical");
		
		screenPosition = new Vector3 (leftH * Screen.width / 3 + (Screen.width / 2), 
		                              leftV * Screen.height / 3 + (Screen.height / 2) - Screen.height / vertOffset, 
		                              3.5f);
		
		speed += forwardSpeed;		
		
		desiredMove = playerCam.ScreenToWorldPoint(screenPosition);				
		desiredMove.z = speed;
		
		distance = desiredMove - transform.position;
		targetVelocity = Vector3.ClampMagnitude(distanceFactor * distance, maxVelocity);
		error = targetVelocity - rigidbody.velocity;
		force = Vector3.ClampMagnitude(gain * error, maxForce);
		rigidbody.AddForce(force);
		
		
	}
}
