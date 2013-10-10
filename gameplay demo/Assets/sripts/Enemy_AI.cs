using UnityEngine;
using System.Collections;

public class Enemy_AI : MonoBehaviour {
	
	public Transform target;
	public int moveSpeed = 10, rotSpeed = 30, maxDistance = 100, minDistance = 2;
	
	public Transform myTrans;
	public Vector3 initialTrans;
	public bool isFollowing, isOutOfBounds;
	
	public float curDisRand, curRotRand, rotRandUpdate, updateRand;
	
	void Awake(){
		myTrans = transform;
		initialTrans = myTrans.position;
	}
	
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		
		target = go.transform;
		
		curDisRand = Random.Range(0,moveSpeed);
		rotRandUpdate = Random.Range(-360, 360);
		
	}
	
	private void Wander(){
		
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
		Debug.DrawLine(target.position,myTrans.position,Color.red);
		
		if (Vector3.Distance(target.position, myTrans.position) < maxDistance){
			isFollowing = true;
		}
		if (Vector3.Distance(target.position, myTrans.position) > maxDistance){
			isFollowing = false;
		}
		
		if (isFollowing == true){
			if (Vector3.Distance(target.position, myTrans.position) > minDistance){
				//Look at target
				myTrans.rotation = Quaternion.Slerp (myTrans.rotation, Quaternion.LookRotation(target.position - myTrans.position), rotSpeed * Time.deltaTime);
				
				//move towards target
				myTrans.position += myTrans.forward * moveSpeed * Time.deltaTime;
			}	
		}else{
			Wander();	
		}
	}
}
