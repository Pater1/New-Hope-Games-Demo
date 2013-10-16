using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadNewScene : MonoBehaviour {
	
	public string Level;
	public Vector3 startingArea;
	public List<GameObject> players;
	public int numberOfPlayers;
	public bool isLocked = false;
	public GameObject key;
	
	void Start(){
		AddAllPlayer();	
		
		numberOfPlayers = players.Count;
		
		if(numberOfPlayers > 1){
			EliminateExraPlayers();	
		}
	}
	
	void OnTriggerEnter (Collider col) {
		if(col.gameObject.tag == "Player"){
			
			DontDestroyOnLoad(col.gameObject);
			
			Application.LoadLevel(Level);
			
			Rigid_contorler ri = (Rigid_contorler) col.gameObject.GetComponent ("Rigid_contorler");
			
			ri.transform.position = startingArea;
		}
	}
	
	void OnCollisionEnter (Collision col) {
		if(col.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.F)){
			
			DontDestroyOnLoad(col.gameObject);
			
			Application.LoadLevel(Level);
			
			Rigid_contorler ri = (Rigid_contorler) col.gameObject.GetComponent ("Rigid_contorler");
			
			ri.transform.position = startingArea;
		}
	}
	
	void OnTriggerStay (Collider col) {
		if(col.gameObject.tag == "Player"){
			
			DontDestroyOnLoad(col.gameObject);
			
			Application.LoadLevel(Level);
			
			Rigid_contorler ri = (Rigid_contorler) col.gameObject.GetComponent ("Rigid_contorler");
			
			ri.transform.position = startingArea;
		}
	}
	
	void OnCollisionStay (Collision col) {
		if(col.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.F)){
			
			DontDestroyOnLoad(col.gameObject);
			
			Application.LoadLevel(Level);
			
			Rigid_contorler ri = (Rigid_contorler) col.gameObject.GetComponent ("Rigid_contorler");
			
			ri.transform.position = startingArea;
		}
	}
	
	public void AddAllPlayer(){
		GameObject[] go = GameObject.FindGameObjectsWithTag("Player");
		
		foreach(GameObject player in go){
			AddPlayer(player);
		}
	}
	
	public void AddPlayer(GameObject player){
		players.Add(player);
	}
	
	public void EliminateExraPlayers(){
		
		Destroy(players[1]);
			
	}
	
	void Update(){
		
		
	}
}
