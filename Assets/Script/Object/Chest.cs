using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class Chest : MonoBehaviour {

	private Animation _anim;						//Animations components
	private AudioSource _audio;

	public enum State{
		open,
		close,
		inbetween
	}
	public AudioClip openSound;
	public AudioClip closeSound;				//sound to play when the chest is being closed

	public GameObject particleEffect;			//link to a particle effect for when the chest is open

	public GameObject[] parts;					//the parts of the chest that you want to apply the highlight to it has focus
	private Color[] _defaultColours;			//the default colors of the parts that you are using for the highlight

	public State state;							//our current state

	public float maxDistance = 5;				//the max distance the player can be to open this chest

	private GameObject _player;
	private Transform _myTransform;

	public bool inUse = false;
	// Use this for initialization
	void Start () {
		_myTransform = transform;
		_anim = GetComponent<Animation>();
		_audio = GetComponent<AudioSource> ();
		state = Chest.State.close;

		particleEffect.SetActive (false);
		_defaultColours = new Color[parts.Length];

		if (parts.Length > 0)
			for (int count = 0; count <_defaultColours.Length; count++)
				_defaultColours [count] = parts [count].GetComponent<Renderer> ().material.GetColor ("_Color");
	}

	void Update(){
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
		myGUI.chest = this;

		_player = GameObject.FindGameObjectWithTag("Player");

		inUse = true;
		_anim.Play ("open");

		particleEffect.SetActive (true);
		//_audio.PlayOneShot (openSound);

		yield return new WaitForSeconds (_anim["open"].clip.length);
		state = Chest.State.open;
		Messenger<int>.Broadcast("PopulateChest", 5, MessengerMode.DONT_REQUIRE_LISTENER);
	}
	private IEnumerator Close(){
		_player = null;
		inUse = false;
		_anim.Play ("close");
		particleEffect.SetActive (false);
		//_audio.PlayOneShot (closeSound);
		yield return new WaitForSeconds (_anim["close"].clip.length);
		state = Chest.State.close;
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
					parts [count].GetComponent<Renderer> ().material.SetColor ("_Color", Color.white);

		} 
		else {
			if (parts.Length > 0)
				for (int count = 0; count <_defaultColours.Length; count++)
					parts [count].GetComponent<Renderer> ().material.SetColor ("_Color",_defaultColours[count]);

		}
	}
}
