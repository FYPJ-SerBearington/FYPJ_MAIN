using UnityEngine;
using System.Collections;
using System;

public static class GameSetting2 {
	public const string VERSION_KEY_NAME = "ver";
	public const float VERSION_NUMBER = 0.1f;
	private const string PLAYER_POSITION = "Player Position";
	private const string _NAME = "Player Name";
	private const string _BASE_VALUE = " - BASE VALUE";
	private const string _EXP_TO_LEVEL = " - EXP TO LEVEL";
	private const string _CUR_VALUE = " - Cur Value";

	#region Resource paths
	public const string MELEE_WEAPON_ICON_PATH = "Item/Icon/Weapon/Melee/";
	public const string MELEE_WEAPON_MESH_PATH = "Item/Mesh/Weapon/Melee/";

	public const string SHIELD_ICON_PATH = "Item/Icon/Armor/Shield/";
	public const string SHIELD_MESH_PATH = "Item/Mesh/Armor/Shield/";

	public const string HEAD_ICON_PATH = "Item/Icon/Armor/Head/";
	public const string HEAD_MESH_PATH = "Item/Mesh/Armor/Head/";

	public const string TORSO_ICON_PATH = "Item/Icon/Armor/Torso/";
	public const string TORSO_MESH_PATH = "Item/Mesh/Armor/Torso/";
	#endregion

//	public static PlayerCharacter pc;

	//index 0 = menu
	//index 1 = LoadSave
	//index 2= Character Generation
	//index 3= game
	public static string[] levelNames ={
		"Menu",
		"LoadSave",
		"Character Generation",
		"game",
		"testingScene"
	} ;


	static GameSetting2(){
		//PlayerCharacter.Instance.Awake ();
	}
	
	public static void SaveName(string name){
		PlayerPrefs.SetString (_NAME, name);
	}
	public static string LoadName(){
		return PlayerPrefs.GetString (_NAME, "Anonomys");
	}
	//Attributes saving
	public static void SaveAttribute(AttributeName name, Attribute attribute){
		PlayerPrefs.SetInt(((AttributeName)name).ToString() + _BASE_VALUE, attribute.BaseValue);
		PlayerPrefs.SetInt(((AttributeName)name).ToString() + _EXP_TO_LEVEL,attribute.ExpToLevel);
	}
	public static void LoadAttribute(AttributeName name){
		PlayerCharacter.Instance.GetPrimaryAttribute((int)name).BaseValue =  PlayerPrefs.GetInt (((AttributeName)name).ToString () + _BASE_VALUE, 0);
		PlayerCharacter.Instance.GetPrimaryAttribute((int)name).ExpToLevel = PlayerPrefs.GetInt (((AttributeName)name).ToString () + _EXP_TO_LEVEL, Attribute.STARTING_EXP_COST);
	}
	public static void SaveAttributes(Attribute[] attribute){
		for (int count = 0; count < attribute.Length; count++) {
			SaveAttribute((AttributeName)count, attribute[count] );
		}
	}
	public static void LoadAttributes(){
		for (int count = 0; count < Enum.GetValues(typeof(AttributeName)).Length; count++) {
			LoadAttribute((AttributeName) count);
		}

	}

	//Vital Saving
	public static void SaveVital(VitalName name, Vital vital){
		PlayerPrefs.SetInt(((VitalName)name).ToString() + _BASE_VALUE, vital.BaseValue);
		PlayerPrefs.SetInt(((VitalName)name).ToString() + _EXP_TO_LEVEL,vital.ExpToLevel);
		PlayerPrefs.SetInt(((VitalName)name).ToString() + _CUR_VALUE, vital.CurValue);

	}
	
	public static void LoadVital(VitalName name){
		PlayerCharacter.Instance.GetVital((int)name).BaseValue =  PlayerPrefs.GetInt(((VitalName)name).ToString() + _BASE_VALUE, 0);//Default value is 0 if Vital Base value is not found
		PlayerCharacter.Instance.GetVital((int)name).ExpToLevel =  PlayerPrefs.GetInt(((VitalName)name).ToString() + _EXP_TO_LEVEL,0);

		//make sure to call this so that the AdjustedBase Value will be updated before you try to call get the cur value
		PlayerCharacter.Instance.GetVital((int)name).Update();

		//get the stored value for the cur value for each vital
		PlayerCharacter.Instance.GetVital((int)name).CurValue = PlayerPrefs.GetInt(((VitalName)name).ToString() + _CUR_VALUE, 1);
	}
	public static void SaveVitals(Vital[] vital){
		for (int count = 0; count < vital.Length; count++) {
			SaveVital((VitalName)count, vital[count] );
		}
	}
	public static void LoadVitals(){
		for (int count = 0; count < Enum.GetValues(typeof(VitalName)).Length; count++) 
		{
			LoadVital((VitalName) count);
		}
	}

	//Skill Saving
	public static void SaveSkill(SkillName name, Skill skill){
		PlayerPrefs.SetInt(((SkillName)name).ToString() + _BASE_VALUE, skill.BaseValue);
		PlayerPrefs.SetInt(((SkillName)name).ToString() + _EXP_TO_LEVEL,skill.ExpToLevel);

	}
	
	public static void LoadSkill(SkillName name){
		PlayerCharacter.Instance.GetSkill((int)name).BaseValue = PlayerPrefs.GetInt(((SkillName)name).ToString() + _BASE_VALUE,0);
		PlayerCharacter.Instance.GetSkill((int)name).ExpToLevel =PlayerPrefs.GetInt(((SkillName)name).ToString() + _EXP_TO_LEVEL,0);
		PlayerCharacter.Instance.GetSkill ((int)name).Update ();
	}
	public static void SaveSkills(Skill[] skill){
		for (int count = 0; count < skill.Length; count++)
			SaveSkill ((SkillName)count, skill[count]);
	}
	public static void LoadSkills(){
		for (int count = 0; count < Enum.GetValues(typeof(SkillName)).Length; count++) 
		{
			LoadSkill((SkillName) count);
		}

	}
	public static void SaveGameVersion(){
		PlayerPrefs.SetFloat(VERSION_KEY_NAME, VERSION_NUMBER);
	}
	public static float LoadGameVersion(){
		return PlayerPrefs.GetFloat ("ver",0);
	}
//	public static void SavePlayerPosition(Vector3 pos){
//		PlayerPrefs.SetInt (PLAYER_POSITION + "x", pos.x);
//		PlayerPrefs.SetInt (PLAYER_POSITION + "y", pos.y);
//		PlayerPrefs.SetInt (PLAYER_POSITION + "z", pos.z);
//	}
//	public static void LoadPlayerPosition()
//	{
//		Vector3 temp = new Vector3(PlayerPrefs.SetInt (PLAYER_POSITION + "x", 1),
//		                           PlayerPrefs.SetInt (PLAYER_POSITION + "y", 1),
//		                           PlayerPrefs.SetInt (PLAYER_POSITION + "z", 1)
//		                           );
//		return temp;
//	}
}
