/// <summary>
/// Modified stat.cs
/// 3/18/2015
/// Gibbie Chairul
/// 
/// This is the base class for all stats that will be modifiable by attributes
/// </summary>

using System.Collections.Generic;  //Generic was added so we can ise the List<.

public class ModifiedStat : BaseStat {
	private List<ModifyingAttribute> _mods; //A List of Attributes that modify this stat
	private int _modeValue; 				//The ammount added to the baseValue from the modifiers

/// <summary>
/// Initializes a new instance of the <see cref="ModifiedStat"/> class.
/// </summary>
	public ModifiedStat(){
		UnityEngine.Debug.Log ("Modified Created");
		_mods = new List<ModifyingAttribute> ();
		_modeValue = 0;
	}
	/// <summary>
	/// Adds a Modifuing Attribute to our list of mods.
	/// </summary>
	/// <param name="mod">Mod.</param>
	//Adding the Modifiying attribute to our list
	public void AddModifier(ModifyingAttribute mod){
		_mods.Add (mod);
	}

	/// <summary>
	/// Reset _modValue to 0
	/// check if we atleast one ModfyingAttribute in our list of mods.
	/// if we do, then iterate through the list and add the AdjustedBaseValue * ratio to our modValue
	/// </summary>
	//Calculate the amount we are suppose to modified
	private void CalculateModValue(){
		_modeValue = 0;

		if (_mods.Count > 0) 
		{
			foreach(ModifyingAttribute att in _mods)
							//Typecasting because AdjustValue is int and ratio is a float
				_modeValue += (int)(att.attribute.AdjustedBaseValue * att.ratio);
		}
	}

	/// <summary>
	/// This function is overriding the AdjustedBaseValue in the BaseStat Class
	/// Calculate the AdjustedBaseValue from the BaseValue + BuffValue + _modValue
	/// </summary>
	/// <value>The adjusted base value.</value>
	//Polymorphism AdjustedBaseValue
	public new int AdjustedBaseValue{
		get { return BaseValue + BuffValue + _modeValue; }
	}


	//Update only for ModifiedStats not base on monobehaviour
	public void Update(){
		CalculateModValue ();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns>The modifying attribute string.</returns>
//	public string GetModifyingAttributeString(){
//		string temp = "";
//		//UnityEngine.Debug.Log (_mods.Count);
//		for(int count = 0; count < _mods.Count; count++){
//			temp += _mods[count].attribute.Name;
//			temp += "_";
//			temp += _mods[count].ratio;
//
//			if(count < _mods.Count - 1)
//				temp += "i";
//			//UnityEngine.Debug.Log (temp);
//			//UnityEngine.Debug.Log (_mods[count].attribute.Name);
//			//UnityEngine.Debug.Log (_mods[count].ratio);
//		}
//		return temp;
//	}
}

/// <summary>
/// A structure that will hold on Attrbute and a ratio that will be added as a modifying attribute to our  ModifiedStats
/// </summary>
//structure
public struct ModifyingAttribute {
	//define Attribute class
	public Attribute attribute; 		//the attribute to be used as a modifier
	public float ratio;					//the perscent of the attrubutes adjustedBaseValue that will be applied to the ModifiedStat

	/// <summary>
	/// Initializes a new instance of the <see cref="ModifyingAttribute"/> struct.
	/// </summary>
	/// <param name="att">Att. the attribute to be used</param>
	/// <param name="rat">Rat. the ratio to be used</param>
	public ModifyingAttribute(Attribute att, float rat){
		UnityEngine.Debug.Log ("Modified Attribute Created");
		attribute = att;
		ratio = rat;
	}
}