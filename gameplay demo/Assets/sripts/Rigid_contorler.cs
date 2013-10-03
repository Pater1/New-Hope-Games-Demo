using UnityEngine;
using System.Collections;

public class Rigid_contorler : MonoBehaviour {
	
	public float maxSpeedForward = 20, maxSpeedBack = 15;
	public float accelerationFactor = .5f, maxSprintMultiplier = 3, maxSprintTime = 5;
	public float maxJumpDuration = 2, curJumpDuration, jumpSpeed = 7, curSprintTime;	
	public float curSpeedForward, curSpeedBackwards, curSprintMultiplier;	
	public bool isMovingForward, isTurningRight, isTurningLeft, isMovingBackwards, isSprinting, isAirBorn, isJumping;
	private Transform myTrans;
	
	
	//no clue how it works, but these are needed to rotate
	public Quaternion deltaRotation1, deltaRotation2;
	public Vector3 eulerAngleVelocity1 = new Vector3 (0, -100, 0);
	public Vector3 eulerAngleVelocity2 = new Vector3 (0, 100, 0);
	
	/*public RaycastHit hit;
	public Ray ray;
	public Collider someCollider;
	public float slope;
	
	public bool Raycast(Ray ray, RaycastHit hitInfo, float distance);*/
	// Use this for initialization
	void Start () {
		myTrans = transform;
		
		//ray = new Ray (transform.position, Vector3.down);
		
		//GameObject go = GameObject.FindGameObjectWithTag("Ground");
		
		//someCollider = go.collider();
		
	}
	
	private void Move(){
		
		myTrans.position += myTrans.forward * curSprintMultiplier * curSpeedForward * Time.deltaTime;
		
		myTrans.position -= myTrans.forward * curSprintMultiplier * curSpeedBackwards * Time.deltaTime;
		
		deltaRotation1 = Quaternion.Euler(eulerAngleVelocity1 * Time.deltaTime);
		deltaRotation2 = Quaternion.Euler(eulerAngleVelocity2 * Time.deltaTime);
		
		if (isTurningLeft == true){		
		rigidbody.MoveRotation(rigidbody.rotation * deltaRotation2);
		}
		if (isTurningRight == true){		
		rigidbody.MoveRotation(rigidbody.rotation * deltaRotation1);
		}
		
		Jump();
		
		if(isJumping == true){
			myTrans.position += myTrans.up * curJumpDuration * jumpSpeed * Time.deltaTime;
		}
		
		/*if (someCollider.Raycast(ray, hit, 10)){
			//slope = hit.normal();
    		transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
		}*/
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
		
		//ray = new Ray(transform.position, Vector3.down);
	}
}
