using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;


public class ToyBookManager : MonoBehaviour
{
	ToyBookContent bookindex;
	public Text Index_Name;
	public Text Index_Content;
	public int page = 0;
	void Awake()
	{
		//load all the xml script
		bookindex = ToyBookContent.Load(Path.Combine(Application.dataPath, "Script/XML/ToyIndex.xml"));
		//D_animation = GetComponent<Animator> ();
	}
	// Use this for initialization
	void Start ()
	{
	//	Debug.Log (bookindex.index [page].Name.ToString ());
	//	Debug.Log (bookindex.index [page].Health);
	//	Debug.Log (bookindex.index [page].Description.ToString());
		ShowContent ();
	}
	void ShowContent()
	{
		Index_Name.text = bookindex.index [page].Name.ToString ();
		Index_Content.text = (bookindex.index [page].Description.ToString());
	}
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(page<bookindex.index.Length-1)
			{
				page+=1;
				ShowContent ();
			}
		}
		if(Input.GetMouseButtonDown(1))
		{
			if(page>0)
			{
				page-=1;
				ShowContent ();
			}
		}
	}
}
