using System.Collections.Generic;

//Inherit from base character
public class PlayerCharacter : BaseCharacter {

	private static List<Item> _inventory = new List<Item>();
	public static List<Item> Inventory{
		get{return _inventory;}
		set{_inventory = value;}
	}

	void Update(){
		Messenger<int,int>.Broadcast("player health update", 80, 100);
	}
}
