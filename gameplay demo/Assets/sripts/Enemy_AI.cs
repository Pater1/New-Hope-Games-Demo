using UnityEngine;
using System.Collections;

public class Enemy_AI : MonoBehaviour {
	
	public Transform target;
	public int moveSpeed, rotSpeed, maxDistance, minDistance, cutOffDistance;
	
	private Transform myTrans;
	private bool isFollowing;
	
	void Awake(){
		myTrans = transform;
	}
	
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		
		target = go.transform;
		cutOffDistance = 500;
		maxDistance = 200;
		minDistance = 2;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(target.position,myTrans.position,Color.red);
		
		if (Vector3.Distance(target.position, myTrans.position) < maxDistance){
			isFollowing = true;
		}
		if (Vector3.Distance(target.position, myTrans.position) > cutOffDistance){
			isFollowing = false;
		}
		
		if (isFollowing == true){
			if (Vector3.Distance(target.position, myTrans.position) > minDistance){
				//Look at target
				myTrans.rotation = Quaternion.Slerp (myTrans.rotation, Quaternion.LookRotation(target.position - myTrans.position), rotSpeed * Time.deltaTime);
				
				//move towards target
				myTrans.position += myTrans.forward * moveSpeed * Time.deltaTime;
			}	
		}
	}
}
