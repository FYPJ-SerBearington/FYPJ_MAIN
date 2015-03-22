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
	public bool _isPlayerHealthBar; //this boolean value tells us if this is the player healthbar or the mob healthbar

	public int _maxBarLength; // this is how large the vital can be if the target is at 100% health
	public int _curBarLength; // this is the current length of the vital bar
	private GUITexture _display; 

	void Awake(){
		
		_display = gameObject.GetComponent<GUITexture> ();
	}
	// Use this for initialization
	void Start () {
		_maxBarLength = (int)_display.pixelInset.width;

		_curBarLength = _maxBarLength;
		_display.pixelInset = CalculatePosition ();

		OnEnable ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//This method is called when the gameobject is enabled
	public void OnEnable(){
		if (_isPlayerHealthBar) {

			Messenger<int,int>.AddListener ("player health update", OnChangeHealthBarSize);
		}
		else {
			ToggleDisplay(false);
			Messenger<int,int>.AddListener ("mob health update", OnChangeHealthBarSize);
			Messenger<bool>.AddListener ("show mob vital bars", ToggleDisplay);
		}

	}
	//This method is called when the gameobject is disabled
	public void OnDisable(){
		if (_isPlayerHealthBar)
			Messenger<int,int>.RemoveListener ("player health update", OnChangeHealthBarSize);
		else {
			Messenger<int,int>.RemoveListener ("mob health update", OnChangeHealthBarSize);
			Messenger<bool>.RemoveListener ("show mob vital bars", ToggleDisplay);
		}
	}

	//This method will calculate the total size of the healthbar in relation to the percentage of health the target has left
	public void OnChangeHealthBarSize(int curHealth, int maxHealth){
		//Debug.Log("We Heard an event: curHealth =" + curHealth);
		_curBarLength = (int)((curHealth / (float)maxHealth) * _maxBarLength);		// this calculates the current bar length based on the players health %

		//_display.pixelInset = new Rect(_display.pixelInset.x,_display.pixelInset.y,_curBarLength, _display.pixelInset.height);
		_display.pixelInset = CalculatePosition();
	}
	//setting the healthbar to the player or mob
	public void SetPlayerHealth(bool b){
		_isPlayerHealthBar = b;
	}

	private Rect CalculatePosition(){
		float yPos = _display.pixelInset.y / 2 - 10;

		if (!_isPlayerHealthBar) {
			float xPos = (_maxBarLength - _curBarLength) - (_maxBarLength/4 + 10) ;
			return new Rect(xPos,yPos,_curBarLength, _display.pixelInset.height);
		}
		else
			return new Rect(_display.pixelInset.x,yPos,_curBarLength, _display.pixelInset.height);

	}

	private void ToggleDisplay(bool show){
		_display.enabled = show;
	}
}
