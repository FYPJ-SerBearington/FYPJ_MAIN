/// <summary>
/// Attribute.cs
/// 3/18/2015
/// Gibbie Chairul
/// 
/// This is the class for all the character attributes in-game
/// </summary>
//Inherit from base stats

public class Attribute : BaseStat {
	new public const int STARTING_EXP_COST = 50;  // this is the starting cost for all of our attributes

	/// <summary>
	/// Initializes a new instance of the <see cref="Attribute"/> class.
	/// </summary>
	public Attribute()
	{

		ExpToLevel = 50;
		LevelModifer = 1.05f;
	}


}

/// <summary>
/// This is a list of all the attributes that we will have in game for our characters
/// </summary>
//Define numeration
public enum AttributeName {
	Strength,
	Agility,
	Vitality,
	Intelligence
}
