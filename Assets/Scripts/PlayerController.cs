using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour 
{
	public float leftH;
	public float leftV;
	
	public Vector2 input;
	
	public Vector3 strafeMove;
	public Vector3 desiredMove;
	
	public float speed;
	public float dodgeSpeed;
	
	public Vector3 distance;
	public Vector3 targetVelocity;
	public Vector3 error;
	public Vector3 force;
	
	public float distanceFactor = 2.5f;
	public float maxVelocity = 15.0f;
	public float maxForce = 40.0f;
	public float gain = 5f;
	
	void Update () {
		leftH = CrossPlatformInput.GetAxis ("Horizontal");
		leftV = CrossPlatformInput.GetAxis ("Vertical");		
		input = new Vector2( leftH, leftV );	
	}
	
	void FixedUpdate ()	{					
		// Separate strafe force and forward force, the ship currently slows down when strafing
		desiredMove = transform.position + transform.up * input.y * dodgeSpeed + transform.right * input.x * dodgeSpeed + transform.forward * speed;
		                                   
		distance = desiredMove - transform.position;
		targetVelocity = Vector3.ClampMagnitude(distanceFactor * distance, maxVelocity);
		error = targetVelocity - rigidbody.velocity;
		force = Vector3.ClampMagnitude(gain * error, maxForce);
		rigidbody.AddForce(force);                                   		
		
	}
}