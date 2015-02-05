using UnityEngine;
using System.Collections;


public class PlayerControllerII : MonoBehaviour 
{
	public float leftH;
	public float leftV;
	
	public Vector2 input;
	
	public Vector3 strafeMove;
	public Vector3 desiredMove;
	
	public float speed;
	public float dodgeSpeed;
	
//	public Vector3 distance;
//	public Vector3 targetVelocity;
//	public Vector3 error;
//	public Vector3 force;
//	
//	public float distanceFactor = 2.5f;
//	public float maxVelocity = 15.0f;
//	public float maxForce = 40.0f;
//	public float gain = 5f;


	public float maxForce = 100f; // the max force available
	public float pGain = 20f; // the proportional gain
	public float iGain = 0.5f; // the integral gain
	public float dGain = 0.5f; // differential gain
	public Vector3 integrator = Vector3.zero; // error accumulator
	Vector2 lastError = Vector2.zero; 
	Vector2 curPos = Vector3.zero; // actual Pos
	Vector3 force = Vector3.zero; // current force

	void Start () {
		desiredMove = Vector3.zero;
	}


	void Update () {
		leftH = CrossPlatformInput.GetAxis ("Horizontal");
		leftV = CrossPlatformInput.GetAxis ("Vertical");		
		input = new Vector3( leftH, leftV, 0f );	
		desiredMove = transform.position;
	}
	
	void FixedUpdate ()	{					
		// Separate strafe force and forward force, the ship currently slows down when strafing
		//desiredMove = transform.position + transform.up * input.y * dodgeSpeed + transform.right * input.x * dodgeSpeed;



		curPos = transform.position;
		Vector3 error = desiredMove - new Vector3 (curPos.x, curPos.y, 0f); // generate the error signal
		integrator += error * Time.deltaTime; // integrate error
		Vector3 diff = (error - new Vector3 (lastError.x, lastError.y, 0f))/ Time.deltaTime; // differentiate error
		lastError = error;
		// calculate the force summing the 3 errors with respective gains:
		force = error * pGain + integrator * iGain + diff * dGain;
		// clamp the force to the max value available
		force = Vector3.ClampMagnitude(force, maxForce);
		// apply the force to accelerate the rigidbody:
		rigidbody.AddForce(force);                                  		
		
	}
}