using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MobGenerator : MonoBehaviour {
	public enum State{
		Idle,
		Initialize,
		Setup,
		SpawnMob
	}
	public GameObject[] mobPrefabs; //An array to hold all of the prefabs of mobs we want to spawn
	public GameObject[] spawnPoints; //This array will hold a reference to all of the spawnpoints of the scene

	public State _state; //this our local variable that holds our current state
	void Awake(){
		_state = MobGenerator.State.Initialize;
	}
	// Use this for initialization
	IEnumerator Start () {
		while (true) {
			switch(_state){
			case State.Initialize:
				_Initialize();
				break;
			case State.Setup:
				_Setup();
				break;
			case State.SpawnMob:
				_SpawnMob();
				break;

			}
			yield return 0;//stop every frame let other run
		}
	}


	//make sure that everything is initialized before we go on to the next step
	private void _Initialize(){
		Debug.Log ("Init");

		if (!CheckForMobPrefabs())
			return;

		if(!CheckForSpawnPoints())
			return;

		_state = MobGenerator.State.Setup;
	}
	//make sure taht everthing is set up before we continue
	private void _Setup(){
		Debug.Log ("SetUp");
		_state = MobGenerator.State.SpawnMob;
	}
	//spawn a mob if we have an open spawn point
	private void _SpawnMob(){
		Debug.Log ("spawnmob");

		GameObject[] gos = AvailableSpawnPoints ();

		for (int count = 0; count < gos.Length; count++) {
			GameObject go = Instantiate(mobPrefabs[Random.Range(0, mobPrefabs.Length)],
			                            gos[count].transform.position,
			                            Quaternion.identity
			                            ) as GameObject;

			go.transform.parent = gos[count].transform;
		}
		_state = MobGenerator.State.Idle;
	}

	//check to see that we have at least one mob prefab to spawn
	private bool CheckForMobPrefabs(){
		if (mobPrefabs.Length > 0)
			return true;
		else
			return false;
	}

	//check to see if we have at least one spawnpoint to spawn mobs at
	private bool CheckForSpawnPoints(){
		if (spawnPoints.Length > 0)
			return true;
		else 
			return false;
	}

	//generate a list of available spawnpoints that do not have any mobs childed to it
	private GameObject[] AvailableSpawnPoints(){
		List<GameObject> gos = new List<GameObject> ();

		//iterate through our spawn points and add the ones that do nto have a mob under it to the list
		for (int count =0; count < spawnPoints.Length; count++) {
			//check how many child it has
			if(spawnPoints[count].transform.childCount == 0)
			{
				Debug.Log("spawn point available");
				gos.Add(spawnPoints[count]);
			}
		}
		//convert our list to array
		return gos.ToArray();
	}
}	
