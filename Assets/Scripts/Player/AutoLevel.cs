using UnityEngine;
using System.Collections;

public class AutoLevel : MonoBehaviour {

	public float maxXRot;
	public float currentX;
	public float levelSpeed;

	void Update () {
		currentX = Mathf.Abs (transform.rotation.eulerAngles.x);

		if (currentX > maxXRot) {
			transform.rotation = Quaternion.identity;
		}
	}
}