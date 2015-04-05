/// <summary>
/// Game master.cs
/// 3/16/2015
/// Gibbie Chairul
/// 
/// This script will be used to run the first level of our game
/// Create a game object called __GameMaster
/// </summary>

using UnityEngine;
using System.Collections;

[AddComponentMenu("Managers/Game Master")]
//Game Master is for instatiating the character
public class GameMaster : MonoBehaviour {
//	public GameObject playerCharacter;
//	public GameObject gameSettings;

	public Camera mainCamera;

	private GameObject _pc;
	public Vector3 _playerSpawnPointPos; // this is the place in 3d space where I want my player to spawn
//	private PlayerCharacter _pcScript; // Getting data the player like attrib etc...
	// Use this for initialization
	void Start () {
		// I have commented out the following line as I am using a publick variable and the inspector to place the character
	//	_playerSpawnPointPos = new Vector3 (5.34f,15.0f,-16.89f ); //The default position of our player to spawn
//		GameObject go = GameObject.Find (GameSetting2.PLAYER_SPAWN_POINT);
//		if (go == null) 
//		{
//			Debug.LogWarning("COULDN'T FIND PLAYER SPAWN POINT. Creating.... Player Spawn Point");
//
//			go = new GameObject(GameSetting2.PLAYER_SPAWN_POINT);
//
//			go.transform.position = _playerSpawnPointPos;
//			Debug.Log("Player Spawn Point Pos");
//		}
//		_pc = Instantiate (playerCharacter, go.transform.position,Quaternion.identity) as GameObject; 
//		_pcScript = _pc.GetComponent<PlayerCharacter> ();

//		LoadCharacter ();
	}

//	public void LoadCharacter(){
//		GameObject gs = GameObject.FindGameObjectWithTag ("Tag_GameSettings");
//		if (gs == null) {
//			GameObject gs1 = Instantiate(gameSettings,Vector3.zero,Quaternion.identity) as GameObject;
//			gs1.name = "__GameSettings";
//			Debug.Log("GAME SETTINGS IS NULL AND CREATING A NEW __GameSettings");
//		}
//		GameSettings gsScript = GameObject.FindGameObjectWithTag ("Tag_GameSettings").GetComponent<GameSettings> ();
//
//		//Loading our character data
//		gsScript.LoadCharacterData ();
//	}
}
