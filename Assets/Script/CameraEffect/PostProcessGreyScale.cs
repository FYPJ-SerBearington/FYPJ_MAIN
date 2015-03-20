using UnityEngine;
using System.Collections;

public class PostProcessGreyScale : MonoBehaviour
{
	// Use this for initialization
	public Material mat;

	void Start ()
	{

	}
	
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source,destination,mat);
		//mat is the material which contains the shader
		//we are passing the destination RenderTexture to
	}
	// Update is called once per frame
	void Update()
	{

	}
}
