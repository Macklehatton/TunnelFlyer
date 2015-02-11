using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour 
{
	public float leftH;
	public float leftV;
	
	public Vector2 input;	
	public Vector3 desiredMove;

	public float lerpTime;
	public float moveSpeed;

	public float followDist;
	
	void Update ()	{
	
		leftH = CrossPlatformInput.GetAxis ("Horizontal");
		leftV = CrossPlatformInput.GetAxis ("Vertical");
		
		input = new Vector2( leftH, leftV );	
		
		
		desiredMove = transform.up * input.y + transform.right * input.x + transform.position + transform.forward * moveSpeed;
		
					
		transform.position = Vector3.Lerp (transform.position, 
		                                   desiredMove, 
		                                   Mathf.SmoothStep(0.0f, 1.0f, lerpTime));
	}
}