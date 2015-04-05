using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("DialoguesCollection")]
public class DialoguesContainer
{
	[XmlArray("Bosses")]
	[XmlArrayItem("Dialogue")]
	public Dialogue [] Bosses;

	// Use this for initialization
	void Start()
	{

	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void Save(string path)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(DialoguesContainer));
		using(FileStream stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	public static DialoguesContainer Load(string path)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(DialoguesContainer));
		using(FileStream stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as DialoguesContainer;
		}
	}
}
