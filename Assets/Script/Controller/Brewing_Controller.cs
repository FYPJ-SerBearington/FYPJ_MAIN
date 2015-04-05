using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Brewing_Controller : MonoBehaviour
{
	public Text feedback;
	public int chances;
	public int result;
	public bool Active;
	public GameObject ball;
	public bool canspawn;
	public bool start;
	// Use this for initialization
	void Start ()
	{
		chances = 3;
		result = 0;
		Active = true;
		canspawn = true;
		start = true;
	}
	void showResult()
	{
		switch(result)
		{
			case 0:
			{
				//show bad
				Debug.Log("BAD");
				feedback.text = "BAD";
			}break;
			case 1:
			{
				//show good
				Debug.Log("GOOD");
				feedback.text = "GOOD";
			}break;
			case 2:
			{
				//show great
				Debug.Log("GREAT");
				feedback.text = "GREAT";
			}break;
			case 3:
			{
				//show excellent
				Debug.Log("EXCELLENT");
				feedback.text = "EXCELLENT";
			}break;
		}
		start = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if(start)
		{
			if(Active)
			{
				if(canspawn)
				{
					if(chances <1 || result>2)
					{
						Active = false;
						//chances = 3;
					}else
					{
						Instantiate(ball,new Vector3(GameObject.Find("NodeSpawner").transform.position.x,GameObject.Find("NodeSpawner").transform.position.y,GameObject.Find("NodeSpawner").transform.position.z),Quaternion.identity);
						canspawn = false;
					}
				}
			}else
			{
				showResult();
			}
		}
	}
}
