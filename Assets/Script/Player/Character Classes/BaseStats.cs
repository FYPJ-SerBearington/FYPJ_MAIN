/// <summary>
/// Base stat.cs
/// Gibbie Chairul
/// 3/18/2015
/// 
/// This is the case class for a stats in game
/// </summary>
//This class not derive from monobehaviour
public class BaseStat{
	public const int STARTING_EXP_COST = 100;  // publicly accessible value for all base stats to start att

	private int _baseValue;		//the base value of this stat
	private int _buffValue;		//the amount of the buff to this stat
	private int _expToLevel;	// experience needed for next point value
	private float _levelModifer; //the modifier applied to the exp needed to raise the skill

	/// <summary>
	/// Initializes a new instance of the <see cref="BaseStat"/> class.
	/// </summary>
	//Default constructor
	public BaseStat(){
		UnityEngine.Debug.Log ("Base Stat Created");
		_baseValue = 0;
		_buffValue = 0;
		_levelModifer = 1.1f;
		_expToLevel = STARTING_EXP_COST;
	}

#region Basic Setters and Getters
	/// <summary>
	/// Gets or sets the base value.
	/// </summary>
	/// <value>_baseValue</value>
	public int BaseValue{
		get{ return _baseValue; }
		set{ _baseValue = value;}
	}

	/// <summary>
	/// Gets or sets the buff value.
	/// </summary>
	/// <value>_buffValue</value>
	public int BuffValue{
		get{ return _buffValue; }
		set{ _buffValue = value;}
	}

	/// <summary>
	/// Gets or sets the exp to level.
	/// </summary>
	/// <value>_expToLevel</value>
	public int ExpToLevel{
		get{ return _expToLevel; }
		set{ _expToLevel = value;}
	}

	/// <summary>
	/// Gets or sets the level modifer.
	/// </summary>
	/// <value>_levelModifer</value>
	public float LevelModifer{
		get{ return _levelModifer; }
		set{ _levelModifer = value;}
	}
#endregion
	/// <summary>
	/// Gets the adjusted base value.
	/// </summary>
	/// <value>The adjusted base value.</value>
	public int AdjustedBaseValue{
		get { return _baseValue + _buffValue; }
	}

	/// <summary>
	/// Calculates the exp to level.
	/// </summary>
	/// <returns>The exp to level.</returns>
	private int CalculateExpToLevel(){
		//type casting to int from float values. NOT ROUND OFF
		return (int)(_expToLevel * _levelModifer);
	}

	/// <summary>
	/// Levels up.
	/// </summary>
	public void LevelUp(){
		_expToLevel = CalculateExpToLevel ();
		_baseValue++ ;
	}



}
