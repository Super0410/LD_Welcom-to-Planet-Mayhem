//=======================================================================================================================================//
// Product:    	Welcome to Planet Mayhem																								 //
// Developer : 	Pericles Barros																											 //
// Company:    	GameDevTeam																												 //
// Date: 	   	2017/04/22																						 						 //
//=======================================================================================================================================//



#region Imports

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


#endregion

public class InputMapping : Singleton<InputMapping>
{
	//===============================================================================================================================//
	//====================================================== Internal Classes =======================================================//
	//===============================================================================================================================//

	#region Internal Classes

	[System.Serializable]
	public struct ButtonMapping
	{
		public string input;
		public int buttonID;
	}

	#endregion

	//===============================================================================================================================//
	//==================================================== Inspector Variables ======================================================//
	//===============================================================================================================================//

	#region Inspector Variables

	[SerializeField] ButtonMapping[] buttonMapping;

	#endregion

	//===============================================================================================================================//
	//======================================================= Private Fields ========================================================//
	//===============================================================================================================================//

	#region Private Fields

	static Dictionary<string, KeyCode>[] inputMapping;

	#endregion

	//===============================================================================================================================//
	//===================================================== Public Properties =======================================================//
	//===============================================================================================================================//

	#region Public Properties

	//Axes
	public const string MoveInput = "Vertical_";
	public const string TurnInput = "Horizontal_";
	public const string YawInput = "CameraYaw_";
	public const string PitchInput = "CameraPitch_";


	//Button names
	public static string JumpBtn = "Jump";
	public static string ShootBtn = "Shoot";
	public static string SwitchLeftBtn = "SwitchLeft";
	public static string SwitchRightBtn = "SwitchRight";
	public static string StartBtn = "Start";
	public static string ReloadBtn = "Reload";
	public static string UseToolBtn = "UseTool";
	public static string ConfirmBtn = "Confirm";
	public static string CancelBtn = "Cancel";

	#endregion

	//===============================================================================================================================//
	//==================================================== Unity Event Functions ====================================================//
	//===============================================================================================================================//

	#region Unity Event Functions

	void Start ()
	{
		inputMapping = new Dictionary<string, KeyCode> [Common.NumPlayers];

		for (int i = 1; i <= Common.NumPlayers; i++) {
			inputMapping [i - 1] = new Dictionary<string, KeyCode> ();
			for (int j = 0; j < buttonMapping.Length; j++) {				
				inputMapping [i - 1].Add (buttonMapping [j].input, (KeyCode)Enum.Parse (typeof(KeyCode), string.Format ("Joystick{0}Button{1}", i, buttonMapping [j].buttonID)));
			}
		}
	}

	#endregion

	//===============================================================================================================================//
	//=========================================================== Public Methods ====================================================//
	//===============================================================================================================================//

	#region Public Methods

	public static KeyCode ButtonID (string name, int playerID)
	{
		if (inputMapping == null)
			Instance.Start ();
		
		if (playerID < 1 || playerID > Common.NumPlayers)
			return KeyCode.None;

		var k = KeyCode.None;
		inputMapping [playerID - 1].TryGetValue (name, out k);
		return k;
	}

	#endregion
}
