using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class Boss_DialogueManager : MonoBehaviour 
{
	DialoguesContainer BossDialogue;
	public float letterPause = 1.0f;
	public AudioClip sound;
	string D_Name;
	string D_Content;
	public Text DialougeBox_Name ;
	public Text DialougeBox_Content ;
	public bool triggers = false;
	public bool End = false;
	public int LineCount = 0;

	private Animator D_animation;
	void Awake()
	{
		//load all the xml script
		BossDialogue = DialoguesContainer.Load(Path.Combine(Application.dataPath, "Script/XML/Boss/D_Boss.xml"));
		D_animation = GetComponent<Animator> ();
	}
	void Start()
	{
		//set name to boss name
		D_Name = BossDialogue.Bosses [0].Name.ToString ();
		//set string to first line of boss dialogue
		D_Content = BossDialogue.Bosses [0].line [LineCount].ToString ();
		//set canvas DialougeBox content and name to null first
		DialougeBox_Content.text = "";
		DialougeBox_Name.text = "" ;
	}
	// Update is called once per frame
	void Update ()
	{
		//change boss name and dialogue
	}
	public void startDialogue()
	{
		if(!triggers)
		{
			D_animation.SetTrigger("toggle");
			StartCoroutine(TypeContent()); 
			StartCoroutine(TypeName());
			triggers = true;
		}else
		{
			triggers = false;
		}
	}
	IEnumerator TypeName()
	{
		foreach (char letter in D_Name.ToCharArray())
		{
			DialougeBox_Name.text += letter;
			yield return new WaitForSeconds (letterPause);
		}
		yield return new WaitForSeconds (2.0f);
	}
	IEnumerator TypeContent()
	{
		yield return new WaitForSeconds (2.0f);
		foreach (char letter in D_Content.ToCharArray())
		{
			DialougeBox_Content.text += letter;
			//don't play sound if letter is space
			if(letter != ' ')
			{
				if (sound)
				{
					GetComponent<AudioSource>().PlayOneShot(sound);
				}
			}
			yield return new WaitForSeconds (letterPause);
		}
		yield return new WaitForSeconds (1.0f);
		if(!End)
		{
			//Debug.Log( monsterCollection.Bosses [0].line.Length);
			//if linecount is smaller than boss total lines - 1 (which should be 0 first)
			if(LineCount < (BossDialogue.Bosses [0].line.Length-1) )
			{
				//increase line count by one
				LineCount++;
				//set content to next line string
				D_Content = BossDialogue.Bosses [0].line [LineCount].ToString ();
				DialougeBox_Content.text = "";
				StartCoroutine(TypeContent ());
			}else
			{
				End=true;
				//end of last line
				reset();
				D_animation.SetTrigger("fadeout");
			}
		}
	}
	void reset()
	{
		DialougeBox_Name.text = "" ;
		DialougeBox_Content.text = "";
		LineCount = 0;
		D_Content = BossDialogue.Bosses [0].line [LineCount].ToString ();
		End=false;
		triggers = false;
	}
}
