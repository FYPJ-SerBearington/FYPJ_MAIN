using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class Boss_DialogueManager : MonoBehaviour 
{
	BossDialoguesContainer BossDialogues;
	public float letterPause = 1.0f;
	public AudioClip sound;
	string D_Name;
	string D_Content;
	public Text DialougeBox_Name;
	public Text DialougeBox_Content;
	public bool triggers = false;
	public bool End = false;
	public int LineCount = 0;
	private Animator D_animation;

	//testing to be replaced with world data
	public bool isAlive = true;
	public int currentstage =0;
	void Awake()
	{
		//load all the xml script
		BossDialogues = BossDialoguesContainer.Load(Path.Combine(Application.dataPath, "Script/XML/Boss/D_Boss.xml"));
		D_animation = GetComponent<Animator> ();
	}
	void Start()
	{
		//set name to boss name
		D_Name = BossDialogues.Bosses[currentstage].Name.ToString ();
		//set string to first line of boss dialogue
		if(isAlive)
		{
			D_Content = BossDialogues.Bosses[currentstage].line [LineCount].ToString ();
		}else
		{
			D_Content = BossDialogues.Bosses[currentstage].line2 [LineCount].ToString ();
		}
		//set canvas DialougeBox content and name to null first
		DialougeBox_Content.text = "";
		DialougeBox_Name.text = D_Name ;
	}
	// Update is called once per frame
	void Update ()
	{
		//change boss name and dialogue
	}
	/// <summary>
	/// Starts the dialogue.
	/// </summary>
	public void startDialogue()
	{
		if(!triggers)
		{
			//reset(currentstage,isAlive);
			D_animation.SetTrigger("toggle");
			//StartCoroutine(TypeName());
			StartCoroutine(TypeContent(currentstage,isAlive)); 
			triggers = true;
		}else
		{
			triggers = false;
		}
	}
//	IEnumerator TypeName()
//	{
//		foreach (char letter in D_Name.ToCharArray())
//		{
//			DialougeBox_Name.text += letter;
//			yield return new WaitForSeconds (letterPause);
//		}
//		yield return new WaitForSeconds (2.0f);
//	}

	/// <summary>
	/// Types the content.
	/// </summary>
	IEnumerator TypeContent(int stage,bool alive)
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
		//delay one sceond
		yield return new WaitForSeconds (1.0f);
		if(!End)
		{
			//Debug.Log( monsterCollection.Bosses [0].line.Length);
			//if linecount is smaller than boss total lines - 1 (which should be 0 first)
			//when boss is alive
			if(alive)
			{
				if(LineCount < (BossDialogues.Bosses[stage].line.Length-1))
				{
					//increase line count by one
					LineCount++;
					D_Content = BossDialogues.Bosses [stage].line [LineCount].ToString ();
					DialougeBox_Content.text = "";
					StartCoroutine(TypeContent(stage,alive));
				}else
				{
					End=true;
					//end of last line
					reset(stage,alive);
					//fade out the whole container
					D_animation.SetTrigger("fadeout");
				}
			}else
			{
				//when boss die
				if(LineCount < (BossDialogues.Bosses[stage].line2.Length-1))
				{
					//increase line count by one
					LineCount++;
					D_Content = BossDialogues.Bosses [stage].line2 [LineCount].ToString ();
					DialougeBox_Content.text = "";
					StartCoroutine(TypeContent(stage,alive));
				}else
				{
					End=true;
					//end of last line
					reset(stage,alive);
					//fade out the whole container
					D_animation.SetTrigger("fadeout");
				}
			}
		}
	}
	/// <summary>
	/// Reset the specified stage and alive.
	/// </summary>
	/// <param name="stage">Stage.</param>
	/// <param name="alive">If set to <c>true</c> alive.</param>
	void reset(int stage,bool alive)
	{
		DialougeBox_Name.text = "" ;
		DialougeBox_Content.text = "";
		LineCount = 0;
		if(alive)
		{
			D_Content = BossDialogues.Bosses [stage].line[LineCount].ToString ();
		}else
		{
			D_Content = BossDialogues.Bosses [stage].line2[LineCount].ToString ();
		}
		End=false;
		triggers = false;
	}
}
