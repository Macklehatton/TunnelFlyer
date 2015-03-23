using UnityEngine;
using System.Collections;

public class CopyRotation : MonoBehaviour {

	public Transform target;
	public float speed;

    Rigidbody rigidbody;
    Rigidbody targetBody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        targetBody = target.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation,
                                             target.rotation,
                                             Mathf.SmoothStep(0.0f, 1.0f, speed));        
    }
}
