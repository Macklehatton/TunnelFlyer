using UnityEngine;
using System.Collections;

public class PIDCopyRotation : MonoBehaviour {
        
    public float targetAngle = 0; // the desired angle
    public float currentAngle; // current angle
    public float accel; // applied accel
    public float angularSpeed = 0; // current ang speed
    public float maxAccel = 180; // max accel in degrees/second2
    public float maxASpeed = 90; // max angular speed in degrees/second
    public float pGain = 20; // the proportional gain
    public float dGain = 10; // differential gain
    private float lastError;

    Rigidbody rigidbody;

    public Transform target;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start(){
        targetAngle = transform.eulerAngles.y; // get the current angle just for start
        currentAngle = targetAngle;
    }
 
    void FixedUpdate(){
        targetAngle = target.eulerAngles.y;

        float error = targetAngle - currentAngle; // generate the error signal
        float diff = (error - lastError)/ Time.deltaTime; // calculate differential error
        lastError = error;
        // calculate the acceleration:
        accel = error * pGain + diff * dGain;
        // limit it to the max acceleration
        accel = Mathf.Clamp(accel, -maxAccel, maxAccel);
        // apply accel to angular speed:
        angularSpeed += accel * Time.deltaTime; 
        // limit max angular speed
        angularSpeed = Mathf.Clamp(angularSpeed, -maxASpeed, maxASpeed);
        currentAngle += angularSpeed * Time.deltaTime; // apply the rotation to the angle...
        // and make the object follow the angle (must be modulo 360)
        rigidbody.rotation = Quaternion.Euler(0, currentAngle % 360, 0);
    }
}
