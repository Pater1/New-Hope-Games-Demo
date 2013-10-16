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
	public float selectedX, selectedY;
	public Rect selected;
	
	/*Lists for items*/
	public List<GameObject> itemsInInventory;
	public List<Texture2D> itemTextures;
	public List<GameObject> possibleItemsToHave;
	
	void Start(){
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
		sliderView = new Rect (inventoryX, inventoryY + openSlotY, Cols * openSlotSize, Rows * openSlotSize);
		
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
	
	public Vector2 scrollPosition = Vector2.zero;
	
	void OnGUI(){
		Rigid_contorler ri = (Rigid_contorler) GetComponent ("Rigid_contorler");
		
		if(isDisplayingInventory == true){
			
			GUIBase();
						
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
			
			GUI.Box(new Rect((selectedX * openSlotSize) + sliderRect.x, selectedY * openSlotSize + sliderRect.y, openSlotSize, openSlotSize),"select");
			
			ri.canMove = false;
		}else{
			ri.canMove = true;	
		}
	}
}