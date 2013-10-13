﻿using UnityEngine;
using System.Collections;

public class Enemy_AI : MonoBehaviour {
	
	public Transform target;
	public int moveSpeed = 10, rotSpeed = 30, maxDistance = 100, minDistance = 2;
	
	public float direction;
	
	public string idleBehavior = "BoundWander", traversionStyle = "walk";
	
	public Transform myTrans;
	public Vector3 initialTrans, newLookAtSpot;
	public bool isFollowing;
	
	public float curDisRand, curRotRand, rotRandUpdate, updateRand, curFloatRand;
	
	
	// Use this for initialization
	void Start () {
		
		myTrans = transform;
		initialTrans = myTrans.position;
		
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
		
		curDisRand = Random.Range(0,moveSpeed);
		rotRandUpdate = Random.Range(-360, 360);
		
		newLookAtSpot = new Vector3(Random.Range(myTrans.position.x - 5,myTrans.position.x + 5),Random.Range(myTrans.position.y - 5,myTrans.position.y + 5),Random.Range(myTrans.position.z - 5,myTrans.position.z + 5));
	}
	
	private void FreeWander(){
		
		updateRand = Random.Range(0,5);
		
		if(traversionStyle == "walk"){
			if(updateRand == 0){
				curDisRand = Random.Range(0,moveSpeed);
				rotRandUpdate = Random.Range(-360, 360);
			}
			
			if(curDisRand < moveSpeed/5) curDisRand = 0;
			
			if(rotRandUpdate > 200 || rotRandUpdate < -200){
				curRotRand = rotRandUpdate;	
			}	
			
			myTrans.position += myTrans.forward * curDisRand* Time.deltaTime;
			
			myTrans.rotation = Quaternion.Slerp(myTrans.rotation, Quaternion.Euler(myTrans.up * curRotRand), Time.deltaTime);
		}
		if(traversionStyle == "float"){
			
			if(updateRand == 0){
				curDisRand = Random.Range(0,moveSpeed);
				newLookAtSpot = new Vector3(
					(float)Random.Range(-Random.Range(myTrans.position.x - initialTrans.x, myTrans.position.x),Random.Range(myTrans.position.x - initialTrans.x,myTrans.position.x)),
					(float)Random.Range(-Random.Range(myTrans.position.y - initialTrans.y, myTrans.position.y),Random.Range(myTrans.position.y - initialTrans.y,myTrans.position.y)),
					(float)Random.Range(-Random.Range(myTrans.position.z - initialTrans.z, myTrans.position.z),Random.Range(myTrans.position.z - initialTrans.z,myTrans.position.z))
					);
			}			
			myTrans.rotation = Quaternion.Slerp(myTrans.rotation,Quaternion.LookRotation(newLookAtSpot),Time.deltaTime);
			
			myTrans.position += myTrans.forward * curDisRand* Time.deltaTime;
		}
	}
	
	private void BoundWander(){
		
		bool isOutOfBounds = false;
		
		updateRand = Random.Range(0,5);
		
		if(updateRand == 0){
			curDisRand = Random.Range(0,moveSpeed);
			rotRandUpdate = Random.Range(-360, 360);
		}
		
		if(curDisRand < moveSpeed/5) curDisRand = 0;
		
		if(rotRandUpdate > 200 || rotRandUpdate < -200){
			curRotRand = rotRandUpdate;	
		}
		if(Vector3.Distance(myTrans.position, initialTrans) > maxDistance){
			isOutOfBounds = true;
		}
		
		if(Vector3.Distance(myTrans.position, initialTrans) < maxDistance/2){
			isOutOfBounds = false;	
		}
		
		if(isOutOfBounds == true){
			
			myTrans.position += myTrans.forward * curDisRand* Time.deltaTime;
			
			myTrans.rotation = Quaternion.Slerp(myTrans.rotation, Quaternion.LookRotation(initialTrans - myTrans.position + new Vector3 (Random.Range(-Vector3.Distance(myTrans.position,initialTrans),Vector3.Distance(myTrans.position,initialTrans))*2,0,Random.Range(-Vector3.Distance(myTrans.position,initialTrans),Vector3.Distance(myTrans.position,initialTrans))*2)),Time.deltaTime);
			
		}else{	
			
			myTrans.position += myTrans.forward * curDisRand* Time.deltaTime;
		
			myTrans.rotation = Quaternion.Slerp(myTrans.rotation, Quaternion.Euler(myTrans.up * curRotRand), Time.deltaTime);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isFollowing == true){
			if (Vector3.Distance(target.position, myTrans.position) > minDistance){
				//Look at target
				myTrans.rotation = Quaternion.Slerp (myTrans.rotation, Quaternion.LookRotation(target.position - myTrans.position), rotSpeed * Time.deltaTime);
				
				//move towards target
				myTrans.position += myTrans.forward * moveSpeed * Time.deltaTime;
			}	
		}else{
			if(idleBehavior == "BoundWander"){
				BoundWander();
			}
			if(idleBehavior == "FreeWander"){
				FreeWander();
			}
		}
	}
}
