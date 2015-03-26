using UnityEngine;
using System.Collections;
using System.Threading;
	/* This script will visualise the texel density of your mesh
	 * based on the texel density you give it. It is a very quick
	 * script so it has some flaws. For example, it will only work
	 * with square textures. If I have more time, I will try to
	 * update it.
	 * 
	 * How to use:
	 * Simply drag this object on your mesh, and run Unity.
	 * It will calculate the texel density and put it in your vertex colors
	 * then the script will switch your shader to the vertex color shader :)
	 * Questions/Suggestions: therealosman@gmail.com / www.gameartist.nl
	 * */

public class OzTexelDensity : MonoBehaviour
{
	private float textureWidth;
	public 	float TexelDensity 	= 4;
	private string MainTexName 	= "_MainTex";
	
	void Start()
	{
	}
	
	//I put it here so that if you scale your object up, you can simply disable and enable this script to see changes
	void OnEnable()
	{
		//get texture width ( so only works with square textures sorry :( )
		//then replace our current shader with the vertex color ones
		//and finaly bake our texelDensity in our vertex colors
		if(GetComponent<Renderer>().material.GetTexture(MainTexName) != null)
		{
			textureWidth =	GetComponent<Renderer>().material.GetTexture(MainTexName).width;
		}else
		{
			Debug.LogError("Could not find the texture");
		}
		GetComponent<Renderer>().material.shader = Shader.Find("Oz/OzVColor");
		RenderTexelDensity();
	}

	void Update () 
	{	
	}
	
	void RenderTexelDensity()
	{
		
		Mesh mesh 				= GetComponent<MeshFilter>().mesh;
	    Vector3[] vertices 		= mesh.vertices;
	    Vector3[] normals 		= mesh.normals;
		int[] triangles 		= mesh.triangles;
		Color[] colors 			= new Color[mesh.uv.Length];
		Vector2[] uvs 			= mesh.uv;
		Color c 				= new Color();
	
		//go through each triangle
		for(int i =0;i<triangles.Length-1;i+=3)
		{
			//get our verts
			int vertA 	= triangles[i];
			int vertB	= triangles[i+1];
			int vertC	= triangles[i+2];

			//calculate the length of each edge of this tri
			float distanceA = Vector3.Distance(transform.TransformPoint(vertices[vertA]),transform.TransformPoint(vertices[vertB]));
			float distanceB = Vector3.Distance(transform.TransformPoint(vertices[vertB]),transform.TransformPoint(vertices[vertC]));
			float distanceC = Vector3.Distance(transform.TransformPoint(vertices[vertC]),transform.TransformPoint(vertices[vertA]));
			
			//do the same for our uv tri
			float uvDistanceA = Vector2.Distance(uvs[vertA],uvs[vertB]);
			float uvDistanceB = Vector2.Distance(uvs[vertB],uvs[vertC]);
			float uvDistanceC = Vector2.Distance(uvs[vertC],uvs[vertA]);
			
				//calc the texel density, if you know a better or more correct way, please let me know :)
			float texelDensity = (((distanceA + distanceB + distanceC) / (uvDistanceA + uvDistanceB + uvDistanceC) )* TexelDensity)/textureWidth;
				//some fancy coloring
			if(texelDensity < 1)
			{
				c = Color.Lerp(Color.red,Color.green,texelDensity);
			}
			else
			{
				c = Color.Lerp(Color.blue,Color.green,1.0f/texelDensity);

			}
			//assign our new verts back to the mesh
			colors[vertA] = c;
			colors[vertB] = c;
			colors[vertC] = c;	
		}
			//same for our colors
     	mesh.colors = colors;
		
	}
	
}
