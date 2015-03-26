using UnityEngine;
using System.Collections;

public class Jewelery : BuffItem {

	private JewelrySlot _slot; //Store the slot the jewelery is in

	public Jewelery(){
		_slot = JewelrySlot.PocketItem;
	}
	public Jewelery(JewelrySlot slot){
		_slot = slot;
	}
	public JewelrySlot Slot{
		get{return _slot; }
		set{_slot = value;}
	}
}

public enum JewelrySlot{
	Earings,
	Necklace,
	Bracelates,
	Rings,
	PocketItem,
}
