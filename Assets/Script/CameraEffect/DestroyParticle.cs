using UnityEngine;
using System.Collections;

public class DestroyParticle : MonoBehaviour
{

	public float Timer;
	public float DestructTiming = 3.0f;
	// Use this for initialization
	void Start ()
	{
		Timer = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Timer += Time.deltaTime;
		if (Timer >= DestructTiming)
		{
			Destroy(this.gameObject);
			//Timer = 0;
		}
		
	}
}
