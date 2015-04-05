/// <summary>
/// Mob UI.cs
/// 3/11/2015
/// Gibbie Chairul
/// 
/// This script is responsible for controllling the mob.Attack this script to a mob prefab
/// </summary>


using UnityEngine;
using System.Collections;

[AddComponentMenu("Mob/All Mob Scripts")]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AI))]
[RequireComponent(typeof(AdvanceMovement))]
public class MobUI : BaseCharacter {
	public int curHealth;
	public int maxHealth;
	// Use this for initialization
	void Start () {
//		GetPrimaryAttribute ((int)AttributeName.Vitality).BaseValue = 100;
//		GetVital ((int)VitalName.Health).Update ();
		Transform displayName = transform.FindChild("Mob Name");

		if(displayName == null){
			Debug.LogWarning("Please add a 3d TExt to the mob");
			return;
		}

		displayName.GetComponent<MeshRenderer>().enabled = false;
		CName = "Slug Mob";
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void DisplayHealth(){
		Messenger<int,int>.Broadcast("mob health update", curHealth, maxHealth,MessengerMode.DONT_REQUIRE_LISTENER);
	}
}
