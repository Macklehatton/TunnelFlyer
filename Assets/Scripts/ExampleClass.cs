﻿using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour {

	void OnDrawGizmosSelected() {
		Vector3 p = camera.ScreenToWorldPoint(new Vector3(100, 100, camera.nearClipPlane));
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(p, 0.1F);
	}
}
