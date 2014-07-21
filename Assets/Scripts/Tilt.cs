using UnityEngine;
using System.Collections;

public class Tilt : MonoBehaviour {

	public Quaternion desiredTurn;

	public float rTilt;
	public float pTilt;

	void Update() {
		rTilt = CrossPlatformInput.GetAxis ("Roll") * -90;
		pTilt = CrossPlatformInput.GetAxis ("Pitch") * -90;

		desiredTurn = Quaternion.Euler (pTilt, 0f, rTilt);

		transform.rotation = desiredTurn;


	}
}

