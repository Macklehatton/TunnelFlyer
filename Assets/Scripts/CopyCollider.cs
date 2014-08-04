using UnityEngine;
using System.Collections;


[RequireComponent (typeof (BoxCollider))]
public class CopyCollider : MonoBehaviour {
	
	public GameObject ship;
	BoxCollider collider;
	
	void Start () {
		collider = gameObject.collider as BoxCollider;
	}
		
	void Update () {
		collider.center = ship.transform.position;
	}
}
