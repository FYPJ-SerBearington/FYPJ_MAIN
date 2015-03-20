using UnityEngine;
using System.Collections;
using System;					//Added to access enum class

public class BaseCharacter : MonoBehaviour {
	private string _cname;
	private int _level;
	private uint _freeExp; //Unsigned int

	private Attribute[] _primaryAttribute;
	private Vital[] _vital;
	private Skill[] _skill;

	//Default
	public void Awake(){
		//Debug.Log("Base Character Awake");
		_cname = string.Empty;
		_level = 0;
		_freeExp = 0;

		_primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		_vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		_skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];

		SetupPrimaryAttributes ();
		SetupVitals ();
		SetupSkills ();
	}

	public string CName{
		get{ return _cname; }
		set{ _cname = value;}
	}
	public int Level {
		get{ return _level; }
		set{ _level = value; }
	}
	public uint FreeExp{
		get{ return _freeExp; }
		set{ _freeExp = value;}
	}
	public void AddExp(uint exp){
		_freeExp += exp;

		CalculateLevel();
	}

	//Tage avr of all the players skills and assign that as the player level
	public void CalculateLevel(){

	}
	private void SetupPrimaryAttributes(){
		for(int count = 0; count <_primaryAttribute.Length; count++)
		{
			_primaryAttribute[count] = new Attribute();
			_primaryAttribute[count].Name = ((AttributeName)count).ToString();
		}
	}
	private void SetupVitals(){
		for(int count = 0; count <_vital.Length; count++)
		{
			_vital[count] = new Vital();
		}

		SetupVitalModifiers ();
	}
	private void SetupSkills(){
		for(int count = 0; count <_skill.Length; count++)
		{
			_skill[count] = new Skill();
		}
		SetupSkillModifiers ();
	}

	public Attribute GetPrimaryAttribute(int index){
		return _primaryAttribute[index];
	}
	public Vital GetVital(int index){
		return _vital[index];
	}
	public Skill GetSkill(int index){
		return _skill[index];
	}

	private void SetupVitalModifiers(){
		//Health
//		ModifyingAttribute health = new ModifyingAttribute ();
//		health.attribute = GetPrimaryAttribute ((int)AttributeName.Vitality);
//		health.ratio = 0.5f;
//
//		GetVital ((int)VitalName.Health).AddModifier (health);
		//Shorter way
		GetVital ((int)VitalName.Health).AddModifier (new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Vitality ), ratio = 0.5f});
		//Energy
		GetVital ((int)VitalName.Energy).AddModifier (new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Agility ), ratio = 1.0f});
		//Mana
		GetVital ((int)VitalName.Mana).AddModifier (new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Intelligence ), ratio = 1.0f});
	}

	private void SetupSkillModifiers(){
		//Melee Offence(Strength)
		GetSkill ((int)SkillName.Melee_Offence).AddModifier(new ModifyingAttribute( GetPrimaryAttribute((int)AttributeName.Strength ), 0.66f));
		GetSkill ((int)SkillName.Melee_Offence).AddModifier(new ModifyingAttribute( GetPrimaryAttribute((int)AttributeName.Agility), 0.33f));
		//Melee Defence(Strength)
		GetSkill ((int)SkillName.Melee_Defence).AddModifier(new ModifyingAttribute( GetPrimaryAttribute((int)AttributeName.Strength ),0.33f));
		GetSkill ((int)SkillName.Melee_Defence).AddModifier(new ModifyingAttribute( GetPrimaryAttribute((int)AttributeName.Agility),  0.33f));

		//Magic Offence(Intelligence)
		GetSkill ((int)SkillName.Magic_Offence).AddModifier(new ModifyingAttribute( GetPrimaryAttribute((int)AttributeName.Intelligence ), 1.66f));
		//Magic Defense(Intelligence)
		GetSkill ((int)SkillName.Magic_Defence).AddModifier(new ModifyingAttribute( GetPrimaryAttribute((int)AttributeName.Intelligence), 0.33f));

		//Range Offence(Agility)
		GetSkill ((int)SkillName.Ranged_Offence).AddModifier(new ModifyingAttribute( GetPrimaryAttribute((int)AttributeName.Agility ), 0.66f));
		//Range Defense(Agility)
		GetSkill ((int)SkillName.Ranged_Defence).AddModifier(new ModifyingAttribute( GetPrimaryAttribute((int)AttributeName.Agility), 0.33f));
	}
	//Update all of our modified stat real time
	public void StatUpdate(){
		for (int count = 0; count < _vital.Length; count++) 
		{
			_vital[count].Update();
		}
		for (int count = 0; count < _skill.Length; count++) 
		{
			_skill[count].Update();
		}

	}
}
