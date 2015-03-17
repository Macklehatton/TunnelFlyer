using UnityEngine;
using System.Collections;

public class ContactSparks : MonoBehaviour {

	public Transform scrapeSparks;
	public Transform hitSparks;

	Vector3 hitPos;

	Vector3 scrapePos;
	Quaternion scrapeRot;
	Transform scrapeInstance;


	void OnCollisionEnter(Collision collisions) {
		ContactPoint contact = collisions.contacts [0];
		Quaternion rot = Quaternion.FromToRotation(transform.forward, contact.normal);
		Vector3 hitPos = contact.point;
		Instantiate(hitSparks, hitPos, rot);		
	}

	void OnCollisionStay(Collision collisions) {
		ContactPoint contact = collisions.contacts [0];
		scrapePos = contact.point;
		scrapeRot = Quaternion.FromToRotation (transform.forward * -1, contact.point);

		if (scrapeInstance == null) {
			scrapeInstance = Instantiate (scrapeSparks, scrapePos, scrapeRot) as Transform;			
		} else {
			scrapeInstance.position = scrapePos;
			scrapeInstance.rotation = scrapeRot;
		}
	}

	void OnCollisionExit () {
		if (scrapeInstance != null) {
			GameObject.Destroy (scrapeInstance.gameObject);
		}
	}
}
