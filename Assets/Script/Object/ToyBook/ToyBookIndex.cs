using System.Xml;
using System.Xml.Serialization;

public class ToyBookIndex
{
	[XmlAttribute("name")]
	public string Name;

	public int Health;
	public bool unlocked;
	public string Description;
	// Use this for initialization
	void Start ()
	{

	}
	// Update is called once per frame
	void Update ()
	{

	}
}
