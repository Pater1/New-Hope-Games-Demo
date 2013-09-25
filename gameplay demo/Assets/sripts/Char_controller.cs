using UnityEngine;
using System.Collections;

public class Char_controller : MonoBehaviour {
	
	public float maxSpeedForward = 20, maxSpeedSideways = 10, maxSpeedBackwards = 5;
	private Transform myTransform;
	
	// Use this for initialization
	void Start () {
		myTransform = transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKeyDown(KeyCode.W)){
			
		}
		if(Input.GetKeyDown(KeyCode.A)){
			
		}
		if(Input.GetKeyDown(KeyCode.S)){
			
		}
		if(Input.GetKeyDown(KeyCode.D)){
			
		}
	}
}
