#pragma strict

 public var target: GameObject;

 var targetPos = Vector3.zero; // the desired position
 var maxForce: float = 100; // the max force available
 var pGain: float = 20; // the proportional gain
 var iGain: float = 0.5; // the integral gain
 var dGain: float = 0.5; // differential gain
 private var integrator = Vector3.zero; // error accumulator
 private var lastError = Vector2.zero; 
 var curPos = Vector3.zero; // actual Pos
 var force = Vector3.zero; // current force
 
 function Start(){
   targetPos = transform.position;
 }
 
 function FixedUpdate(){
 	
 	
 
	targetPos = target.transform.position;
 	targetPos.y = targetPos.y - 2.5f;
 	targetPos.z = targetPos.z - 10;
 	
 	
	curPos = transform.position;
	var error = targetPos - curPos; // generate the error signal
	integrator += error * Time.deltaTime; // integrate error
	var diff = (error - lastError)/ Time.deltaTime; // differentiate error
	lastError = error;
	// calculate the force summing the 3 errors with respective gains:
	force = error * pGain + integrator * iGain + diff * dGain;
	// clamp the force to the max value available
	force = Vector3.ClampMagnitude(force, maxForce);
	// apply the force to accelerate the rigidbody:
	rigidbody.AddForce(force);
 }