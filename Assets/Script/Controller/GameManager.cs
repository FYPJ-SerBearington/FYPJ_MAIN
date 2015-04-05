using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager:MonoBehaviour
{
	public static int currentStage;
	public static int healthCount;
	public static int armorType;
	public static int legoCount;
	public static bool bossIsAlive;

	//Player data want to save
	public static string P_name;
	// Use this for initialization
	void Start ()
	{

	}
	// Update is called once per frame
	void Update ()
	{
		//if (Input.GetKeyDown (KeyCode.P))
		//{
		//	SaveLoad.Save();
		//}
	}

	//it's static so we can call it from anywhere
	public static void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create (Application.persistentDataPath + "/savedData.dt"); //you can call it anything you want
		Debug.Log(Application.persistentDataPath + "/savedData.dt");
		GameData data = new GameData();
		data.currentStage = currentStage;

		//Player stuff
		data.P_name = P_name;
		bf.Serialize(file, data);
		file.Close();
	}   
	
	public static void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/savedData.dt"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedData.dt", FileMode.Open);
			GameData data = (GameData)bf.Deserialize(file);
			file.Close();
			
			currentStage = data.currentStage;

			P_name = data.P_name;
		}
	}
}

[System.Serializable]
public class GameData
{
	public int currentStage;
	public int healthCount;
	public int armorType;
	public int legoCount;
	public bool bossIsAlive;

	public string P_name;
}