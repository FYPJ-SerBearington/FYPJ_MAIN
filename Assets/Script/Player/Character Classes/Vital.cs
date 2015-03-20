/// <summary>
/// Vital.cs
/// 3/18/2015
/// Gibbie Chairul
/// 
/// This class contain all the extra functions for a characters vitals
/// </summary>
public class Vital : ModifiedStat {
	private int _curValue;

	/// <summary>
	/// Initializes a new instance of the <see cref="Vital"/> class.
	/// </summary>
	//Default Constructor
	public Vital(){
		_curValue = 0;
		ExpToLevel = 50;
		LevelModifer = 1.1f;
	}

	/// <summary>
	/// When Getting the _curValue, make sure that it is not greater than our AdjustedBaseValue
	/// If it is, make it the same as our AdjustedBaseValue
	/// </summary>
	/// <value>The current value.</value>
	//Default Setter Getter
	public int CurValue{
		get{
			//Check current health is not greater our maximum health that it could be 
			if(_curValue > AdjustedBaseValue)
				_curValue = AdjustedBaseValue;
			return _curValue;
		}
		set{ _curValue = value;}
	}
}

/// <summary>
/// This enumerations is just a list of the vitals our character will have
/// </summary>
//numeration
public enum VitalName{
	Health,
	Energy,
	Mana
}
