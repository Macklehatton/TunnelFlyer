using UnityEngine;
using System.Collections;

public class CopyRotation : MonoBehaviour {

	public GameObject target;
	public float speed;

	void Update () {
		transform.rotation = Quaternion.Lerp(transform.rotation, 
		                                     target.transform.rotation,
		                                     Mathf.SmoothStep(0.0f, 1.0f, speed));                                
	
	}
}
