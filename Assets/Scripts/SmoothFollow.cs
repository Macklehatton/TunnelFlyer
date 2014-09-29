using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {

	public Transform player;
	public float inLimit;
	public float outLimit;
	public float damping;
	public float speed;
	
		
	void FixedUpdate () {
				
		if (Vector3.Distance(transform.position, player.position) > inLimit) {
			if (Vector3.Distance(transform.position, player.position) > outLimit){
				transform.position = Lerp(transform.position, player.position, speed);
			}
		}		
	}
	
	public static Vector3 Lerp(Vector3 start, Vector3 finish, float percentage)
	{
		//Make sure percentage is in the range [0.0, 1.0]
		percentage = Mathf.Clamp01(percentage);
		
		//(finish-start) is the Vector3 drawn between 'start' and 'finish'
		Vector3 startToFinish = finish - start;
		
		//Multiply it by percentage and set its origin to 'start'
		return start + startToFinish * percentage;
	}
}