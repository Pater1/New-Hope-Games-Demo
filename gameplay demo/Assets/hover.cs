using UnityEngine;
using System.Collections;


public class hover : MonoBehaviour {
	
	public float force = 10f;
	
	Random varX, varZ;
	
	int localX, localZ;
	
	bool moveEast = false, moveNorth = false;
	
	void OnTriggerStay (Collider other){
		
		other.rigidbody.AddForce(Vector3.up * force, ForceMode.Acceleration);
			
	}
	
}
