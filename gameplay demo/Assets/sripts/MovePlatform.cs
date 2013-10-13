using UnityEngine;
using System.Collections;

public class MovePlatform : MonoBehaviour {
	
	public Vector3 startingVector, endingVector;
	
	public float travelDistance, travelSpeed;
	
	public string travelAxis;
	
	public bool travelingTo;
	
	// Use this for initialization
	void Start () {
		startingVector = transform.position;
		if(travelAxis == "x"){
			endingVector = new Vector3 (startingVector.x + travelDistance,startingVector.y,startingVector.z);
		}
		if(travelAxis == "y"){
			endingVector = new Vector3 (startingVector.x,startingVector.y + travelDistance,startingVector.z);
		}
		if(travelAxis == "z"){
			endingVector = new Vector3 (startingVector.x,startingVector.y,startingVector.z + travelDistance);
		}
	}
	
	void OnCollisionStay(Collision col){
		if(travelAxis == "x"){
			if(travelingTo == true){
				col.gameObject.transform.position += Vector3.right * travelSpeed * Time.deltaTime;
			}
			if(travelingTo == false){
				col.gameObject.transform.position -= Vector3.right * travelSpeed * Time.deltaTime;
			}
		}
		if(travelAxis == "y"){
			if(travelingTo == true){
				col.gameObject.transform.position += Vector3.up * travelSpeed * Time.deltaTime;
			}
			if(travelingTo == false){
				col.gameObject.transform.position -= Vector3.up * travelSpeed * Time.deltaTime;
			}
		}
		if(travelAxis == "z"){
			if(travelingTo == true){
				col.gameObject.transform.position += Vector3.forward * travelSpeed * Time.deltaTime;
			}
			if(travelingTo == false){
				col.gameObject.transform.position -= Vector3.forward * travelSpeed * Time.deltaTime;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		Rigid_contorler rc = (Rigid_contorler) GetComponent ("Rigid_contorler");
		
		if(transform.position == startingVector){
			travelingTo = true;	
		}
		if(transform.position == endingVector){
			travelingTo = false;	
		}
		if(travelAxis == "x"){
			if(travelingTo == true){
				transform.position += Vector3.right * travelSpeed * Time.deltaTime;
			}
			if(travelingTo == false){
				transform.position -= Vector3.right * travelSpeed * Time.deltaTime;
			}
		}
		if(travelAxis == "y"){
			if(travelingTo == true){
				transform.position += Vector3.up * travelSpeed * Time.deltaTime;
			}
			if(travelingTo == false){
				transform.position -= Vector3.up * travelSpeed * Time.deltaTime;
			}
		}
		if(travelAxis == "z"){
			if(travelingTo == true){
				transform.position += Vector3.forward * travelSpeed * Time.deltaTime;
			}
			if(travelingTo == false){
				transform.position -= Vector3.forward * travelSpeed * Time.deltaTime;
			}
		}
	}
}
