/// <summary>
/// My GUI.cs
/// 3/26/2015
/// Gibbie Chairul
/// 
/// This class will display all of the gui elementswhile the game is playing.
/// this includs:
/// Loot WIndow
/// Inventory Window
/// Character Panels (Equipment, attributes, skills)
/// HealBars, Satmiabars, Mana bars
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("GUI/MyGUI")]
public class myGUI : MonoBehaviour {
	public GUISkin mySkin;										//create a public variable for us to reference our custom guiskin

	public string emptyInventorySlotStyle;						//the style name for an empty inventory slot
	public string closeButtonStyle;								//the style name for a close cutton
	public string inventorySlotCommonStyle;						//the style name for a common item

	public float lootWindowHeight = 90;							//define how tall our loot window will be

	public float buttonWidth = 40;								//define the width of all the buttons we will be using for items
	public float buttonHeight = 40;								//define the height of all the buttons we will be using for items
	public float closeButtonWidth = 20;							//the width of a close button (small x in a corner)
	public float closeButtonHeight = 20;						//the width of a close button (small y in a corner)
	private float _offset = 10;
	/******************************************/
	//inventory loot variables
	/******************************************/
	private bool _displayLootWindow = false;
	private const int _LOOT_WINDOW_ID = 0;
	private Rect _lootWindowRect = new Rect(0,0,0,0);
	private Vector2 _lootWindowSlider = Vector2.zero;
	public static Chest chest;

	private string _toolTip = "";	
	/******************************************/
	//inventory window variables
	/******************************************/
	private bool _displayInventoryWindow = false;
	private const int _INVENTORY_WINDOW_ID = 1;
	private Rect _inventoryWindowRect = new Rect(10,10,170,265);
	private int _inventoryRows = 6;
	private int _inventoryCols = 4;


	private float _doubleClickTimer = 10;
	private const float _DOUBLE_CLICK_TIMER_THRESHHOLD = 0.5f;
	private Item _selectedItem;
	/******************************************/
	//Character window variables
	/******************************************/
	private bool _displayCharacterWindow = false;
	private const int _CHARACTER_WINDOW_ID = 2;
	private Rect _characterWindowRect = new Rect(10,10,170,265);
	private int _characterPanel = 0;
	private string[] _characterPanelNames = new string[]{"Equiment","Attributes", "Skills"};

	// Use this for initialization
	void Start () {
		PlayerCharacter.Instance.Initialize ();
	}
	private void OnEnable(){
		Messenger.AddListener("DisplayLoot",DisplayLoot);
		Messenger.AddListener("CloseChest",ClearWindow);
		Messenger.AddListener ("ToggleInventory",ToggleInventoryWindow);
		Messenger.AddListener ("ToggleCharacter",ToggleCharacterWindow);
	}
	private void OnDisable(){
		Messenger.RemoveListener("DisplayLoot",DisplayLoot);
		Messenger.RemoveListener("CloseChest",ClearWindow);
		Messenger.RemoveListener ("ToggleInventory",ToggleInventoryWindow);
		Messenger.RemoveListener ("ToggleCharacter",ToggleCharacterWindow);
	}
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		GUI.skin = mySkin;

		if(_displayInventoryWindow){
			_inventoryWindowRect = GUI.Window (_INVENTORY_WINDOW_ID, _inventoryWindowRect , InventoryWindow, "Inventory");
		}
		if(_displayCharacterWindow)
			_characterWindowRect = GUI.Window (_CHARACTER_WINDOW_ID, _characterWindowRect , CharacterWindow, "Character");


		if(_displayLootWindow)
			_lootWindowRect = GUI.Window (_LOOT_WINDOW_ID, new Rect (_offset, Screen.height - (_offset + lootWindowHeight), Screen.width - (_offset * 2), lootWindowHeight), LootWindow, "Loot Window");

		//displaying the stats
		DisplayToolTip ();
	}

	private void LootWindow(int id){
		GUI.skin = mySkin;

		if(GUI.Button(new Rect(_lootWindowRect.width - 20, 0, closeButtonWidth,closeButtonHeight),"x",closeButtonStyle)){
			ClearWindow();
		}

		if (chest == null)
			return;

		if (chest.loot.Count == 0) {
			ClearWindow();
			return;
		}
		_lootWindowSlider = GUI.BeginScrollView (new Rect(_offset,15,_lootWindowRect.width - 10,70),_lootWindowSlider, new Rect(0,0,(chest.loot.Count * buttonWidth) + _offset,buttonHeight + _offset));


		for (int count = 0; count < chest.loot.Count; count++) {
			if(GUI. Button(new Rect(5 + (buttonWidth * count), 0, buttonWidth,buttonHeight),new GUIContent( chest.loot[count].Icon, chest.loot[count].ToolTip()),emptyInventorySlotStyle)){
				PlayerCharacter.Instance.Inventory.Add(chest.loot[count]);
				chest.loot.RemoveAt(count);
				Debug.Log("Remove");
			}
		}

		GUI.EndScrollView ();
		
		SetToolTip ();
	}

	private void DisplayLoot(){
		_displayLootWindow = true;
	}

	private void ClearWindow(){
		_displayLootWindow = false;

		chest.OnMouseUp();
		chest = null;
	}

	public void InventoryWindow(int id){
		int counter = 0;

		for (int y = 0; y < _inventoryRows; y++) {
			for (int x = 0; x < _inventoryCols; x++) {
				if(counter < PlayerCharacter.Instance.Inventory.Count){
					if(GUI.Button(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight),buttonWidth,buttonHeight), new GUIContent( PlayerCharacter.Instance.Inventory[counter].Icon,PlayerCharacter.Instance.Inventory[counter].ToolTip()),inventorySlotCommonStyle)){
						//check if to see if it's the same button that is clicked previously and the time that elapsed since the last click 
						if(_doubleClickTimer != 0 && _selectedItem != null){
							//first click done on the item
							//Time difference between the first click and 2nd click. if it's within the time frame that we setup then we progress to doing something.
							if(Time.time - _doubleClickTimer < _DOUBLE_CLICK_TIMER_THRESHHOLD){
								Debug.Log("Double Click" + PlayerCharacter.Instance.Inventory[counter]);

								if(typeof(Weapon) == PlayerCharacter.Instance.Inventory[counter].GetType()){
									//Check if there an item in equipment
									if(PlayerCharacter.Instance.EquipedWeapon == null){
										PlayerCharacter.Instance.EquipedWeapon = PlayerCharacter.Instance.Inventory[counter];
										PlayerCharacter.Instance.Inventory.RemoveAt(counter);
										Debug.Log("Item Weapon from Inventory to Equiped");
									}
									else{
										//Move back/switched item to inventory
										Item temp = PlayerCharacter.Instance.EquipedWeapon; 							//temp item storage
										PlayerCharacter.Instance.EquipedWeapon = PlayerCharacter.Instance.Inventory[counter]; //Assigned the temp to item equiped that Player clicked on;
										PlayerCharacter.Instance.Inventory[counter] = temp;							//Inventory spot they get the item from and assigned it to what we've stored in temp;					
									}
								}
								else if(typeof(Armor) == PlayerCharacter.Instance.Inventory[counter].GetType()){
									Armor arm = (Armor)(PlayerCharacter.Instance.Inventory[counter]);
									switch(arm.Slot ){
									case EquipmentSlot.Head:
										Debug.Log("Head Slot");


										//Check if there an item in equipment
										if(PlayerCharacter.Instance.EquipedHead == null){
											PlayerCharacter.Instance.EquipedHead = PlayerCharacter.Instance.Inventory[counter];
											PlayerCharacter.Instance.Inventory.RemoveAt(counter);
											Debug.Log("Item Head from Inventory to Equiped");
										}
										else{
											//Move back/switched item to inventory
											Item temp = PlayerCharacter.Instance.EquipedHead; 							//temp item storage
											PlayerCharacter.Instance.EquipedHead = PlayerCharacter.Instance.Inventory[counter]; //Assigned the temp to item equiped that Player clicked on;
											PlayerCharacter.Instance.Inventory[counter] = temp;							//Inventory spot they get the item from and assigned it to what we've stored in temp;					
										}


										break;

									case EquipmentSlot.OffHand:
										Debug.Log("Shield Slot");

										//Check if there an item in equipment
										if(PlayerCharacter.Instance.EquipedShield == null){
											PlayerCharacter.Instance.EquipedShield = PlayerCharacter.Instance.Inventory[counter];
											PlayerCharacter.Instance.Inventory.RemoveAt(counter);
											Debug.Log("Item Shield from Inventory to Equiped");
										}
										else{
											//Move back/switched item to inventory
											Item temp = PlayerCharacter.Instance.EquipedShield; 							//temp item storage
											PlayerCharacter.Instance.EquipedShield = PlayerCharacter.Instance.Inventory[counter]; //Assigned the temp to item equiped that Player clicked on;
											PlayerCharacter.Instance.Inventory[counter] = temp;							//Inventory spot they get the item from and assigned it to what we've stored in temp;					
										}


										break;

									case EquipmentSlot.Torso:
										Debug.Log("Torso Slot");
										
										//Check if there an item in equipment
										if(PlayerCharacter.Instance.EquipedTorso == null){
											PlayerCharacter.Instance.EquipedTorso = PlayerCharacter.Instance.Inventory[counter];
											PlayerCharacter.Instance.Inventory.RemoveAt(counter);
											Debug.Log("Item Shield from Inventory to Equiped");
										}
										else{
											//Move back/switched item to inventory
											Item temp = PlayerCharacter.Instance.EquipedTorso; 							//temp item storage
											PlayerCharacter.Instance.EquipedTorso = PlayerCharacter.Instance.Inventory[counter]; //Assigned the temp to item equiped that Player clicked on;
											PlayerCharacter.Instance.Inventory[counter] = temp;							//Inventory spot they get the item from and assigned it to what we've stored in temp;					
										}
										
										
										break;

									default:
										Debug.Log("No defined Equipment slot");
										break;
									}


								}
								//reset our first double clicks timer and and selected items
								_doubleClickTimer = 0;
								_selectedItem = null;
							}
							//to handle if player click on 1 item then double click on the 2nd item
							else{
								Debug.Log("Reset the Double Click timer");
								//reset the time to the current click
								_doubleClickTimer = Time.time;
							}
						}
						else{
							//set it current time how long the game have been running
							_doubleClickTimer = Time.time;
							_selectedItem = PlayerCharacter.Instance.Inventory[counter];
						}
					}
				}
				else{
					GUI.Button(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight),buttonWidth,buttonHeight), (x + y * _inventoryCols).ToString(),inventorySlotCommonStyle);
					
				}
				counter++;
			}
		}
		SetToolTip ();
		GUI.DragWindow ();
	}

	public void ToggleInventoryWindow(){
		_displayInventoryWindow = !_displayInventoryWindow;
		Debug.Log("Recieve Toggle");
		Debug.Log(_displayInventoryWindow);
	}

	public void CharacterWindow(int id){
		_characterPanel = GUI.Toolbar (new Rect (5, 25, _characterWindowRect.width - 10, 50), _characterPanel, _characterPanelNames);

		switch (_characterPanel) {
		case 0:
			DisplayEquipment();
			break;
		case 1:
			DisplayAttribute();
			break;
		case 2:
			DisplaySkills();
			break;
		}

		GUI.DragWindow ();
	}

	private void DisplayEquipment(){

		if(PlayerCharacter.Instance.EquipedWeapon == null){
			GUI.Label(new Rect(20,100,40,40),"",inventorySlotCommonStyle);
		}
		else{
			//if we clicked on the item it will remove it and return it to inventory
			if(GUI.Button(new Rect(20,100,40,40),new GUIContent( PlayerCharacter.Instance.EquipedWeapon.Icon,PlayerCharacter.Instance.EquipedWeapon.ToolTip()) ) ){
				//add it to the inventory
				PlayerCharacter.Instance.Inventory.Add(PlayerCharacter.Instance.EquipedWeapon);
	
				//remove the item
				PlayerCharacter.Instance.EquipedWeapon = null;
			}
		}

		if(PlayerCharacter.Instance.EquipedShield == null){
			GUI.Label(new Rect(110,100,40,40),"",inventorySlotCommonStyle);
		}
		else{
			//if we clicked on the item it will remove it and return it to inventory
			if(GUI.Button(new Rect(110,100,40,40),new GUIContent( PlayerCharacter.Instance.EquipedShield.Icon,PlayerCharacter.Instance.EquipedShield.ToolTip()) ) ){
				//add it to the inventory
				PlayerCharacter.Instance.Inventory.Add(PlayerCharacter.Instance.EquipedShield);
				
				//remove the item
				PlayerCharacter.Instance.EquipedShield = null;
			}
		}

		if(PlayerCharacter.Instance.EquipedHead == null){
			GUI.Label(new Rect(65,80,40,40),"",inventorySlotCommonStyle);
		}
		else{
			//if we clicked on the item it will remove it and return it to inventory
			if(GUI.Button(new Rect(65,80,40,40),new GUIContent( PlayerCharacter.Instance.EquipedHead.Icon,PlayerCharacter.Instance.EquipedHead.ToolTip()) ) ){
				//add it to the inventory
				PlayerCharacter.Instance.Inventory.Add(PlayerCharacter.Instance.EquipedHead);
				
				//remove the item
				PlayerCharacter.Instance.EquipedHead = null;
			}
		}

		if(PlayerCharacter.Instance.EquipedTorso == null){
			GUI.Label(new Rect(65,130,40,40),"",inventorySlotCommonStyle);
		}
		else{
			//if we clicked on the item it will remove it and return it to inventory
			if(GUI.Button(new Rect(65,130,40,40),new GUIContent( PlayerCharacter.Instance.EquipedTorso.Icon,PlayerCharacter.Instance.EquipedTorso.ToolTip()) ) ){
				//add it to the inventory
				PlayerCharacter.Instance.Inventory.Add(PlayerCharacter.Instance.EquipedTorso);
				
				//remove the item
				PlayerCharacter.Instance.EquipedTorso = null;
			}
		}

		SetToolTip();
	}
	private void DisplayAttribute(){
		int LineHeight = 17;
		int valueDisplayWidth = 50;
		GUI.BeginGroup (new Rect(5,75,_characterWindowRect.width - (_offset *2 ),_characterWindowRect.height - 75),"");
		//GUI.Label(new Rect(10,10,50,25),"Label");

		//display the attributes
		for (int counter = 0; counter < PlayerCharacter.Instance.primaryAttribute.Length; counter++) {
			GUI.Label(new Rect(0,counter * LineHeight,_characterWindowRect.width -(_offset * 2) - valueDisplayWidth - 5,25),((AttributeName)counter).ToString());
			GUI.Label(new Rect(_characterWindowRect.width -(_offset * 2) - 25,counter *LineHeight,_characterWindowRect.width -(_offset * 2) - valueDisplayWidth,25),PlayerCharacter.Instance.GetPrimaryAttribute(counter).BaseValue.ToString());
		}
		//display the vitals
		for (int counter = 0; counter < PlayerCharacter.Instance.vital.Length; counter++) {
			GUI.Button(new Rect(0,(counter +PlayerCharacter.Instance.primaryAttribute.Length ) * LineHeight, _characterWindowRect.width -(_offset * 2) - valueDisplayWidth - 5,25),((VitalName)counter).ToString());
			GUI.Button(new Rect(_characterWindowRect.width -(_offset * 2) - valueDisplayWidth,(counter +PlayerCharacter.Instance.primaryAttribute.Length) *LineHeight,valueDisplayWidth,25),PlayerCharacter.Instance.GetVital(counter).CurValue + "/" + PlayerCharacter.Instance.GetVital(counter).AdjustedBaseValue);
		}
		GUI.EndGroup ();


	}
	private void DisplaySkills(){
		int LineHeight = 17;
		int valueDisplayWidth = 50;
		GUI.BeginGroup (new Rect(5,75,_characterWindowRect.width - (_offset *2 ),_characterWindowRect.height - 75),"");
		
		//display the skills
		for (int counter = 0; counter < PlayerCharacter.Instance.skill.Length; counter++) {
			GUI.Label(new Rect(0,counter * LineHeight,_characterWindowRect.width -(_offset * 2) - valueDisplayWidth - 5,25),((SkillName)counter).ToString());
			GUI.Label(new Rect(_characterWindowRect.width -(_offset * 2) - 25,counter *LineHeight,_characterWindowRect.width -(_offset * 2) - valueDisplayWidth,25),PlayerCharacter.Instance.GetSkill(counter).AdjustedBaseValue.ToString());
		}
		GUI.EndGroup ();
	}
	public void ToggleCharacterWindow(){
		_displayCharacterWindow = !_displayCharacterWindow;
	}

	private void SetToolTip(){
		if (Event.current.type == EventType.Repaint && GUI.tooltip != _toolTip) {
			if(_toolTip != "")
				_toolTip = "";
			if(GUI.tooltip != "")
				_toolTip = GUI.tooltip;

		}
	}
	private void DisplayToolTip(){
		if(_toolTip != "")
			GUI.Box (new Rect (Screen.width / 2 - 100, 10, 200, 100), _toolTip);
	}
}
