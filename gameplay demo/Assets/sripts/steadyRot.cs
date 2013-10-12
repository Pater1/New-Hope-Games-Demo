using UnityEngine;
using System.Collections;

public class steadyRot : MonoBehaviour {
		
	// Update is called once per frame
	void Update () {
		
		GameObject go = GameObject.FindGameObjectWithTag("Player");
				
		transform.LookAt(go.transform);
	}
}
