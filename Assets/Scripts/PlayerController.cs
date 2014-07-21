using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour 
{
	public float leftH;
	public float leftV;

	public Vector3 strafeMove;
	public Vector3 desiredMove;

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
	}
}