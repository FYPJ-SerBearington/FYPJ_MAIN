/// <summary>
/// Skill.cs
/// Gibbie Chairul
/// 
/// This class contains all the extra functopns that are needed for a skill
/// </summary>

public class Skill : ModifiedStat {
	private bool _known; //Tell the game knows character this skill

	/// <summary>
	/// Initializes a new instance of the <see cref="Skill"/> class.
	/// </summary>
	//Default Constructor
	public Skill(){
		_known = false;
		ExpToLevel = 25;
		LevelModifer = 1.1f;
	}

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="Skill"/> is known.
	/// </summary>
	/// <value><c>true</c> if known; otherwise, <c>false</c>.</value>
	//Setter and Getter
	public bool Known{
		get{ return _known;}
		set{ _known = value;}
	}
}

/// <summary>
/// This enumeration is just a list of skills the palyer can learn
/// </summary>
public enum SkillName{
	Melee_Offence,
	Melee_Defence,
	Ranged_Offence,
	Ranged_Defence,
	Magic_Offence,
	Magic_Defence
}
