using UnityEngine;
using System.Collections;

public class SwingDoor : MonoBehaviour {
	
	public int force = 10;
	
	void OnCollisionEnter (Collision col) {
		if(col.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.F)){
			rigidbody.AddForce(Vector3.forward * force,ForceMode.Impulse);
		}
	}
	
	void OnCollisionStay (Collision col) {
		if(col.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.F)){
			rigidbody.AddForce(Vector3.forward * force,ForceMode.Impulse);
		}
	}
}
