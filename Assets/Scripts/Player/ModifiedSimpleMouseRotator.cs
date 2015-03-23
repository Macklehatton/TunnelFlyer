using UnityEngine;
using System.Collections;

public class ModifiedSimpleMouseRotator : MonoBehaviour {
	
	// Modification of the sample assets mouse rotator. Has valleys in rotation speed that are like a soft version of angle snaps
	// Automatically levels out and snaps to a different set of angles when there is no input.
	
	public Vector2 rotationRange = new Vector3(70,70); 
	public float rotationSpeed = 1.5f;
	public float dampingTime = 0.0f;
	public bool autoZeroVerticalOnMobile = true;
	public bool autoZeroHorizontalOnMobile = false;
	public Vector3 targetAngles;
	public Vector3 followAngles;
	public Vector3 followVelocity;
	Quaternion originalRotation;
	
	public bool leveling;
	public float levelingTime;
	public float levelingTimer;
	public float timeSinceInput;
	
	public bool snapping;
	public Vector3 startRotation;
	public Vector3 startAngles;
	public float startTime;
	public Quaternion zeroX;	
	public float valleySnap;
	public float settleSnap;
	public float xDiff;
	public float yDiff;
	public float xAdjustment;
	public float yAdjustment;
	public float valleyDepth;
	
	void Start () {
		originalRotation = transform.rotation;
		targetAngles = Vector3.zero;
		followAngles = Vector3.zero;
		originalRotation = transform.rotation;
	}
	
	void FixedUpdate () {		
		float inputH = 0;
		float inputV = 0;      

		inputH = CrossPlatformInput.GetAxis("Mouse X");
		inputV = CrossPlatformInput.GetAxis("Mouse Y");
		
		if (targetAngles.y > 180) { targetAngles.y -= 360; followAngles.y -= 360; }
		if (targetAngles.x > 180) { targetAngles.x -= 360; followAngles.x-= 360; }
		if (targetAngles.y < -180) { targetAngles.y += 360; followAngles.y += 360; }
		if (targetAngles.x < -180) { targetAngles.x += 360; followAngles.x += 360; }		
		
		targetAngles.y += inputH * (rotationSpeed - yAdjustment);
		targetAngles.x += inputV * (rotationSpeed - xAdjustment);								
		
		targetAngles.y = Mathf.Clamp ( targetAngles.y, -rotationRange.y * 0.5f, rotationRange.y * 0.5f );
		targetAngles.x = Mathf.Clamp ( targetAngles.x, -rotationRange.x * 0.5f, rotationRange.x * 0.5f );

        if (Mathf.Abs (inputH) <= 0.0f || Mathf.Abs(inputV) <= 0.0f)
        {
            timeSinceInput += Time.deltaTime;
        }
        else
        {
            timeSinceInput = 0.0f;
        }

        if (timeSinceInput >= levelingTimer)
        {
            LevelingOn();
        }

        if (leveling)
        {            
            if (Mathf.Abs(inputH) > 0f || Mathf.Abs(inputH) > 0f)
            {
                iTween.StopByName("RotateTo");
                LevelingOff();
            }	
        }

        if (!leveling)
        {
            transform.rotation = originalRotation;
        }

		
		
		if (leveling) {
			zeroX = transform.rotation;
			zeroX.eulerAngles = new Vector3 (0f, zeroX.eulerAngles.y, zeroX.eulerAngles.z);			
			float ySettle = Mathf.Round ( transform.rotation.eulerAngles.y / settleSnap) * settleSnap;
			
			iTween.RotateTo(gameObject, iTween.Hash("easetype", "easeInQuad", "time",levelingTime, "x",0f,"y", ySettle, "name", "RotateTo"));			
			
		} else {
			followAngles = Vector3.SmoothDamp( followAngles, targetAngles, ref followVelocity, dampingTime );			
			
			transform.rotation = originalRotation * Quaternion.Euler( -followAngles.x, followAngles.y, 0 );
		}
		
		xDiff = Mathf.Abs((Mathf.Round ( transform.rotation.eulerAngles.x / valleySnap) * valleySnap) - transform.rotation.eulerAngles.x)/valleySnap;
		yDiff = Mathf.Abs((Mathf.Round ( transform.rotation.eulerAngles.y / valleySnap) * valleySnap) - transform.rotation.eulerAngles.y)/valleySnap;		
		
		xAdjustment = Mathf.Pow ((1 - xDiff), 3 ) * valleyDepth * rotationSpeed;
		yAdjustment = Mathf.Pow ((1 - yDiff), 3 ) * valleyDepth * rotationSpeed;		
	}
	
	
	void LevelingOn() {
		if (!leveling) {
			if (startAngles == Vector3.zero) {
				startAngles = targetAngles;
				startRotation = transform.rotation.eulerAngles;
				startTime = Time.time;
				Debug.Log ("Set start angles");
			}
			leveling = true;
		}
	}
	
	void LevelingOff() {
		if (leveling) {	
			Debug.Log ("Leveling off");
			startAngles = Vector3.zero;
			targetAngles = transform.rotation.eulerAngles;
			timeSinceInput = 0.0f;
			leveling = false;
		}
	}
}