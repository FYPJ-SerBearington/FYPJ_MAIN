using System.Xml;
using System.Xml.Serialization;

public class Dialogue
{
	[XmlAttribute("by")]
	public string Name;
	//what it say
	[XmlArray("Sentence")]
	[XmlArrayItem("line")]
	public string [] line;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
