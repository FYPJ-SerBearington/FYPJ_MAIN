using UnityEngine;
using System.Collections;

public class ToyBookController : MonoBehaviour
{
	public bool Activated;
	public int Page;
	Animator animator;
	// Use this for initialization
	void Start ()
	{
		Page = 0;
		Activated = false;
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update ()
	{
		if(Activated)
		{
			animator.SetBool("Open",true);
			if(Input.GetMouseButtonDown(0))
			{
				Page++;
			}
		}else
		{
			animator.SetBool("Open",false);
		}
	}
}
