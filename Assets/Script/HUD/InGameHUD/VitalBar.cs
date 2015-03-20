/// <summary>
/// Vital bar.cs
/// 3/19/2015
/// Gibbie Chairul
/// 
/// This class is responsible for dispaying a vital for the player character or a mob
/// </summary>

using UnityEngine;
using System.Collections;

public class VitalBar : MonoBehaviour {
	private bool _isPlayerHealthBar; //this boolean value tells us if this is the player healthbar or the mob healthbar

	private int _maxBarLength; // this is how large the vital can be if the target is at 100% health
	private int _curBarLength; // this is the current length of the vital bar
	private GUITexture _display; 

	// Use this for initialization
	void Start () {
//		_isPlayerHealthBar = true;

		_display = gameObject.GetComponent<GUITexture> ();

		_maxBarLength = (int)_display.pixelInset.width;

		OnEnable ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//This method is called when the gameobject is enabled
	public void OnEnable(){
		if(_isPlayerHealthBar)
			Messenger<int,int>.AddListener("player health update", OnChangeHealthBarSize);
		else
			Messenger<int,int>.AddListener("mob health update", OnChangeHealthBarSize);
	}
	//This method is called when the gameobject is disabled
	public void OnDisable(){
		if(_isPlayerHealthBar)
			Messenger<int,int>.RemoveListener("player health update", OnChangeHealthBarSize);
		else
			Messenger<int,int>.RemoveListener("player health update", OnChangeHealthBarSize);
	}

	//This method will calculate the total size of the healthbar in relation to the percentage of health the target has left
	public void OnChangeHealthBarSize(int curHealth, int maxHealth){
		//Debug.Log("We Heard an event: curHealth =" + curHealth);
		_curBarLength = (int)((curHealth / (float)maxHealth) * _maxBarLength);		// this calculates the current bar length based on the players health %

		_display.pixelInset = new Rect(_display.pixelInset.x,_display.pixelInset.y,_curBarLength, _display.pixelInset.height);

	}
	//setting the healthbar to the player or mob
	public void SetPlayerHealth(bool b){
		_isPlayerHealthBar = b;
	}
}
