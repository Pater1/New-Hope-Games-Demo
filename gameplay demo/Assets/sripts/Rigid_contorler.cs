using UnityEngine;
using System.Collections;

public class Rigid_contorler : MonoBehaviour {
	
	public float maxSpeedForward = 20, maxSpeedSideways = 10, maxSpeedBack = 15;
	public float accelerationFactor = .5f, maxSprintMultiplier = 3;
	public float maxJumpDuration = 100, curJumpDuration, jumpSpeed = 10;	
	
	public float curSpeedForward, curSpeedSideways, curSpeedBackwards, curSprintMultiplier;	
	public bool isMovingForward, isMovingRight, isMovingLeft, isMovingBackwards, isSprinting, isAirBorn, isJumping;
	private Transform myTrans;
	
	// Use this for initialization
	void Start () {
		myTrans = transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CheckButtons();
		
		FindMovement();

		Move();
	}
	
	private void Move(){
		
		myTrans.position += myTrans.forward * curSprintMultiplier * curSpeedForward * Time.deltaTime;
		
		myTrans.position -= myTrans.forward * curSprintMultiplier * curSpeedBackwards * Time.deltaTime;
		
		myTrans.position += myTrans.right * curSprintMultiplier * curSpeedSideways * Time.deltaTime;
		
		Jump();
		
		if(isJumping == true){
			myTrans.position += myTrans.up * curJumpDuration * jumpSpeed * Time.deltaTime;
		}
	}
	
	private void FindMovement(){
		if(isMovingForward == true){
			curSpeedForward += accelerationFactor;
			if(curSpeedForward > maxSpeedForward) curSpeedForward = maxSpeedForward;
		}else{
			curSpeedForward -= accelerationFactor;
			if(curSpeedForward < 0) curSpeedForward = 0;
		}
		
		if(isMovingBackwards == true){
			curSpeedBackwards += accelerationFactor;
			if(curSpeedBackwards > maxSpeedBack) curSpeedBackwards = maxSpeedBack;
		}else{
			curSpeedBackwards -= accelerationFactor;
			if(curSpeedBackwards < 0) curSpeedBackwards = 0;
		}
		
		if(isMovingRight == true){
			curSpeedSideways += accelerationFactor;
			if(curSpeedSideways > maxSpeedSideways) curSpeedSideways = maxSpeedSideways;
		}else if(isMovingLeft == true){
			curSpeedSideways -= accelerationFactor;
			if(curSpeedSideways < -maxSpeedSideways) curSpeedSideways = -maxSpeedSideways;
		}else{
			curSpeedSideways += accelerationFactor;
			if(curSpeedSideways > 0) curSpeedSideways = 0;
		}
		
		if(isSprinting == true){
			curSprintMultiplier += Time.deltaTime;
			
			if(curSprintMultiplier > maxSprintMultiplier) curSprintMultiplier = maxSprintMultiplier;
		}else{
			curSprintMultiplier -= Time.deltaTime;
			
			if(curSprintMultiplier < 1) curSprintMultiplier = 1;
		}
		
		if(isJumping == true){
				
		}
	}
	
	private void CheckButtons(){
		if(Input.GetKeyDown(KeyCode.W)){
			isMovingForward = true;	
		}
		if(Input.GetKeyUp(KeyCode.W)){
			isMovingForward = false;	
		}
		
		if(Input.GetKeyDown(KeyCode.S)){
			isMovingBackwards = true;	
		}
		if(Input.GetKeyUp(KeyCode.S)){
			isMovingBackwards = false;	
		}
		
		if(Input.GetKeyDown(KeyCode.A)){
			isMovingLeft = true;	
		}
		if(Input.GetKeyUp(KeyCode.A)){
			isMovingLeft = false;	
		}
		
		if(Input.GetKeyDown(KeyCode.D)){
			isMovingRight = true;	
		}
		if(Input.GetKeyUp(KeyCode.D)){
			isMovingRight = false;	
		}
		
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			isSprinting = true;	
		}
		if(Input.GetKeyUp(KeyCode.LeftShift)){
			isSprinting = false;	
		}
	}
	
	private void Jump(){
		if(Input.GetKeyDown(KeyCode.Space)){
			if(isAirBorn == false){
				isJumping = true;
				curJumpDuration = 0;
			}
			if(curJumpDuration < maxJumpDuration && isJumping == true){
				curJumpDuration ++;
			}
		}if(Input.GetKeyUp(KeyCode.Space)){
			isJumping = false;
		}
	}
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Ground"){
			isAirBorn = false;
		}else{
			isAirBorn = true;	
		}
	}
}
