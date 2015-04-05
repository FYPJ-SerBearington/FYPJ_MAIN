//Check to see if we have same saved data in the playerprefs
//check the version of hte saved data
//if the saved version of the data is not the current version
//do something
//else if the saved version is the current version
//check to see if they have a character saved - check for a character name
//if they do not have a character saved , load the character generation
// else if they do have a character saved
//if they want to load the character, load character and go to level 1
//if they want to delete the character, delete the character and go to the character generation scene

using UnityEngine;
using System.Collections;

public class LoadSave : MonoBehaviour {
//	public const float VERSION = 0.1f;
	public bool clearPrefs = false;
	private string _levelToLoad = "";

	private string _characterGeneration = GameSetting2.levelNames[2]; // Name of scene
	private string _firstLevel = GameSetting2.levelNames[3]; // name of scene

	private bool _hasCharacter = false;
	private float _percentLoaded = 0;
	private bool _displayOptions = true;
//	public Texture2D emptyProgressBar; // Set this in inspector.
//	public Texture2D fullProgressBar; // Set this in inspector.
	
//	private AsyncOperation async = null; // When assigned, load is in progress.

	// Use this for initialization
	void Start () {
		if (clearPrefs) {
			Debug.Log("DeleteAll Start Func");
			PlayerPrefs.DeleteAll ();
		}

		if (PlayerPrefs.HasKey ( GameSetting2.VERSION_KEY_NAME )) {
			Debug.Log ("There is a ver key");
			if (GameSetting2.LoadGameVersion() != GameSetting2.VERSION_NUMBER) {
				Debug.Log ("Saved Version is not the same as current version");

			} 
			else {
				Debug.Log ("Saved version is the same as the current version");
				if (PlayerPrefs.HasKey ("Player Name")) {
					Debug.Log ("There is a Player Name Tag");
					if (PlayerPrefs.GetString ("Player Name") == "") {
						Debug.Log ("The Player Name key does not have anything in it.");
						PlayerPrefs.DeleteAll();
						_levelToLoad = _characterGeneration;

					}
					else {
						Debug.Log ("The Player name key has a value");
						_hasCharacter = true;
						//_levelToLoad = _firstLevel;
					}
				} 
				else {
					Debug.Log ("There is no Player Name key");
					PlayerPrefs.DeleteAll();
					GameSetting2.SaveGameVersion();
					_levelToLoad = _characterGeneration;
					Debug.Log("load Character generation");

				}
			}
		} 
		else {
			Debug.Log("There is no ver key");
			PlayerPrefs.DeleteAll();
			GameSetting2.SaveGameVersion();
			_levelToLoad = _characterGeneration;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (_levelToLoad == "")
			return;

		if(Application.GetStreamProgressForLevel(_levelToLoad) == 1)
		{
			_percentLoaded = 1;
			if(Application.CanStreamedLevelBeLoaded(_levelToLoad)){
				Application.LoadLevel(_levelToLoad);
				Debug.Log("Level is ready");
			}
		}
		else
		//StartCoroutine( "LoadALevel", _levelToLoad);

		_percentLoaded = Application.GetStreamProgressForLevel(_levelToLoad);
		
	}

//	private IEnumerator LoadALevel(string levelName) {
//		async = Application.LoadLevelAsync(levelName);
////		Debug.Log(async);
//		Debug.Log(levelName);
////		Debug.Log("Level is loaddeddd");
//		yield return async;
//
//	}
//	
	void OnGUI() {
//		if (async != null) {
//			Debug.Log("Level is ready");
////			GUI.DrawTexture(new Rect(0, 0, 100, 50), emptyProgressBar);
////			GUI.DrawTexture(new Rect(0, 0, 100 * async.progress, 50), fullProgressBar);
//		}
		if(_displayOptions == true){
			if(_hasCharacter){
				if(GUI.Button(new Rect(10,10, 110,25 ),"Load Character")){
					_levelToLoad = _firstLevel;
					_displayOptions = false;
				}
				if(GUI.Button(new Rect(10,40, 110,25 ),"Delete Character")){
					PlayerPrefs.DeleteAll();
					GameSetting2.SaveGameVersion();
					_levelToLoad = _characterGeneration;
					_displayOptions = false;
				}
			}
		}

		if(_levelToLoad == "")
			return;
		GUI.Label(new Rect(Screen.width/2  - 50 , Screen.height - 45,100,25), (_percentLoaded * 100) + "%" );
		GUI.Box(new Rect(0, Screen.height - 20, Screen.width * _percentLoaded,15), ""  );
	}
}
