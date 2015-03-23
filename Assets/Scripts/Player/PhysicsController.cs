using UnityEngine;
using System.Collections;

public class PhysicsController : MonoBehaviour {

	public float leftH;
	public float leftV;	
	public Vector2 input;

	public float maxStrafeSpeed;
	public float strafeThrust;
	public float maxForwardSpeed;
	public float forwardThrust;

	public float currentSpeed;
	Vector3 lastPosition;    

    private Rigidbody rigidbody;

    Vector3 relVelocity;
    public float dragScaleValue;    

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

	void FixedUpdate () {
		currentSpeed = (transform.position - lastPosition).magnitude;
		lastPosition = transform.position;		
		
		leftH = CrossPlatformInput.GetAxis ("Horizontal");
		leftV = CrossPlatformInput.GetAxis ("Vertical");
		
		input = new Vector2( leftH, leftV );

		Vector3 strafeDir = new Vector3 (input.x, input.y, 0f);

        if (Mathf.Abs(rigidbody.velocity.x) > maxStrafeSpeed || Mathf.Abs(rigidbody.velocity.y) > maxStrafeSpeed)
        {
			return;
		} else {
            rigidbody.AddRelativeForce(strafeDir * strafeThrust);			
		}

        if (rigidbody.velocity.z > maxForwardSpeed)
        {
            rigidbody.AddRelativeForce(forwardThrust * Vector3.forward);
		}else {
            rigidbody.AddRelativeForce(forwardThrust * Vector3.forward);
		}

        //side drag
        relVelocity = transform.InverseTransformDirection(rigidbody.velocity);
        rigidbody.AddRelativeForce(-relVelocity.x * dragScaleValue * Vector3.right);
	}
}
