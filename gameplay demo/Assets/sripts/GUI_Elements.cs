using UnityEngine;
using System.Collections;

public class GUI_Elements : MonoBehaviour {
	
	public float allyHealthBarLength;
	
	public float enemyHealthBarLength;
	
	public int enemyHealthBarPlace;
	
	void OnGUI(){
		
		Ally_Health_Tracking ah = (Ally_Health_Tracking) GetComponent ("Ally_Health_Tracking");
		
		Enemy_health_tracking eh = (Enemy_health_tracking) GetComponent ("Enemy_health_tracking");
		
		//ally_targeting at = (ally_targeting) GetComponent ("ally_targeting");
		
		GUI.Box(new Rect(0,10, allyHealthBarLength, 20), ah.curHealth + "/" + ah.maxHealth);		
		
		GUI.Box(new Rect(0,10, enemyHealthBarLength, 20), eh.curHealth + "/" + eh.maxHealth);
		
		allyHealthBarLength = (Screen.width/2) * ((float)ah.curHealth / (float)ah.maxHealth);	
				
		//enemyHealthBarLength = (Screen.width/2) * ((float)eh.curHealth / (float)eh.maxHealth);
		
		//enemyHealthBarLength = Screen.width / 2;
		
		enemyHealthBarPlace = 50;

	}
	/*
	
	public Texture2D tex;
	
	public Rect fullRect;
	
	public float maxHealth;
	
	public float currHealth;
	
	void OnGUI() {
	
	    float healthFrac = currHealth / maxHealth;
	
	    float actualRect = Rect(fullRect.x, fullRect.y, fullRect.width * healthFrac, fullRect.height);
	
	    
	
	    GUI.BeginGroup(actualRect, tex);
	
	        Rect innerRect = Rect(0, 0, fullRect.width, fullRect.height);
	
	        GUI.DrawTexture(innerRect, tex);
	
	    GUI.EndGroup();
	
	}*/
}
