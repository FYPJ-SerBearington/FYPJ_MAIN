using UnityEngine;
using System.Collections;

public class Menu_ButtonScript : MonoBehaviour
{

	Ray ray;
	RaycastHit hit;
	Animator animator;
	bool rotateWall2Left = false;
	bool rotateWall2Right = false;
	// Use this for initialization
	void Start()
	{
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update ()
	{
		if(rotateWall2Left == true)
		{
			GameObject.Find ("Wall_2").transform.Rotate(0,50*Time.deltaTime,0);
			if(GameObject.Find ("Wall_2").transform.eulerAngles.y >=90)
			{
				rotateWall2Left = false;
			}
		}
		if(rotateWall2Right == true)
		{
			GameObject.Find ("Wall_2").transform.Rotate(0,-50*Time.deltaTime,0);
			if(GameObject.Find ("Wall_2").transform.eulerAngles.y <=1)
			{
				rotateWall2Right = false;
			}
		}
		//ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Input.GetMouseButtonDown(0))
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit))
			{
				//print (hit.collider.name);
				if(hit.transform.gameObject.name == "PlayBtn")
				{
					Application.LoadLevel(1);
				}
				if(hit.transform.gameObject.name == "CreditBtn")
				{
					//Debug.Log ("credits");
					animator.SetBool("MenuToCredit",true);
				}
				//Credits to Main menu
				if(hit.transform.gameObject.name == "CtoMainBtn")
				{
					animator.SetBool("MenuToCredit",false);
				}
				//Credits to page 2
				if(hit.transform.gameObject.name == "CtoC2Btn")
				{
					rotateWall2Left = true;
				}
				//Credits page2 to 1
				if(hit.transform.gameObject.name == "C2toCBtn")
				{
					rotateWall2Right = true;
				}
				if(hit.transform.gameObject.name == "QuitBtn")
				{
					ExitGame();
				}
			}
		}
	}

	public void NextLevelButton(int index)
	{
		Application.LoadLevel(index);
	}
	
	public void NextLevelButton(string levelName)
	{
		Application.LoadLevel(levelName);
	}

	public void ExitGame()
	{
		//Debug.Log ("Quit game");
		Application.Quit();
	}
}
