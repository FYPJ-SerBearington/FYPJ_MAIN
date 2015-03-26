using UnityEngine;
using System.Collections;

public class Node_Behaviour : MonoBehaviour
{
	public bool hit;
	public float OrbitSpeed = 3.0f;
	private Transform OriginPoint;
	// Use this for initialization
	void Start ()
	{
		hit = false;
		OriginPoint = GameObject.Find ("pot").transform;
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Tag_Brew_NodeHolder")
		{
			hit = true;
			other.GetComponent<Renderer>().material.color = Color.red;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Tag_Brew_NodeHolder")
		{
			hit = false;
			other.GetComponent<Renderer>().material.color = Color.white;
		}
	}
	public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle)
	{
		return angle * ( point - pivot) + pivot;
	}
	void Orbit()
	{
		transform.position = RotatePointAroundPivot(transform.position, OriginPoint.position,Quaternion.Euler(0, OrbitSpeed * Time.deltaTime, 0));
	}	
	
	void LateUpdate ()
	{
		Orbit();
	}
	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(hit)
			{
				//add one to result
				GameObject.Find("pot").GetComponent<Brewing_Controller>().result +=1;
				GameObject.Find("pot").GetComponent<Brewing_Controller>().chances -=1;
				GameObject.Find("pot").GetComponent<Brewing_Controller>().canspawn =true;
				Destroy(this.gameObject);
			}else
			{
				GameObject.Find("pot").GetComponent<Brewing_Controller>().chances -=1;
				GameObject.Find("pot").GetComponent<Brewing_Controller>().canspawn =true;
				Destroy(this.gameObject);

			}
		}
	}
}
