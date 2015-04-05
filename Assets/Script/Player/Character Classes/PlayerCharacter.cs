/// <summary>
/// Player character.cs
/// Gibbie Chairul
/// 3/29/2015
/// 
/// This script controls the users ingame characture. Make sure this script is attached to the character prefab
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Player/Player Character Stats")]
//Inherit from base character
public class PlayerCharacter : BaseCharacter {
//	public GameObject playerPreb;
	private List<Item> inventory = new List<Item>();
	public List<Item> Inventory{
		get{ return inventory;}
		set{ inventory = value;}
	}

	private Item[] _equipment = new Item[(int)EquipmentSlot.COUNT];

//	public Item _equipedWeapon;

	public bool initialized = false;

	private static PlayerCharacter instance = null;
	public static PlayerCharacter Instance{
		get{
			if(instance == null){
				Debug.Log("instanceing a new pc");

				GameObject go = Instantiate( Resources.Load("Character/Model/_ _Ser Bearington")) as GameObject;
			

				PlayerCharacter temp = go.GetComponent<PlayerCharacter>();;

				if(temp == null)
					Debug.LogError("Player Prefab does not contain an PC Script. Please add an configure");

				instance = go.GetComponent<PlayerCharacter>();
				go.name = "PC";
				go.tag = "Player";
			}
			return instance;

		}
	}
	public void Initialize(){


		if (!initialized)
			LoadCharacter ();
	}
	#region unity function
	public new void Awake()
	{
		base.Awake();
		//Debug.Log(Resources.Load("Character/Model"));
		instance = this;
	}
	//we do not want to be sinding messages out each frame. We will be moving this out when we get back in to combat
	void Update(){
		//Messenger<int,int>.Broadcast("player health update", 100, 100,MessengerMode.DONT_REQUIRE_LISTENER);
	}
	#endregion
	


	public Item EquipedWeapon{
		get{ return _equipment[(int)EquipmentSlot.MainHand]; }
		set{ 
			_equipment[(int)EquipmentSlot.MainHand] = value;

			if( mainMount.transform.childCount > 0)
				Destroy(mainMount.transform.GetChild(0).gameObject);
//			Debug.Log(GameSetting2.MELEE_WEAPON_MESH_PATH + _equipedWeapon.Name);
			if(_equipment[(int)EquipmentSlot.MainHand] != null){
				GameObject mesh = Instantiate(Resources.Load(GameSetting2.MELEE_WEAPON_MESH_PATH + _equipment[(int)EquipmentSlot.MainHand].Name),mainMount.transform.position,mainMount.transform.rotation)as GameObject;
				mesh.transform.parent = mainMount.transform;
			}

		}
	}
	public Item EquipedShield{
		get{ return _equipment[(int)EquipmentSlot.OffHand]; }
		set{ 
			_equipment[(int)EquipmentSlot.OffHand] = value;
			
			if( offHandMount.transform.childCount > 0)
				Destroy(offHandMount.transform.GetChild(0).gameObject);

			if(_equipment[(int)EquipmentSlot.OffHand] != null){
				Debug.Log(GameSetting2.SHIELD_ICON_PATH + _equipment[(int)EquipmentSlot.OffHand].Name);
				GameObject mesh = Instantiate(Resources.Load(GameSetting2.SHIELD_MESH_PATH + _equipment[(int)EquipmentSlot.OffHand].Name),offHandMount.transform.position,offHandMount.transform.rotation)as GameObject;
				mesh.transform.parent = offHandMount.transform ;
			}
			
		}
	}
	public Item EquipedHead{
		get{ return _equipment[(int)EquipmentSlot.Head]; }
		set{ 
			_equipment[(int)EquipmentSlot.Head] = value;
			
			if( headMount.transform.childCount > 0)
				Destroy(headMount.transform.GetChild(0).gameObject);
			
			if(_equipment[(int)EquipmentSlot.Head] != null){
				Debug.Log(GameSetting2.SHIELD_ICON_PATH + _equipment[(int)EquipmentSlot.Head].Name);
				GameObject mesh = Instantiate(Resources.Load(GameSetting2.HEAD_MESH_PATH + _equipment[(int)EquipmentSlot.Head].Name),headMount.transform.position,headMount.transform.rotation)as GameObject;
				mesh.transform.parent = headMount.transform ;
			}
			
		}
	}

	public Item EquipedTorso{
		get{ return _equipment[(int)EquipmentSlot.Torso]; }
		set{ 
			_equipment[(int)EquipmentSlot.Torso] = value;
			
			if( torsoMount.transform.childCount > 0)
				Destroy(torsoMount.transform.GetChild(0).gameObject);
			
			if(_equipment[(int)EquipmentSlot.Torso] != null){
				Debug.Log(GameSetting2.HEAD_MESH_PATH + _equipment[(int)EquipmentSlot.Torso].Name);
				GameObject mesh = Instantiate(Resources.Load(GameSetting2.TORSO_MESH_PATH + _equipment[(int)EquipmentSlot.Torso].Name),torsoMount.transform.position,torsoMount.transform.rotation)as GameObject;
				mesh.transform.parent = torsoMount.transform ;
			}
			
		}
	}

	public void LoadCharacter(){
		GameSetting2.LoadAttributes ();
		ClearModifers ();
		GameSetting2.LoadVitals ();
		GameSetting2.LoadSkills ();

		initialized = true;

	}
	public void LoadArmor(){
	}
}

public enum EquipmentSlot{
	Head,
	Torso,
	Legging,
	Boots,
	OffHand,
	MainHand,
	COUNT			//MUST!Always at the end
}
