/// <summary>
/// Clothing.cs
/// </summary>

using UnityEngine;
using System.Collections;

public class Clothing : BuffItem {
	private EquipmentSlot _slot; //store the slot the armor will be in

	public Clothing(){

		_slot = EquipmentSlot.Head;
	}
	public Clothing(EquipmentSlot slot){
		_slot = slot;
	}
	public EquipmentSlot Slot{
		get{return _slot;}
		set{_slot = value;}
	}
}

//public enum ArmorSlot{
//	Head,
//	Upperbody,
//	Legging,
//	Boots,
//	OffHand
//}
