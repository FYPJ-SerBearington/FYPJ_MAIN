using UnityEngine;
using System.Collections;
using System;  //use for enum

public class CharacterGenerator : MonoBehaviour {
//	private PlayerCharacter _toon;
	private const int _STARTING_POINTS = 350;
	private const int _MIN_STARTING_ATTRIBUTE_VALUE = 10;
	private const int _STARTING_VALUE = 50;
	private int _pointsLeft;

	
	private const int _OFFSET = 5;				//Variable of the pixels around the screen
	private const int _LINE_HEIGHT = 20;		//How tall each our lines

	private const int _STAT_LABEL_WIDTH = 100;	//The Width of the stats
	private const int _BASEVALUE_LABEL_WIDTH = 30;// the width  of the number of the stats
	private const int _BUTTON_WIDTH = 20; // the width of the buttons
	private const int _BUTTON_HEIGHT = 20;// the height of the buttons
	
	public GUISkin mySkin;
	

	public float delayTimer = 0.25f;
	public float _lastClick = 0;
	private const int _statStartingPos = 40; // the start position of the stats
	void Awake(){
		//PlayerCharacter.Instance.Awake ();
	}
	// Use this for initialization
	void Start () {
		Debug.Log ("Start Character Generation");
		_pointsLeft = _STARTING_POINTS;

		for(int count = 0; count < Enum.GetValues(typeof(AttributeName)).Length; count++)
		{
			//Taking all of our base attributes and sign starting value
			PlayerCharacter.Instance.GetPrimaryAttribute(count).BaseValue = _STARTING_VALUE;
//			Debug.Log("Base Value"+ count+ " " + _pointsLeft);
			_pointsLeft -= ( _STARTING_VALUE - _MIN_STARTING_ATTRIBUTE_VALUE ); 
//			Debug.Log(_pointsLeft);
		}
		PlayerCharacter.Instance.StatUpdate ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		Debug.Log ("Displays stuffs");
		DisplayName ();
		DisplayPointsLeft();
		DisplayAttributes ();
		DisplayVitals ();
		DisplaySkills ();
		if(PlayerCharacter.Instance.CName == "" || _pointsLeft >0)
			DisplayCreateLabel ();
		else
		DisplayCreateButton ();
	}

	private void DisplayName(){
		GUI.Label (new Rect(10,10,50,25), "Name:");
		//Player Enter
		PlayerCharacter.Instance.CName = GUI.TextField(new Rect(65,10, 100, 25),PlayerCharacter.Instance.CName);

	}
	private void DisplayPointsLeft(){
		GUI.Label (new Rect(250,10,100,25), "Points Left: " + _pointsLeft.ToString());
	}

	private void DisplayAttributes(){
		// Displaying the name by looping the attribute from enum Attribute.cs
		for(int count = 0; count < Enum.GetValues(typeof(AttributeName)).Length; count++)
		{
			GUI.Label(new Rect(_OFFSET,										//x
			                   _statStartingPos + (count * _LINE_HEIGHT),	//y
			                   _STAT_LABEL_WIDTH,							//width
			                   _LINE_HEIGHT									//height
			                   ), ((AttributeName)count).ToString());

			GUI.Label(new Rect(_STAT_LABEL_WIDTH + _OFFSET,			//x
			                   _statStartingPos + (count * _LINE_HEIGHT),	//y
			                   _BASEVALUE_LABEL_WIDTH,						//width
			                   _LINE_HEIGHT									//height
			                   ), PlayerCharacter.Instance.GetPrimaryAttribute(count).AdjustedBaseValue.ToString());

			if(GUI.RepeatButton(new Rect(_OFFSET + _STAT_LABEL_WIDTH + _BASEVALUE_LABEL_WIDTH, 	// x
			                       _statStartingPos + (count * _BUTTON_HEIGHT),				// y
			                       _BUTTON_WIDTH,											// width
			                       _BUTTON_HEIGHT											//height
			                       ),"-"))
			{
				if(Time.time - _lastClick > delayTimer){
					if(PlayerCharacter.Instance.GetPrimaryAttribute(count).BaseValue > _MIN_STARTING_ATTRIBUTE_VALUE)
					{
						PlayerCharacter.Instance.GetPrimaryAttribute(count).BaseValue--;
						_pointsLeft++;
						PlayerCharacter.Instance.StatUpdate ();
					}
					_lastClick = Time.time;
				}

			}
			if(GUI.RepeatButton(new Rect(_OFFSET + _STAT_LABEL_WIDTH + _BASEVALUE_LABEL_WIDTH + _BUTTON_WIDTH //x
			                       ,_statStartingPos + (count * _BUTTON_HEIGHT),						//y
			                       _BUTTON_WIDTH,														//width
			                       _BUTTON_HEIGHT														//height
			                       ),"+") == true)
			{
				if(Time.time - _lastClick > delayTimer){
					if(_pointsLeft > 0)
					{
						PlayerCharacter.Instance.GetPrimaryAttribute(count).BaseValue++;
						_pointsLeft--;
						PlayerCharacter.Instance.StatUpdate ();
					}
					_lastClick = Time.time;
				}

			}
		}
	}

	private void DisplayVitals(){
		for(int count = 0; count < Enum.GetValues(typeof(VitalName)).Length; count++)
		{
			GUI.Label(new Rect(_OFFSET ,											//x
			                   _statStartingPos + ((count + 7) * _LINE_HEIGHT),		//y
			                   _STAT_LABEL_WIDTH,									//width
			                   _LINE_HEIGHT											//height
			                   ), ((VitalName)count).ToString());
			GUI.Label(new Rect(_OFFSET + _STAT_LABEL_WIDTH,								//x
			                   _statStartingPos + ((count + 7) * _LINE_HEIGHT),			//y
			                   _BASEVALUE_LABEL_WIDTH,									//width
			                   _LINE_HEIGHT												//height
			                   ),PlayerCharacter.Instance.GetVital(count).AdjustedBaseValue.ToString());
		}

	}
	private void DisplaySkills(){
		for(int count = 0; count < Enum.GetValues(typeof(SkillName)).Length; count++)
		{
			GUI.Label(new Rect(_OFFSET + _STAT_LABEL_WIDTH + _BASEVALUE_LABEL_WIDTH + _BUTTON_WIDTH * 2 + _OFFSET * 2,						//x
			                   _statStartingPos + (count * _LINE_HEIGHT),																	//y
			                   _STAT_LABEL_WIDTH,																							//width
			                   _LINE_HEIGHT																									//height
			                   ), ((SkillName)count).ToString());
			GUI.Label(new Rect(_OFFSET + _STAT_LABEL_WIDTH + _BASEVALUE_LABEL_WIDTH + _BUTTON_WIDTH * 2 + _OFFSET * 2 + _STAT_LABEL_WIDTH,	//x
			                   _statStartingPos + (count * _LINE_HEIGHT),																	//y
			                   _BASEVALUE_LABEL_WIDTH,																						//width
			                   _LINE_HEIGHT																									//height
			                   ), PlayerCharacter.Instance.GetSkill(count).AdjustedBaseValue.ToString());
		}
	}
	private void DisplayCreateLabel (){
		GUI.Button (new Rect (Screen.width / 2 - 50,
		                    _statStartingPos + ((10) * _LINE_HEIGHT),
		                    100,
		                    _LINE_HEIGHT
		), "Creating....", "Button");
	}
	private void DisplayCreateButton (){
		if(GUI.Button(new Rect(Screen.width/2 - 50,
		                    _statStartingPos + ((10)*_LINE_HEIGHT),
							100,
							_LINE_HEIGHT
		                       ),"Next")
		   )
		{

//			GameSettings gsScript = GameObject.FindGameObjectWithTag("Tag_GameSettings").GetComponent<GameSettings>();

			//Change the cur value of the vitals to the max modified of that vital
			UpdateCurVitalValues();
			//gsScript.SaveCharacterData();
			//GameSetting2 = PlayerCharacter.Instance;
			GameSetting2.SaveName( PlayerCharacter.Instance.CName);
			GameSetting2.SaveAttributes( PlayerCharacter.Instance.primaryAttribute);
			GameSetting2.SaveVitals(PlayerCharacter.Instance.vital);
			GameSetting2.SaveSkills(PlayerCharacter.Instance.skill);

			//GameManager.P_name = PlayerCharacter.Instance.CName;
			//Attribute[] temp = GameSetting2.LoadAttributes();

			Application.LoadLevel(GameSetting2.levelNames[3]);
		}
	}
	private void UpdateCurVitalValues(){
		for (int count = 0; count < Enum.GetValues(typeof(VitalName)).Length; count++) {
			//Let's get the vital get the cur val assign it to adjusted base value
			PlayerCharacter.Instance.GetVital(count).CurValue = PlayerCharacter.Instance.GetVital(count).AdjustedBaseValue;
		}
	}
}
