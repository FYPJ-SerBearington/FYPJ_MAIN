using UnityEngine;
using System.Collections;
using System;

public class GameSettings : MonoBehaviour {
	public const string PLAYER_SPAWN_POINT = "Player Spawn Point"; //This is the name of the game objext that the player will spawn at the start of the level

	void Awake(){
		DontDestroyOnLoad (this);
	}

	public void SaveCharacterData(){
		GameObject pc = GameObject.FindGameObjectWithTag("Player");

		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter> ();

		//PlayerPrefs.DeleteAll ();

		PlayerPrefs.SetString ("Player Name: ", pcClass.CName);

		for (int count = 0; count < Enum.GetValues(typeof(AttributeName)).Length; count++) 
		{
			PlayerPrefs.SetInt(((AttributeName)count).ToString() + "Base Value", pcClass.GetPrimaryAttribute(count).BaseValue);
			PlayerPrefs.SetInt(((AttributeName)count).ToString() + " - Exp To Level",pcClass.GetPrimaryAttribute(count).ExpToLevel);
		}
		for (int count = 0; count < Enum.GetValues(typeof(VitalName)).Length; count++) 
		{
			PlayerPrefs.SetInt(((VitalName)count).ToString() + "Base Value", pcClass.GetVital(count).BaseValue);
			PlayerPrefs.SetInt(((VitalName)count).ToString() + " - Exp To Level",pcClass.GetVital(count).ExpToLevel);
			PlayerPrefs.SetInt(((VitalName)count).ToString() + " - Cur Value", pcClass.GetVital(count).CurValue);

		}
		for (int count = 0; count < Enum.GetValues(typeof(SkillName)).Length; count++) 
		{
			PlayerPrefs.SetInt(((SkillName)count).ToString() + "Base Value", pcClass.GetSkill(count).BaseValue);
			PlayerPrefs.SetInt(((SkillName)count).ToString() + " - Exp To Level",pcClass.GetSkill(count).ExpToLevel);


		}
	}
	public void LoadCharacterData(){
		GameObject pc = GameObject.FindGameObjectWithTag("Player");
		
		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter> ();
		
		//PlayerPrefs.DeleteAll ();
		//Return actual string and assigned it to our player class the name of the character FROM the inputed in prev scene(GameSettings scene). If name is empty it's going to same "Name Me"			
		pcClass.CName = PlayerPrefs.GetString ("Player Name", "Name Me(EMPTY/DEFAULT NAME)");
		//Debug.Log (pcClass.CName);

		for (int count = 0; count < Enum.GetValues(typeof(AttributeName)).Length; count++) 
		{
			pcClass.GetPrimaryAttribute(count).BaseValue = PlayerPrefs.GetInt(((AttributeName)count).ToString() + "Base Value", 0);//Default value is 0 if Attribute cannot find
			pcClass.GetPrimaryAttribute(count).ExpToLevel = PlayerPrefs.GetInt(((AttributeName)count).ToString() + " - Exp To Level",Attribute.STARTING_EXP_COST);
		}
		//Debugging Base value and Exptolevel
		for (int count = 0; count < Enum.GetValues(typeof(AttributeName)).Length; count++) 
		{
			Debug.Log(pcClass.GetPrimaryAttribute(count).BaseValue+ " : " + pcClass.GetPrimaryAttribute(count).ExpToLevel);
		}
		for (int count = 0; count < Enum.GetValues(typeof(VitalName)).Length; count++) 
		{
			pcClass.GetVital(count).BaseValue =  PlayerPrefs.GetInt(((VitalName)count).ToString() + "Base Value", 0);//Default value is 0 if Vital Base value is not found
			pcClass.GetVital(count).ExpToLevel =  PlayerPrefs.GetInt(((VitalName)count).ToString() + " - Exp To Level",0);
			//make sure to call this so that the AdjustedBase Value will be updated before you try to call get the cur value
			pcClass.GetVital(count).Update();
			//get the stored value for the cur value for each vital
			pcClass.GetVital(count).CurValue = PlayerPrefs.GetInt(((VitalName)count).ToString() + " - Cur Value", 1);

		}
		for (int count = 0; count < Enum.GetValues(typeof(SkillName)).Length; count++) 
		{
			pcClass.GetSkill(count).BaseValue = PlayerPrefs.GetInt(((SkillName)count).ToString() + "Base Value",0);
			pcClass.GetSkill(count).BaseValue = PlayerPrefs.GetInt(((SkillName)count).ToString() + " - Exp To Level",0);
		}

	}
}
