using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	public float lifeTimer;
			
	void Update () {
		lifeTimer -= Time.deltaTime;
		if (lifeTimer <= 0) {
			GameObject.Destroy(gameObject);
		}	
	}
}
