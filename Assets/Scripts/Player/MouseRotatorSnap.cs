using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	public class MouseRotatorSnap : MonoBehaviour
	{
		// A mouselook behaviour with constraints which operate relative to
		// this gameobject's initial rotation.
		// Only rotates around local X and Y.
		// Works in local coordinates, so if this object is parented
		// to another moving gameobject, its local constraints will
		// operate correctly
		// (Think: looking out the side window of a car, or a gun turret
		// on a moving spaceship with a limited angular range)
		// to have no constraints on an axis, set the rotationRange to 360 or greater.
		public Vector2 rotationRange = new Vector3(70, 70);
		public float rotationSpeed = 10;
		public float dampingTime = 0.2f;
		public bool autoZeroVerticalOnMobile = true;
		public bool autoZeroHorizontalOnMobile = false;
		public bool relative = true;


		private Vector3 m_TargetAngles;
		private Vector3 m_FollowAngles;
		private Vector3 m_FollowVelocity;
		private Quaternion m_OriginalRotation;


		public float levelingTime;
		Vector3 levelingVelocity;
		public float snapAngle;
		public float inputTimeLimit;
		bool leveling;
		float timeSinceInput;
		
		private void Start()
		{
			m_OriginalRotation = transform.localRotation;
		}
		
		
		private void LateUpdate()
		{
			// read input from mouse or mobile controls
			float inputH;
			float inputV;

			inputH = CrossPlatformInput.GetAxis("Mouse X");
			inputV = CrossPlatformInput.GetAxis("Mouse Y");

			if (inputH > 0f || inputV > 0f) {
				leveling = false;
			} else {
				timeSinceInput += Time.deltaTime;
				if (timeSinceInput >= inputTimeLimit) {
					leveling = true;
				}
			}

			if (!leveling){
				// we make initial calculations from the original local rotation
				transform.localRotation = m_OriginalRotation;
				
				// wrap values to avoid springing quickly the wrong way from positive to negative
				if (m_TargetAngles.y > 180)
				{
					m_TargetAngles.y -= 360;
					m_FollowAngles.y -= 360;
				}
				if (m_TargetAngles.x > 180)
				{
					m_TargetAngles.x -= 360;
					m_FollowAngles.x -= 360;
				}
				if (m_TargetAngles.y < -180)
				{
					m_TargetAngles.y += 360;
					m_FollowAngles.y += 360;
				}
				if (m_TargetAngles.x < -180)
				{
					m_TargetAngles.x += 360;
					m_FollowAngles.x += 360;
				}
				
				
				// on mobile, sometimes we want input mapped directly to tilt value,
				// so it springs back automatically when the look input is released.
				if (autoZeroHorizontalOnMobile) {
					m_TargetAngles.y = Mathf.Lerp (-rotationRange.y * 0.5f, rotationRange.y * 0.5f, inputH * .5f + .5f);
				} else {
					m_TargetAngles.y += inputH * rotationSpeed;
				}
				if (autoZeroVerticalOnMobile) {
					m_TargetAngles.x = Mathf.Lerp (-rotationRange.x * 0.5f, rotationRange.x * 0.5f, inputV * .5f + .5f);
				} else {
					m_TargetAngles.x += inputV * rotationSpeed;
				}
								
				// clamp values to allowed range
				m_TargetAngles.y = Mathf.Clamp(m_TargetAngles.y, -rotationRange.y*0.5f, rotationRange.y*0.5f);
				m_TargetAngles.x = Mathf.Clamp(m_TargetAngles.x, -rotationRange.x*0.5f, rotationRange.x*0.5f);			
								
				// smoothly interpolate current values to target angles
				m_FollowAngles = Vector3.SmoothDamp(m_FollowAngles, m_TargetAngles, ref m_FollowVelocity, dampingTime);
				
				// update the actual gameobject's rotation
				transform.localRotation = m_OriginalRotation*Quaternion.Euler(-m_FollowAngles.x, m_FollowAngles.y, 0);

			} else {
				//float ySettle = Mathf.Round ( transform.localRotation.eulerAngles.y / snapAngle) * snapAngle;

				m_TargetAngles.y = Mathf.Round ( transform.localRotation.eulerAngles.y / snapAngle) * snapAngle;
				m_TargetAngles.x = Mathf.Round ( transform.localRotation.eulerAngles.x / snapAngle) * snapAngle;			
				
				m_FollowAngles = Vector3.SmoothDamp(m_FollowAngles, m_TargetAngles, ref levelingVelocity, levelingTime);
				
				// update the actual gameobject's rotation
				transform.localRotation = m_OriginalRotation*Quaternion.Euler(m_FollowAngles.x, m_FollowAngles.y, 0);
			}
		}
	}
}
