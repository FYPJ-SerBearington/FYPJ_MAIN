using UnityEngine;
using System.Collections;

public static class ItemGenerator  {
	public const int BASE_MELEE_RANGE = 2;
	public const int BASE_RANGE_RANGE = 10;

	//private const string _MELEE_WEAPON_PATH = "Item Icons/Weapon/Melee/";
	public static Item CreateItem(){
		//decide what type of item to make
		//call the method to create that base item type
		Item item = CreateWeapon ();

		item.Value = Random.Range (1, 101);
		item.Rare = RarityTypes.Common;
		item.MaxDurability = Random.Range (50,61);
		item.CurDurabilty = item.MaxDurability;


		//return the new Item
		return item;
	}
	private static Weapon CreateWeapon (){
		//Decide if we make a melee or a range weapon
		Weapon weapon = CreateMeleeWeapon ();

		//return the created weapon
		return weapon;
	}
	private static Weapon CreateMeleeWeapon(){
		Weapon meleeWeapon = new Weapon();
		//fill in all of the values for that item type
		meleeWeapon.Name = "W: " + Random.Range(0,100);

		//assign the max damage of the weapon
		meleeWeapon.MaxDamage = Random.Range (5,11);
		meleeWeapon.DamageVariance = Random.Range (0.20f, 0.76f);
		meleeWeapon.TypeOfDamage = DamageType.Slash;

		//assign the max range of this weapon;
		meleeWeapon.MaxRange = BASE_MELEE_RANGE;

		//return the melee weapon
		return meleeWeapon;
	}
}

public enum ItemType{
	Armor,
	Weapon,
	Potion,
	Essence
}