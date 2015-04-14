using System.Xml;
using System.Xml.Serialization;

public class BossDialogue
{
	[XmlAttribute("by")]
	public string Name;
	//what it say
	[XmlArray("AliveSentences")]
	[XmlArrayItem("a_line")]
	public string [] line;

	[XmlArray("DeadSentences")]
	[XmlArrayItem("d_line")]
	public string [] line2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
