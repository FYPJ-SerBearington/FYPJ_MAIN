using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[AddComponentMenu("Game/Object/Chest")]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class Chest : MonoBehaviour {

	private Animation _anim;						//Animations components
//	private AudioSource _audio;

	public enum State{
		open,
		close,
		inbetween
	}
	public string openAnimName;
	public string closeAnimName;
	//public AudioClip openSound;
	//public AudioClip closeSound;				//sound to play when the chest is being closed

	public GameObject particleEffect;			//link to a particle effect for when the chest is open

	public GameObject[] parts;					//the parts of the chest that you want to apply the highlight to it has focus
	private Color[] _defaultColours;			//the default colors of the parts that you are using for the highlight

	public State state;							//our current state

	public float maxDistance = 5;				//the max distance the player can be to open this chest

	private GameObject _player;
	private Transform _myTransform;

	public bool inUse = false;

	public List<Item> loot = new List<Item>();

	public static float defaultLifeTime = 10;
	private float _lifeTimer = 0;

	private bool _used = false; 		//track if the chest has been used or not
	// Use this for initialization
	void Start () {
		_myTransform = transform;
		_anim = GetComponent<Animation>();
//		_audio = GetComponent<AudioSource> ();
		state = Chest.State.close;

		//particleEffect.SetActive (false);
		_defaultColours = new Color[parts.Length];

		if (parts.Length > 0)
			for (int count = 0; count <_defaultColours.Length; count++)
				_defaultColours [count] = parts [count].GetComponent<Renderer> ().material.GetColor ("_Color");
	}

	void Update(){
		_lifeTimer += Time.deltaTime;
		//will destory chest if not loot within life time.
		if(_lifeTimer > defaultLifeTime && state == Chest.State.close){
			DestroyChest();
		}
		if(!inUse)
			return;

		if(_player == null)
			return;

		if(Vector3.Distance(transform.position, _player.transform.position) > maxDistance){
			myGUI.chest.ForceClose();
		}
	}
	public void OnMouseEnter(){
	//	Debug.Log ("On mouse enter");
		HighLight (true);
	}
	public void OnMouseExit(){
	//	Debug.Log ("On mouse Exit");
		HighLight (false);
	}
	public void OnMouseUp(){
	//	Debug.Log ("On mouse Up");

		GameObject go = GameObject.FindGameObjectWithTag ("Player");

		if (go == null)
			return;

		if (Vector3.Distance (transform.position, go.transform.position) > maxDistance && !inUse) 
			return;

		switch (state) {
		case State.open:
	//		Debug.Log ("open");
			state = Chest.State.inbetween;
	//		StartCoroutine ("Close");
			ForceClose();
			break;
		case State.close:
			if(myGUI.chest != null){
				myGUI.chest.ForceClose();
			}
			state = Chest.State.inbetween;
	//		Debug.Log ("Close");
			StartCoroutine ("Open");
			break;

		}

	}

	private IEnumerator Open(){
		//Set this script to be the one that is holding the items
		myGUI.chest = this;

		//find the player so we can track his distance after openning the chest
		_player = GameObject.FindGameObjectWithTag("Player");

		//make this chest as being in use
		inUse = true;

		//play the open anumation
		_anim.Play (openAnimName);

		//quickly turn on the particle animation
		//particleEffect.SetActive (true);

		//play the audio
		//_audio.PlayOneShot (openSound);
		if(!_used)
			PopulateChest (5);
		//wait until the chest is done opening
		yield return new WaitForSeconds (_anim["open"].clip.length);

		//change the chest state to open
		state = Chest.State.open;

		//send a message to the GUI to create 5 items and display them in the loot window
		Messenger.Broadcast("DisplayLoot");
	}

	private void PopulateChest(int x){
		for (int count = 0; count < x; count++) {
			loot.Add (ItemGenerator.CreateItem());

		}

		_used = true;

	}

	private IEnumerator Close(){
		_player = null;
		inUse = false;
		_anim.Play (closeAnimName);

		if(particleEffect != null)
			particleEffect.SetActive(false);
		//particleEffect.SetActive (false);
		//_audio.PlayOneShot (closeSound);

		//	float tempTimer = _anim[closeAnimName].length;
		//if(closeSound != null)
//		if(closeSound.length > tempTimer)
//			tempTimer = closeSound.length;
		yield return new WaitForSeconds (_anim[closeAnimName].length);
		state = Chest.State.close;

		if (loot.Count == 0)
			DestroyChest();
	}
	private void DestroyChest(){
		loot = null;
		Destroy(gameObject);
	}
	public void ForceClose(){
		Messenger.Broadcast("CloseChest");

		StopCoroutine("Open");
		StartCoroutine("Close");
	}
	private void HighLight(bool glow){
		if (glow) {
			if (parts.Length > 0)
				for (int count = 0; count <_defaultColours.Length; count++)
					for(int matCount = 0; matCount < parts [count].GetComponent<Renderer> ().materials.Length; matCount++)
						parts [count].GetComponent<Renderer> ().materials[matCount].SetColor ("_Color", Color.white);

		} 
		else {
			if (parts.Length > 0)
				for (int count = 0; count <_defaultColours.Length; count++)
					for(int matCount = 0; matCount < parts [count].GetComponent<Renderer> ().materials.Length; matCount++)
						parts [count].GetComponent<Renderer> ().materials[matCount].SetColor ("_Color", _defaultColours[count]);

		}
	}
}
