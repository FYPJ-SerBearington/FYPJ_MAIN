/// <summary>
/// Item generator.cs
/// 4/1/15 - 4/2/15
/// Gibbie Chairul
/// 
/// </summary>

using UnityEngine;
using System.Collections;

public static class ItemGenerator  {
	public const int BASE_MELEE_RANGE = 2;
	public const int BASE_RANGE_RANGE = 10;

//	private const string _MELEE_WEAPON_PATH = "Item Icons/Weapon/Melee/";
	public static Item CreateItem(){
		//decide what type of item to make
		Item item = new Item ();

		int rand = Random.Range (0,2);
		Debug.Log("Create Item" + rand);
		//call the method to create that base item type
		switch (rand) {
		case 0:
			item = CreateMeleeWeapon(); 
			break;
		case 1:
			item = CreateArmor();
			break;
		}



		item.Value = Random.Range (1, 101);
		item.Rare = RarityTypes.Common;
		item.MaxDurability = Random.Range (50,61);
		item.CurDurabilty = item.MaxDurability;

		//return the new Item
		return item;
	}
	private static Weapon CreateWeapon (){
		//Decide if we make a melee or a range weapon
		Weapon weapon = new Weapon ();

		//return the created weapon
		return weapon;
	}
	private static Weapon CreateMeleeWeapon(){
		Weapon meleeWeapon = new Weapon();

		string[] weaponNames = new string[]
		{
			"Sword",
			"Samurai"
		};


		//fill in all of the values for that item type
		meleeWeapon.Name = weaponNames[Random.Range(0,weaponNames.Length)];

		//assign the max damage of the weapon
		meleeWeapon.MaxDamage = Random.Range (5,11);
		meleeWeapon.DamageVariance = Random.Range (0.20f, 0.76f);
		meleeWeapon.TypeOfDamage = DamageType.Slash;

		//assign the max range of this weapon;
		meleeWeapon.MaxRange = BASE_MELEE_RANGE;

		//Assign the icon for the weapon
		meleeWeapon.Icon = Resources.Load(GameSetting2.MELEE_WEAPON_ICON_PATH + meleeWeapon.Name) as Texture2D;

		Debug.Log ("Creating Weapon");
		//return the melee weapon
		return meleeWeapon;
	}

	private static Armor CreateArmor(){
		//Decide if what type of armor to make
		int temp = Random.Range (0,3);
		Armor armor = new Armor();
		Debug.Log("Create Armor" + temp);
		switch(temp){
		case 0:
			armor = CreateShield ();
			break;
		case 1:
			armor = CreateHead ();
			break;
		case 2:
			armor = CreateTorso ();
			break;
		}
		//return the created weapon
		return armor;
	}

	private static Armor CreateShield(){
		Armor armor = new Armor();
		
		string[] shieldNames = new string[]
		{
			"Small_Shield",
			"Big_Shield"
		};
		
		//fill in all of the values for that item type
		armor.Name = shieldNames[Random.Range(0,shieldNames.Length)];

		//assign properties of the shield
		armor.ArmorLevel = Random.Range (10,50);

		
		//Assign the icon for the Shield
		armor.Icon = Resources.Load(GameSetting2.SHIELD_ICON_PATH + armor.Name) as Texture2D;

		//Debugging Stuffs
		Debug.Log (GameSetting2.SHIELD_ICON_PATH + armor.Name);
		Debug.Log ("Creating Shield");

		//Assign the equipment slot where this can be assigned
		armor.Slot = EquipmentSlot.OffHand;
		//return the melee weapon
		return armor;
	}
	private static Armor CreateHead(){
		Armor armor = new Armor();
		
		string[] headNames = new string[]
		{
			"head_bronze_armor",
			"head_gem_armor"
		};
		
		//fill in all of the values for that item type
		armor.Name = headNames[Random.Range(0,headNames.Length)];
		
		//assign properties of the shield
		armor.ArmorLevel = Random.Range (10,50);

		//Assign the icon for the head
		armor.Icon = Resources.Load(GameSetting2.HEAD_ICON_PATH+ armor.Name) as Texture2D;

		//Debugging Stuffs
		Debug.Log (GameSetting2.HEAD_ICON_PATH + armor.Name);
		Debug.Log ("Creating Head");

		//Assign the equipment slot where this can be assigned
		armor.Slot = EquipmentSlot.Head;
		//return the melee weapon
		return armor;
	}
	private static Armor CreateTorso(){
		Armor armor = new Armor();
		
		string[] torsoNames = new string[]
		{
			"torso_bronze_armor",
			"torso_gem_armor"
		};
		
		//fill in all of the values for that item type
		armor.Name = torsoNames[Random.Range(0,torsoNames.Length)];
		Debug.Log (armor.Name);
		//assign properties of the shield
		armor.ArmorLevel = Random.Range (10,50);
		
		//Assign the icon for the head
		armor.Icon = Resources.Load(GameSetting2.TORSO_ICON_PATH + armor.Name) as Texture2D;

		//Debugging Stuffs
		Debug.Log (GameSetting2.TORSO_ICON_PATH + armor.Name);
		Debug.Log ("Creating Head");
		
		//Assign the equipment slot where this can be assigned
		armor.Slot = EquipmentSlot.Torso;
		//return the melee weapon
		return armor;
	}
}

public enum ItemType{
	Armor,
	Weapon,
	Potion,
	Essence
}