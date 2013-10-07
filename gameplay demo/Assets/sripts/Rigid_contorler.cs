using UnityEngine;
using System.Collections;

public class Rigid_contorler : MonoBehaviour {
	
	public float maxSpeedForward = 20, maxSpeedBack = 15, maxJumpDuration = 5, vertSwimSpeed = 5, horSwimSpeed = 10;
	public float accelerationFactor = .5f, maxSprintMultiplier = 3, maxSprintTime = 5, jumpSpeed = 7;
	private float curJumpDuration, curSprintTime, curSpeedForward, curSpeedBackwards, curSprintMultiplier;	
	public bool isMovingForward, isTurningRight, isTurningLeft, isMovingBackwards, isSprinting, isAirBorn, isJumping, isSwimming;
	private Transform myTrans;
	private Quaternion myRot;
	
	
	//no clue how it works, but these are needed to rotate
	private Quaternion deltaRotation;
	public Vector3 eulerAngleVelocity = new Vector3 (0, 100, 0);
	
	private RaycastHit hit;
	private Ray ray;
	private Vector3 slope;
	
	// Use this for initialization
	void Start () {
		myTrans = transform;
		
		myRot = myTrans.rotation;
		
		ray = new Ray (transform.position, Vector3.down);
				
	}
	
	private void Move(){
		
		myTrans.position += myTrans.forward * curSprintMultiplier * curSpeedForward * Time.deltaTime;
		
		myTrans.position -= myTrans.forward * curSprintMultiplier * curSpeedBackwards * Time.deltaTime;
				
		rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
		
		Jump();
		
		Swim();
		
		if(isSwimming == true){
			if(isJumping == true){
				myTrans.position += myTrans.up * vertSwimSpeed * curSprintMultiplier * Time.deltaTime;
			}else{
				myTrans.position -= myTrans.up * vertSwimSpeed * curSprintMultiplier * Time.deltaTime;
			}
		}else{
			myTrans.position += myTrans.up * curJumpDuration * jumpSpeed * Time.deltaTime;
		}
		
		if (Physics.Raycast(ray, out hit ,10)){
			if(hit.collider.tag == "Ground"){
				slope = hit.normal;
			}
		}
	}
	
	private void FindMovement(){
		if(isMovingForward == true){
			curSpeedForward += accelerationFactor;
			if(isSwimming == true){
				if(curSpeedForward > horSwimSpeed) curSpeedForward = horSwimSpeed;
			}else{
				if(curSpeedForward > maxSpeedForward) curSpeedForward = maxSpeedForward;
				//animation.Play("walk");
			}
		}else{
			curSpeedForward -= accelerationFactor;
			if(curSpeedForward < 0) curSpeedForward = 0;
		}
		
		if(isMovingBackwards == true){
			curSpeedBackwards += accelerationFactor;
			if(isSwimming == true){
				if(curSpeedBackwards > horSwimSpeed) curSpeedBackwards = horSwimSpeed;
			}else{
				if(curSpeedBackwards > maxSpeedBack) curSpeedBackwards = maxSpeedBack;
			}
		}else{
			curSpeedBackwards -= accelerationFactor;
			if(curSpeedBackwards < 0) curSpeedBackwards = 0;
		}
		
		if (isTurningLeft == true){		
			deltaRotation = Quaternion.Euler(-eulerAngleVelocity * Time.deltaTime);	
		}else if (isTurningRight == true){		
			deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
		}else{
			deltaRotation = Quaternion.identity;	
		}
		
		if(isSprinting == true){
			curSprintMultiplier += Time.deltaTime;
			
			if(curSprintMultiplier > maxSprintMultiplier) curSprintMultiplier = maxSprintMultiplier;
			
			curSprintTime += Time.deltaTime;
			
			if(curSprintTime > maxSprintTime) isSprinting = false;
		}else{
			curSprintMultiplier -= Time.deltaTime;
			
			if(curSprintMultiplier < 1) curSprintMultiplier = 1;
			
			curSprintTime -= Time.deltaTime;
			
			if(curSprintTime < 0)curSprintTime = 0;
		}
		
		if(isJumping == true){
			curJumpDuration += Time.deltaTime;
			if(curJumpDuration > maxJumpDuration) curJumpDuration = maxJumpDuration;
		}else{
			curJumpDuration -= Time.deltaTime;	
			if(curJumpDuration < 0) curJumpDuration = 0;
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
			isTurningLeft = true;	
		}
		if(Input.GetKeyUp(KeyCode.A)){
			isTurningLeft = false;	
		}
		
		if(Input.GetKeyDown(KeyCode.D)){
			isTurningRight = true;	
		}
		if(Input.GetKeyUp(KeyCode.D)){
			isTurningRight = false;	
		}
		
		if(Input.GetKeyDown(KeyCode.LeftShift) && curSprintTime == 0){
			isSprinting = true;	
		}
		if(Input.GetKeyUp(KeyCode.LeftShift)){
			isSprinting = false;	
		}
	}
	
	private void Jump(){
		if(Input.GetKeyDown(KeyCode.Space)){
			if(isAirBorn == false || isSwimming == true){
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
	
	private void Swim(){
		if(isSwimming == true){
			rigidbody.useGravity = false;
		}else{
			rigidbody.useGravity = true;
		}
	}
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Ground"){
			isAirBorn = false;
		}
	}
		
	void OnCollisionExit(Collision col){
		if(col.gameObject.tag == "Ground"){
			isAirBorn = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CheckButtons();
		
		FindMovement();

		Move();
				
		ray = new Ray(transform.position, Vector3.down);
		
		if(isAirBorn == true || isJumping == true) isSprinting = false;
	}
}
