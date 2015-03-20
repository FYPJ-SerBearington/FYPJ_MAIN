using UnityEngine;
using System.Collections;

public class DestructionScript : MonoBehaviour
{
	public float Timer;
	public float DestructTiming = 3.0f;
	public GameObject hitPar;
	// Use this for initialization
	void Start ()
	{
		Timer = 0;
	}
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name == "wall2")
		{
			Instantiate(hitPar,new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z),Quaternion.identity);
			hitPar.name = "par2";
		}
	}
	// Update is called once per frame
	void Update ()
	{
		Timer += Time.deltaTime;
		if (Timer >= DestructTiming)
		{
			Destroy(this.gameObject);
		}

	}
}
