using UnityEngine;
using System.Collections;

//Game Master is for instatiating the character
public class GameMaster : MonoBehaviour {
	public GameObject playerCharacter;
	public GameObject gameSettings;

	public Camera mainCamera;

	private GameObject _pc;
	private Vector3 _playerSpawnPointPos; // this is the place in 3d space where I want my player to spawn
	private PlayerCharacter _pcScript; // Getting data the player like attrib etc...
	// Use this for initialization
	void Start () {
		_playerSpawnPointPos = new Vector3 (5.34f,15.0f,-16.89f ); //The default position of our player to spawn
		GameObject go = GameObject.Find (GameSettings.PLAYER_SPAWN_POINT);
		if (go == null) 
		{
			Debug.LogWarning("COULDN'T FIND PLAYER SPAWN POINT. Creating.... Player Spawn Point");

			go = new GameObject(GameSettings.PLAYER_SPAWN_POINT);

			go.transform.position = _playerSpawnPointPos;
			Debug.Log("Player Spawn Point Pos");
		}
		_pc = Instantiate (playerCharacter, go.transform.position,Quaternion.identity) as GameObject; 
		_pcScript = _pc.GetComponent<PlayerCharacter> ();

		LoadCharacter ();
	}

	public void LoadCharacter(){
		GameObject gs = GameObject.FindGameObjectWithTag ("Tag_GameSettings");
		if (gs == null) {
			GameObject gs1 = Instantiate(gameSettings,Vector3.zero,Quaternion.identity) as GameObject;
			gs1.name = "__GameSettings";
			Debug.Log("GAME SETTINGS IS NULL AND CREATING A NEW __GameSettings");
		}
		GameSettings gsScript = GameObject.FindGameObjectWithTag ("Tag_GameSettings").GetComponent<GameSettings> ();

		//Loading our character data
		gsScript.LoadCharacterData ();
	}
}
