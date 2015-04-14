using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour
{
	public float frequencyMin  = 1.0f;
	public float frequencyMax  = 2.0f;
	public float magnitude = 0.0025f;
	private float randomInterval;
	public bool inwater;
	// Use this for initialization
	void Start ()
	{
		if(inwater == true)
		{
			randomInterval = Random.Range(frequencyMin, frequencyMax);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if(inwater == true)
		{
			this.transform.position = new Vector3 (this.transform.position.x,this.transform.position.y+ (Mathf.Cos (Time.time * randomInterval) * magnitude),this.transform.position.z);
			this.transform.eulerAngles = new Vector3 (this.transform.eulerAngles.x+ (Mathf.Cos(Time.time * randomInterval) * 2),this.transform.eulerAngles.y,this.transform.eulerAngles.z);
		}

	}
}
