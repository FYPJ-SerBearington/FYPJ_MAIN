using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckData : MonoBehaviour
{
	public Image test;
	public GameObject test2char;
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		test.fillAmount = (float)test2char.GetComponent<BearController> ().health / 100;
	}
}
