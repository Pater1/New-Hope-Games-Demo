using UnityEngine;
using System.Collections;

public class Enemy_health_tracking : MonoBehaviour {
	
	public int maxHealth = 100, curHealth = 100;
	
	public float healthBarLength;
	
	// Use this for initialization
	void Start () {
		healthBarLength = Screen.width / 2;
	}
	
	// Update is called once per frame
	void Update () {
		AddjustCurentHealth(0);
	}
	
	void OnGUI(){
		GUI.Box(new Rect((Screen.width - healthBarLength),10, healthBarLength, 20), curHealth + "/" + maxHealth);	
	}
	
	public void AddjustCurentHealth(int adj){
		curHealth += adj;
		
		if(curHealth < 0){
			curHealth = 0;	
		}
		if(curHealth > maxHealth){
			curHealth = maxHealth;	
		}
		if(maxHealth < 1){
			maxHealth = 1;	
		}
		
		healthBarLength = (Screen.width/2) * (curHealth / (float)maxHealth);
	}
}
