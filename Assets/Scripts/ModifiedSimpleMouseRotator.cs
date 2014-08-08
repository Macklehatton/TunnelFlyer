﻿using UnityEngine;

public class ModifiedSimpleMouseRotator : MonoBehaviour {
	
	// A mouselook behaviour with constraints which operate relative to
	// this gameobject's initial rotation.
	
	// Only rotates around local X and Y.
	
	// Works in local coordinates, so if this object is parented
	// to another moving gameobject, its local constraints will
	// operate correctly
	// (Think: looking out the side window of a car, or a gun turret
	// on a moving spaceship with a limited angular range)
	
	// to have no constraints on an axis, set the rotationRange to 360 or greater.

	public Vector2 rotationRange = new Vector3(70,70); 
	public float rotationSpeed = 10;
	public float dampingTime = 0.2f;
	public bool autoZeroVerticalOnMobile = true;
	public bool autoZeroHorizontalOnMobile = false;
	public bool relative = true;
	Vector3 targetAngles;
	Vector3 followAngles;
	Vector3 followVelocity;
	Quaternion originalRotation;

	public bool leveling;
	public float levelingSpeed;
	public float levelingTimer;
	public float timeSinceInput;
	public Quaternion lastRotation;
	public Vector3 lastHeading;
	public float angle;


	// Use this for initialization
	void Start () {
		originalRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {

		angle = Vector3.Angle (transform.forward, lastHeading);

		if (angle <= 0.0f) {
			timeSinceInput += Time.deltaTime;
		}

		lastHeading = transform.forward;

		if (timeSinceInput >= levelingTimer) {
			leveling = true;
		}

		if (leveling) {
			if (Quaternion.Angle(transform.localRotation, Quaternion.Euler(new Vector3(0f, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z))) <= 0f) {
				leveling = false;
				timeSinceInput = 0f;
			}
		}

		// we make initial calculations from the original local rotation
		if (!leveling) {
			transform.localRotation = originalRotation;
		}



		// read input from mouse or mobile controls
		float inputH = 0;
		float inputV = 0;
		if (relative)
		{
			#if CROSS_PLATFORM_INPUT
			inputH = CrossPlatformInput.GetAxis("Mouse X");
			inputV = CrossPlatformInput.GetAxis("Mouse Y");
			#else
			inputH = Input.GetAxis("Mouse X");
			inputV = Input.GetAxis("Mouse Y");
			#endif
			// wrap values to avoid springing quickly the wrong way from positive to negative
			if (targetAngles.y > 180) { targetAngles.y -= 360; followAngles.y -= 360; }
			if (targetAngles.x > 180) { targetAngles.x -= 360; followAngles.x-= 360; }
			if (targetAngles.y < -180) { targetAngles.y += 360; followAngles.y += 360; }
			if (targetAngles.x < -180) { targetAngles.x += 360; followAngles.x += 360; }

			#if MOBILE_INPUT
			// on mobile, sometimes we want input mapped directly to tilt value,
			// so it springs back automatically when the look input is released.
			if (autoZeroHorizontalOnMobile) {
				targetAngles.y = Mathf.Lerp (-rotationRange.y * 0.5f, rotationRange.y * 0.5f, inputH * .5f + .5f);
			} else {
				if (leveling) {
					targetAngles.y = 0.0f;
				} else {
					targetAngles.y += inputH * rotationSpeed;
				}
			}
			if (autoZeroVerticalOnMobile) {
				targetAngles.x = Mathf.Lerp (-rotationRange.x * 0.5f, rotationRange.x * 0.5f, inputV * .5f + .5f);
			} else {
				targetAngles.x += inputV * rotationSpeed;
			}			
			#else
			// with mouse input, we have direct control with no springback required.
			targetAngles.y += inputH * rotationSpeed;
			targetAngles.x += inputV * rotationSpeed;
			#endif

			// clamp values to allowed range
			targetAngles.y = Mathf.Clamp ( targetAngles.y, -rotationRange.y * 0.5f, rotationRange.y * 0.5f );
			targetAngles.x = Mathf.Clamp ( targetAngles.x, -rotationRange.x * 0.5f, rotationRange.x * 0.5f );

		} else {

			inputH = Input.mousePosition.x;
			inputV = Input.mousePosition.y;

			targetAngles.y = Mathf.Lerp ( -rotationRange.y * 0.5f, rotationRange.y * 0.5f, inputH/Screen.width );
			targetAngles.x = Mathf.Lerp ( -rotationRange.x * 0.5f, rotationRange.x * 0.5f, inputV/Screen.height );

		}

		followAngles = Vector3.SmoothDamp( followAngles, targetAngles, ref followVelocity, dampingTime );
		

<<<<<<< HEAD
		xAdjustment = Mathf.Pow ((1 - xDiff), 3 ) * valleyDepth * rotationSpeed;
		yAdjustment = Mathf.Pow ((1 - yDiff), 3 ) * valleyDepth * rotationSpeed;		
	}
	
	
	void LevelingOn() {
		if (!leveling) {
			if (startAngles == Vector3.zero) {
				startAngles = targetAngles;
				startRotation = transform.localRotation.eulerAngles;
				startTime = Time.time;
				Debug.Log ("Set start angles");
			}
			leveling = true;
		}
	}
=======
		if (leveling) {			
			transform.localRotation = Quaternion.RotateTowards (transform.localRotation, Quaternion.identity, Time.deltaTime * levelingSpeed);
>>>>>>> parent of 4ab13f2... Rotation valleys and horizontal snapping

		}

		transform.localRotation = originalRotation * Quaternion.Euler( -followAngles.x, followAngles.y, 0 );

	}
}
