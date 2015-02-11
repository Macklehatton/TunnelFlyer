using UnityEngine;
using System.Collections;

public class PhysicsController : MonoBehaviour {

	public float leftH;
	public float leftV;
	
	public Vector2 input;
	public float speed;
	
	void FixedUpdate () {
		leftH = CrossPlatformInput.GetAxis ("Horizontal");
		leftV = CrossPlatformInput.GetAxis ("Vertical");
		
		input = new Vector2( leftH, leftV );

		Vector3 direction = new Vector3 (input.x, input.y, 0f);

		rigidbody.AddForce (direction * speed * Time.deltaTime);
	}
}
