using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("BossDialoguesCollection")]
public class BossDialoguesContainer
{
	[XmlArray("Bosses")]
	[XmlArrayItem("Dialogue")]
	public BossDialogue [] Bosses;

	// Use this for initialization
	void Start()
	{

	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
	//don't need to save don't use
	public void Save(string path)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(BossDialoguesContainer));
		using(FileStream stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	public static BossDialoguesContainer Load(string path)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(BossDialoguesContainer));
		using(FileStream stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as BossDialoguesContainer;
		}
	}
}
