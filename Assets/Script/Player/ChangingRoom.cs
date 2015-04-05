using UnityEngine;
using System.Collections;

public class ChangingRoom : MonoBehaviour {

	private int _charModelIndex = 0;

	private CharacterAssest ca;

	void Awake(){
		DontDestroyOnLoad (this);
	}
	// Use this for initialization
	void Start () {
		ca = GameObject.Find("Character Assest Manager").GetComponent<CharacterAssest>();
		InstantiateCharacterModel();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	private void InstantiateCharacterModel(){
		GameObject model = Instantiate(ca.characterMesh[_charModelIndex],transform.position,Quaternion.identity) as GameObject;

		model.transform.parent = transform;
		model.transform.rotation = transform.rotation;
	}
}
