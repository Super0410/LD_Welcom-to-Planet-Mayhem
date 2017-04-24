//=======================================================================================================================================//
// Product:    	Welcome to Planet Mayhem																								 //
// Developer : 	Pericles Barros																											 //
// Company:    	GameDevTeam																												 //
// Date: 	   	2017/04/22																						 						 //
//=======================================================================================================================================//

#region Imports

using System.Collections.Generic;

#endregion


public class PlayerInventory
{
	//===============================================================================================================================//
	//======================================================= Private Fields ========================================================//
	//===============================================================================================================================//

	#region Private Fields

	List<string> weaponIDs = new List<string> ();
	Item currentTool;

	int currentWeaponID = 0;

	#endregion

	//===============================================================================================================================//
	//===================================================== Public Properties =======================================================//
	//===============================================================================================================================//

	#region Public Properties

	public bool HasTool {
		get{ return currentTool != null; }
	}

	#endregion

	//===============================================================================================================================//
	//=========================================================== Construtor ========================================================//
	//===============================================================================================================================//

	#region Constructor

	public PlayerInventory (string starterWeapon)
	{
		weaponIDs = new List<string> ();
		weaponIDs.Add (starterWeapon);
		currentWeaponID = 0;
	}

	#endregion

	//===============================================================================================================================//
	//=========================================================== Public Methods ====================================================//
	//===============================================================================================================================//

	#region Public Methods

	public void AddWeapon (string weaponID)
	{
		if (weaponIDs == null)
			weaponIDs = new List<string> ();
			
		if (!weaponIDs.Contains (weaponID))
			weaponIDs.Add (weaponID);
	}

	public bool UseTool ()
	{
		if (!HasTool)
			return false;
			
		if (currentTool != null) {
			currentTool.Use ();
			ObjectPooler.ReturnInstance (currentTool.GetType ().Name, currentTool.gameObject);
			currentTool = null;
		}

		return true;			
	}

	public string GetNextWeapon (int increment)
	{
		currentWeaponID += increment;

		if (currentWeaponID < 0)
			currentWeaponID = weaponIDs.Count - 1;
		if (currentWeaponID >= weaponIDs.Count)
			currentWeaponID = 0;
			
		return weaponIDs [currentWeaponID];
	}


	#endregion
}
