using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Talking : MonoBehaviour 
{
	void Start()
	{
		var monsterCollection = DialoguesContainer.Load(Path.Combine(Application.dataPath, "Script/XML/Boss_D.xml"));
		Debug.Log (monsterCollection.Bosses[0].Name);
		Debug.Log (monsterCollection.Bosses[0].line[0].ToString());
		Debug.Log (monsterCollection.Bosses[0].line[1].ToString());
		Debug.Log (monsterCollection.Bosses[0].line[2].ToString());

	}
	// Update is called once per frame
	void Update ()
	{
	
	}
}
