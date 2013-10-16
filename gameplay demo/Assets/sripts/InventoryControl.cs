using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryControl : MonoBehaviour {
	
	public bool isDisplayingInventory = false;
	
	/*GUI Window Variables*/
	private int inventoryWidth, inventoryHeight;
	private Rect inventoryRect;
	
	public int inventoryX = 100, inventoryY = 50, inventoryYOffSet = 20, areaBuffer = 10, upperAreaX = 10, upperAreaY, upperAreaHeight = 200;
	
	private int charX, charWidth;
	public Texture2D charTex;
	private Rect charRect;
	
	public int ArmSlots = 4;
	private int armSize, armX, armWidthAdded = 10;
	
	public int WeapPresets = 4, WeapPerSet = 2;
	private int weapX, weapWidthAdded, weapSize;
	
	public int ItemSlotsX = 3, ItemSlotsY = 3;
	private int itemX, itemWidthAdded, itemSize;
	
	private int openSlotY = 300, openSlotSize = 50, Cols, Rows = 4;
	public int OpenSlots = 100;
	
	private Rect sliderRect, sliderView;
	private Vector2 sliderVector = Vector2.zero;
	
	/*variables for selector box*/
	public float selectedX, selectedY, selectedInList;
	public Rect selected, selector;
	public bool somethingSelected = false;
	public GameObject selectedItem;
	
	/*Lists for items*/
	public List<GameObject> itemsInInventory;
	public List<Texture2D> itemTextures;
	public List<GameObject> possibleItemsToHave;
	
	void Start(){
		selectedItem = itemsInInventory[0];
		
		inventoryWidth = Screen.width - inventoryX * 2;
		inventoryHeight = Screen.height - inventoryY * 2;
		
		armSize = upperAreaHeight / ArmSlots;
		weapSize = upperAreaHeight / WeapPresets;
		itemSize = upperAreaHeight / ItemSlotsY;
		charWidth = upperAreaHeight / 2;
		
		armX = upperAreaX + areaBuffer;
		charX = armX + areaBuffer + armSize + armWidthAdded + inventoryX;
		weapX = charX + areaBuffer + charWidth;
		itemX = weapX + areaBuffer + (weapSize + weapWidthAdded) * WeapPerSet;
		
		Cols = OpenSlots / Rows;
		
		inventoryRect = new Rect(inventoryX, inventoryY + inventoryYOffSet, inventoryWidth, inventoryHeight);
		charRect = new Rect (charX, upperAreaY + inventoryY + inventoryYOffSet, charWidth, upperAreaHeight);
		sliderRect = new Rect (inventoryX + openSlotSize / 4, inventoryY + openSlotY - openSlotSize / 2 + areaBuffer, inventoryWidth - openSlotSize / 2, openSlotSize * Rows + openSlotSize / 2);
		sliderView = new Rect (inventoryX, inventoryY + openSlotY, Cols * openSlotSize - openSlotSize * 1.25f - 5, Rows * openSlotSize);
		
	}
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "item"){	
			itemControl ic = (itemControl) col.gameObject.GetComponent ("itemControl");
			
			AddItem(ic.myTex, ic.myObject);
			
			Destroy(col.gameObject);
		}
	}
	
	public void AddItem(Texture2D tex, GameObject go){
		itemTextures.Add(tex);
		
		foreach(GameObject obj in possibleItemsToHave){
			if(obj.name == go.name){
				itemsInInventory.Add(obj);	
			}
		}
	}
	
	public void GUIBase(){
		
		GUI.Box(inventoryRect,"inventory");
			GUI.DrawTexture(charRect,charTex);
			for(int y = 0; y < ArmSlots; y++){
				GUI.Box(new Rect(armX + inventoryX, (upperAreaY + (armSize * y)) + inventoryY + inventoryYOffSet, armWidthAdded + armSize, armSize),
					"arm");
			}
		
		for(int x = 0; x < WeapPerSet; x++){
			for(int y = 0; y < WeapPresets; y++){
				GUI.Box(new Rect(weapX + (weapWidthAdded + weapSize) * x, (upperAreaY + (weapSize * y)) + inventoryYOffSet + inventoryY, weapSize + weapWidthAdded, weapSize),
					"weapon");
			}
		}
		for(int x = 0; x < ItemSlotsX; x++){
			for(int y = 0; y < ItemSlotsY; y++){
				GUI.Box(new Rect(itemX + (itemWidthAdded + itemSize) * x, (upperAreaY + (itemSize * y)) + inventoryY + inventoryYOffSet, itemSize + itemWidthAdded, itemSize),
					"item");
			}
		}
		sliderVector = GUI.BeginScrollView(sliderRect, sliderVector,sliderView);
			for(int x = 0; x < Cols; x++){
				for(int y = 0; y < Rows; y++){
					if((x+y*Cols) < itemsInInventory.Count){
						GUI.Box(new Rect(openSlotSize * x, openSlotSize * y + openSlotY + openSlotSize, openSlotSize, openSlotSize),
							itemsInInventory[(x+y*Cols)].name);
						GUI.DrawTexture(new Rect(openSlotSize * x, openSlotSize * y + openSlotY + openSlotSize, openSlotSize, openSlotSize),
							itemTextures[(x+y*Cols)]);
					}else{						
					GUI.Box(new Rect(openSlotSize * x, openSlotSize * y + openSlotY + openSlotSize, openSlotSize, openSlotSize),
						"open");
					}
						
				}
			}
		GUI.EndScrollView();

	}
	
	public void SelectBase(){
		if(Input.GetKeyDown(KeyCode.D)){
			selectedX += .5f;	
		}
		if(Input.GetKeyDown(KeyCode.A)){
			selectedX -= .5f;	
		}
		if(Input.GetKeyDown(KeyCode.W)){
			selectedY -= .5f;
		}
		if(Input.GetKeyDown(KeyCode.S)){
			selectedY += .5f;
		}
		
		if(selectedY < 0){
			if(selectedX < 1){
				GUI.Box(new Rect(armX + inventoryX, (upperAreaY + (armSize * (selectedY + 4))) + inventoryY + inventoryYOffSet, armSize + armWidthAdded, armSize),"select");
				if(selectedY < -4) selectedY = -4;
				selector = new Rect(armX + inventoryX, (upperAreaY + (armSize * (selectedY + 4))) + inventoryY + inventoryYOffSet, armSize + armWidthAdded, armSize);
				if(Input.GetKeyDown(KeyCode.F)){
					if(somethingSelected == true){
						somethingSelected = false;
					}
					if(somethingSelected == false){
						
						selected = new Rect(armX + inventoryX, (upperAreaY + (armSize * (selectedY + 4))) + inventoryY + inventoryYOffSet, armSize + armWidthAdded, armSize);
						somethingSelected = true;
						
					}
				}				
			}
			if(selectedX >=1 && selectedX <= 2){
				GUI.Box(new Rect(weapX + (weapWidthAdded + weapSize) * (selectedX - 1), (upperAreaY + (weapSize * (selectedY + 4))) + inventoryY + inventoryYOffSet, weapSize + weapWidthAdded, weapSize),"select");
				if(selectedY < -4) selectedY = -4;
				selector = new Rect(armX + inventoryX, (upperAreaY + (armSize * (selectedY + 4))) + inventoryY + inventoryYOffSet, armSize + armWidthAdded, armSize);
				if(Input.GetKeyDown(KeyCode.F)){
						if(somethingSelected == true){
							somethingSelected = false;
						}
						if(somethingSelected == false){
						selected = new Rect(weapX + (weapWidthAdded + weapSize) * (selectedX - 1), (upperAreaY + (weapSize * (selectedY + 4))) + inventoryY + inventoryYOffSet, weapSize + weapWidthAdded, weapSize);
						somethingSelected = true;
						
					}
				}
			}
			if(selectedX > 2){
					GUI.Box(new Rect(itemX + (itemWidthAdded + itemSize) * (selectedX - 3),	(upperAreaY + (itemSize * (selectedY + 3))) + inventoryY + inventoryYOffSet, itemSize + itemWidthAdded, itemSize), "select");
				if(selectedY < -3) selectedY = -3;
				selector = new Rect(armX + inventoryX, (upperAreaY + (armSize * (selectedY + 4))) + inventoryY + inventoryYOffSet, armSize + armWidthAdded, armSize);
				if(Input.GetKeyDown(KeyCode.F)){
					if(somethingSelected == true){
						somethingSelected = false;
					}
					if(somethingSelected == false){
						selected = new Rect(itemX + (itemWidthAdded + itemSize) * (selectedX - 3),	(upperAreaY + (itemSize * (selectedY + 3))) + inventoryY + inventoryYOffSet, itemSize + itemWidthAdded, itemSize);
						somethingSelected = true;
						
					}
				}
			}
			if(selectedX > 5) selectedX = 5;
			if(selectedX < 0) selectedX = 0;
		}else{
			GUI.Box(new Rect((selectedX * openSlotSize) + sliderRect.x, selectedY * openSlotSize + sliderRect.y, openSlotSize, openSlotSize),"select");
			if(selectedY > 3) selectedY = 3;
			if(selectedX > 9){
				selectedX = 9;
				sliderVector.x += openSlotSize;
			}if(selectedX < 0){
				selectedX = 0;
				sliderVector.x -= openSlotSize;
			}
			selector = new Rect(armX + inventoryX, (upperAreaY + (armSize * (selectedY + 4))) + inventoryY + inventoryYOffSet, armSize + armWidthAdded, armSize);
			if(Input.GetKeyDown(KeyCode.F)){
				if(somethingSelected == true){
					somethingSelected = false;
				}
				if(somethingSelected == false){
					selected = new Rect((selectedX * openSlotSize) + sliderRect.x, selectedY * openSlotSize + sliderRect.y, openSlotSize, openSlotSize);
					somethingSelected = true;
					
				}
			}
			selectedInList = selectedX+selectedY*Cols;
		}
		if(somethingSelected == true){
			GUI.Box(selected,"selected");
		}

	}
	
	public void WorkSelected(){
		
		equipment eq = (equipment) GetComponent ("equipment");
		itemControl ic = (itemControl) selectedItem.GetComponent ("itemControl");
		
		if(Input.GetKeyDown(KeyCode.F)){
			if(selectedY >= 0){
				selectedItem = itemsInInventory[(int)(selectedX+selectedY*Cols) + 2];
			}else{
				if(selectedX < 1 /*&& ic.itemType == "armour"*/){
					if(ic.moreSpecific == "helm" && selectedY == -3){
						//itemsInInventory.Add(eq.armour[0]);
						//itemTextures.Add(eq.amourTex[0]);
						
						eq.armour.Remove(eq.armour[0]);
						eq.armour[0] = selectedItem;
						
						Debug.Log("should work");
					}
					if(ic.moreSpecific == "chestPlate" && selectedY == -2){
						itemsInInventory.Add(eq.armour[1]);
						eq.armour.Remove(eq.armour[1]);
						eq.armour[1] = selectedItem;
					}
					if(ic.moreSpecific == "arms" && selectedY == -1){
						itemsInInventory.Add(eq.armour[2]);
						eq.armour.Remove(eq.armour[2]);
						eq.armour[2] = selectedItem;
					}
					if(/*ic.moreSpecific == "legs" &&*/ selectedY == 0){
						itemsInInventory.Add(eq.armour[3]);
						eq.armour.Remove(eq.armour[3]);
						eq.armour[3] = selectedItem;
						Debug.Log("tag issues");
					}
					Debug.Log("need fewer if's");
				}else if(selectedX >=1 && selectedX <= 2){
					eq.weapons.Add(selectedItem);
				}else if(selectedX > 2 && ic.itemType != "amo"){
					
				}
				Debug.Log("well, the button works");
			}
		}
	}
	
	void OnGUI(){		
		if(isDisplayingInventory == true){
			
			GUIBase();
			
			SelectBase();
			
			WorkSelected();
			
		}

	}
	
	void Update(){
		Rigid_contorler ri = (Rigid_contorler) GetComponent ("Rigid_contorler");
		if(isDisplayingInventory == true){
			ri.canMove = false;
		}else{
			ri.canMove = true;	
		}
	}
}