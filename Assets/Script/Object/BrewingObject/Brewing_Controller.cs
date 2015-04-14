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
	public GameObject potion,potion2,potion3;

	public bool canspawn;
	public bool start = false;
	// Use this for initialization


	void Start ()
	{
		chances = 3;
		result = 0;
		Active = true;
		canspawn = true;
	}
	void showResult()
	{
		switch(result)
		{
			case 0:
			{
				//show bad
				feedback.text = "BAD";
				//spawn noting
			}break;
			case 1:
			{
				//show good
				feedback.text = "GOOD";
				Instantiate(potion,new Vector3(GameObject.Find("pot").transform.position.x,GameObject.Find("pot").transform.position.y,GameObject.Find("pot").transform.position.z),Quaternion.identity);
			}break;
			case 2:
			{
				//show great
				feedback.text = "GREAT";
				Instantiate(potion2,new Vector3(GameObject.Find("pot").transform.position.x,GameObject.Find("pot").transform.position.y,GameObject.Find("pot").transform.position.z),Quaternion.identity);
			}break;
			case 3:
			{
				//show excellent
				feedback.text = "EXCELLENT";
				Instantiate(potion3,new Vector3(GameObject.Find("pot").transform.position.x,GameObject.Find("pot").transform.position.y,GameObject.Find("pot").transform.position.z),Quaternion.identity);
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
					if(chances < 1 || result > 2)
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
				reset();
			}
		}
	}

	void reset()
	{
		chances = 3;
		result = 0;
		Active = true;
		canspawn = true;
		feedback.text = "";
	}
}
