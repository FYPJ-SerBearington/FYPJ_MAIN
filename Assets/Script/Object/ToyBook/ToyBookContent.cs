using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("ToyIndexCollection")]
public class ToyBookContent
{
	[XmlArray("Toys")]
	[XmlArrayItem("Info")]
	public ToyBookIndex [] index;
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
		XmlSerializer serializer = new XmlSerializer(typeof(ToyBookContent));
		using(FileStream stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	public static ToyBookContent Load(string path)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(ToyBookContent));
		using(FileStream stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as ToyBookContent;
		}
	}
}
