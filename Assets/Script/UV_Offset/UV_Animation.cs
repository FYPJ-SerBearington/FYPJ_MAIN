using UnityEngine;
using System.Collections;

public class UV_Animation : MonoBehaviour
{
	public float scrollspeed = 0.5f;
	public float offsetX = 0;
	public float offsetY ;
	private Renderer rend;
	// Use this for initialization
	void Start ()
	{
		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//offsetX += (Time.deltaTime * scrollspeed) / 10.0f;
		offsetY += (Time.deltaTime * scrollspeed) / 10.0f;
		rend.material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
//		if(offsetY>1)
//		{
//			offsetY=0;
//		}
	}
}
