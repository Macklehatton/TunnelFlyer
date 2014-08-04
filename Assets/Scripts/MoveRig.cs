using UnityEngine;
using System.Collections;

public class MoveRig : MonoBehaviour {

	public float speed;
	public float strafeSpeed;
	public float strafeThreshold;	

	Vector3 forwardMovement;

	public float leftH;
	public float leftV;

	// Update is called once per frame
	void Update () {
		leftH = CrossPlatformInput.GetAxis ("Horizontal");
		leftV = CrossPlatformInput.GetAxis ("Vertical");

		if (leftH > strafeThreshold) {
			transform.Translate (transform.right * strafeSpeed * (leftH - strafeThreshold) * Time.deltaTime, Space.World);
				}
		if (leftH < -strafeThreshold) {
			transform.Translate (transform.right * strafeSpeed * (leftH + strafeThreshold) * Time.deltaTime, Space.World);
		}
		if (leftV > strafeThreshold) {
			transform.Translate (transform.up * strafeSpeed * (leftV - strafeThreshold) * Time.deltaTime, Space.World);
		}
		if (leftV < -strafeThreshold) {
			transform.Translate (transform.up * strafeSpeed * (leftV + strafeThreshold) * Time.deltaTime, Space.World);
		}

		transform.Translate (transform.forward * speed * Time.deltaTime, Space.World);


	}
}
