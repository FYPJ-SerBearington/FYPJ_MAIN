using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class myGUI : MonoBehaviour {
	public float lootWindowHeight = 90;

	public float buttonWidth = 40;
	public float buttonHeight = 40;
	public float closeButtonWidth = 20;
	public float closeButtonHeight = 20;
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
	private const int _INVENTORY_WINDOW_ID = 0;
	private Rect _inventoryWindowRect = new Rect(10,10,170,265);
	private int _inventoryRows = 6;
	private int _inventoryCols = 4;

	/******************************************/
	//Character window variables
	/******************************************/
	private bool _displayCharacterWindow = true;
	private const int _CHARACTER_WINDOW_ID = 0;
	private Rect _characterWindowRect = new Rect(10,10,170,265);
	private int _characterPanel = 0;
	private string[] _characterPanelNames = new string[]{"Equiment","Attributes", "Skills"};

	// Use this for initialization
	void Start () {

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
		if(_displayInventoryWindow)
			_inventoryWindowRect = GUI.Window (_INVENTORY_WINDOW_ID, _inventoryWindowRect , InventoryWindow, "Inventory");

		if(_displayCharacterWindow)
			_characterWindowRect = GUI.Window (_CHARACTER_WINDOW_ID, _characterWindowRect , CharacterWindow, "Character");


		if(_displayLootWindow)
			_lootWindowRect = GUI.Window (_LOOT_WINDOW_ID, new Rect (_offset, Screen.height - (_offset + lootWindowHeight), Screen.width - (_offset * 2), lootWindowHeight), LootWindow, "Loot Window");
		DisplayToolTip ();
	}

	private void LootWindow(int id){
		if(GUI.Button(new Rect(_lootWindowRect.width - 20, 0, closeButtonWidth,closeButtonHeight),"x")){
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
			if(GUI. Button(new Rect(5 + (buttonWidth * count), 0, buttonWidth,buttonHeight),new GUIContent( chest.loot[count].Name, chest.loot[count].ToolTip() ))){
				PlayerCharacter.Inventory.Add(chest.loot[count]);
				chest.loot.RemoveAt(count);
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
				if(counter < PlayerCharacter.Inventory.Count){
					GUI.Button(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight),buttonWidth,buttonHeight), new GUIContent( PlayerCharacter.Inventory[counter].Name,PlayerCharacter.Inventory[counter].ToolTip()));
				}
				else{
					GUI.Button(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight),buttonWidth,buttonHeight), (x + y * _inventoryCols).ToString(),"box");
					
				}
				counter++;
			}
		}
		SetToolTip ();
		GUI.DragWindow ();
	}

	public void ToggleInventoryWindow(){
		_displayInventoryWindow = !_displayInventoryWindow;
	}

	public void CharacterWindow(int id){
		_characterPanel = GUI.Toolbar (new Rect (5, 25, _characterWindowRect.width - 10, 50), _characterPanel, _characterPanelNames);

		switch (_characterPanel) {
		case 0:
			DisplayEquiment();
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

	private void DisplayEquiment(){
	}
	private void DisplayAttribute(){
	}
	private void DisplaySkills(){
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
