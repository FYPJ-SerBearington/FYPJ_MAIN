using UnityEngine;
using System.Collections;

public class Object_Platform : MonoBehaviour
{
	//public bool checker;
	enum state
	{
		IN = 1,
		OUT,
		REST
	}
	private state checker;
	public int Cstate;
	public float t;
	public float min,max;
	public float movevalue = 1;
	// Use this for initialization
	void Start ()
	{
		min = this.transform.position.x;
		max = this.transform.position.x + movevalue;
		t = 0;
		checker = 0;
		Cstate = (int)checker;
	}
	// Update is called once per frame
	void Update ()
	{
		switch(Cstate)
		{
			case (int)state.IN:
			{
				t+=Time.deltaTime;
				if(t>1.0f)
				{
					this.transform.Translate(Time.deltaTime,0,0);
				}
				if(this.transform.position.x >= max)
				{
					t = 0;
					this.transform.position.Set (max,this.transform.position.y,this.transform.position.z);
					Cstate = (int)state.REST;
				}
			}break;
			case (int)state.OUT:
			{
				t+=Time.deltaTime;
				if(t>1.0f)
				{
					this.transform.Translate(-Time.deltaTime,0,0);
				}
				if(this.transform.position.x <=min)
				{
					t=0;
					this.transform.position.Set (min,this.transform.position.y,this.transform.position.z);
				}
			}break;
			case (int)state.REST:
			{
				t+=Time.deltaTime;
				if(t>1.0f)
				{
					Cstate = (int)state.OUT;
					t = 0;
				}
			}break;
		}

	}
}
