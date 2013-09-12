using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ally_targeting : MonoBehaviour {
	
	public List<Transform> targets;
	
	// Use this for initialization
	void Start () {
		targets = new List<Transform>();
		
		AddAllEnemy();
	}
	
	public void AddAllEnemy(){
		GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
		
		foreach(GameObject enemy in go){
			AddTarget(enemy.transform);
		}
	}
	
	public void AddTarget(Transform enemy){
		targets.Add(enemy);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
