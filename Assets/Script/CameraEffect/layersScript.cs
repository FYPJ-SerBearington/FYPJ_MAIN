using UnityEngine;
using System.Collections;

public class layersScript : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		//8 = triggers 9 = player
		Physics.IgnoreLayerCollision(8,9);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown (KeyCode.N))
		{
			Debug.Log(GameManager.currentStage);
		}
		if(Input.GetKeyDown (KeyCode.B))
		{
			GameManager.currentStage = 100;
			Debug.Log(GameManager.currentStage);
		}
		if (Input.GetKeyDown (KeyCode.I))
		{
			GameManager.Save();
		}
		if (Input.GetKeyDown (KeyCode.O))
		{
			GameManager.Load();
		}
	}
}
