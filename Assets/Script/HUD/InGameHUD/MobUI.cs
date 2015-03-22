/// <summary>
/// Mob UI.cs
/// </summary>

using UnityEngine;
using System.Collections;

public class MobUI : BaseCharacter {
	public int curHealth;
	public int maxHealth;
	// Use this for initialization
	void Start () {
//		GetPrimaryAttribute ((int)AttributeName.Vitality).BaseValue = 100;
//		GetVital ((int)VitalName.Health).Update ();

		CName = "Slug Mob";
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void DisplayHealth(){
		Messenger<int,int>.Broadcast("mob health update", curHealth, maxHealth);
	}
}
