using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Combat : MonoBehaviour {
	
	public GameObject target;
	public float timer, coolDown;
	// Use this for initialization
	void Start () {
		timer = 0;
		coolDown = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0){
			timer -= Time.deltaTime;	
		}
		if (timer < 0) timer = 0;
		
		if(Input.GetKeyDown(KeyCode.F)){
			if(timer == 0){
				Attack();
				timer = coolDown;
			}
		}
		if(target == null){
			ally_targeting al = (ally_targeting) GetComponent ("ally_targeting");
			
			al.targets  = new System.Collections.Generic.List<UnityEngine.Transform>();
			
			al.AddAllEnemy();
		}
	}
	
	private void Attack(){
		Vector3 dir = (target.transform.position - transform.position).normalized;
		
		float distance = Vector3.Distance(target.transform.position, transform.position);
		
		float direction = Vector3.Dot(dir, transform.forward);
		
		if (distance < 3){
			if (direction > 0){
				
				Enemy_health_tracking eh = (Enemy_health_tracking)target.GetComponent("Enemy_health_tracking");
				
				eh.AddjustCurentHealth(-10);
				
			}
		}
	}
}
