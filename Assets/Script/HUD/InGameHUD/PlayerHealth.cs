using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int maxHealth = 100;
	public int curHealth = 100;

	private float _healthBarLength;
	// Use this for initialization
	void Start () {
		_healthBarLength = Screen.width / 2;
	}
	
	// Update is called once per frame
	void Update () {
		AdjustCurrentHealth (0);
	}
	void OnGUI(){
		GUI.Box (new Rect (10,10,_healthBarLength, 20), curHealth + "/" + maxHealth);
	}
	//Adjusting the current health. parameter (adj = adjust)
	public void AdjustCurrentHealth(int adj){
		curHealth += adj;

		//Preventing from going below zero
		if (curHealth < 0)
			curHealth = 0;
		//Preventing from going over the maxHealth
		if (curHealth > maxHealth)
			curHealth = maxHealth;
		//Preventing from maxHealh be divided by zero which will cause massive lag.
		if (maxHealth < 1)
			maxHealth = 1;
							//length of the bar    *  By percentage of the health. Typecasting
		_healthBarLength = (Screen.width / 2) * (curHealth / (float)maxHealth);
	}
}
