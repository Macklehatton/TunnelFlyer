using UnityEngine;
using System.Collections;

public class CopyRotation : MonoBehaviour {

	public GameObject target;
	public float speed;
	public float cancelVelocity;

	void Update () {
		//This implmentation of lerp is incorrect, fix it if lerping is the solution										
		transform.rotation = Quaternion.Lerp(transform.rotation, 
		                                     target.transform.rotation,
		                                     Time.deltaTime * speed); 
		                                     
		rigidbody.angularVelocity *= cancelVelocity;
	}
}