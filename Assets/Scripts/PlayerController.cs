using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour 
{
	public float leftH;
	public float leftV;

	public Vector3 strafeMove;
	public Vector3 desiredMove;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
	
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
		desiredMove = transform.position + transform.up * input.y * dodgeSpeed + transform.right * input.x * dodgeSpeed;
		                                   
		distance = desiredMove - transform.position;
		targetVelocity = Vector3.ClampMagnitude(distanceFactor * distance, maxVelocity);
		error = targetVelocity - rigidbody.velocity;
		force = Vector3.ClampMagnitude(gain * error, maxForce);
		rigidbody.AddForce(force);                                   		
		
=======
=======
>>>>>>> parent of 54281cb... Basic movement working again
=======
>>>>>>> parent of 54281cb... Basic movement working again

	public Camera playerCam;

	public float dodgeSpeed;

	public float followDist;

	void Update ()	{
		leftH = CrossPlatformInput.GetAxis ("Horizontal");
		leftV = CrossPlatformInput.GetAxis ("Vertical");

		strafeMove = new Vector3 (leftH * Screen.width / 4 + (Screen.width / 2), 
		                          leftV * Screen.height / 4 + (Screen.height / 2) - Screen.height / 6, 
		                          followDist);

 		desiredMove = playerCam.ScreenToWorldPoint(strafeMove);

		transform.position = Vector3.Lerp (transform.position, 
		                                   desiredMove, 
		                                   Mathf.SmoothStep(0.0f, 1.0f, dodgeSpeed));
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 54281cb... Basic movement working again
=======
>>>>>>> parent of 54281cb... Basic movement working again
=======
>>>>>>> parent of 54281cb... Basic movement working again
	}
}