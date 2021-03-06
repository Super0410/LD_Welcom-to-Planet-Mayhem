﻿//=======================================================================================================================================//
// Product:    	Welcome to Planet Mayhem																								 //
// Developer : 	Pericles Barros																											 //
// Company:    	GameDevTeam																												 //
// Date: 	   	2017/04/22																						 						 //
//=======================================================================================================================================//
using UnityEngine.UI;


#region Imports

using UnityEngine;
using System.Collections;

#endregion

public class Lobby : MonoBehaviour
{
	//===============================================================================================================================//
	//======================================================== Pending Tasks ========================================================//
	//===============================================================================================================================//

	#region Pending Tasks


	#endregion

	//===============================================================================================================================//
	//====================================================== Internal Classes =======================================================//
	//===============================================================================================================================//

	#region Internal Classes


	#endregion

	//===============================================================================================================================//
	//==================================================== Inspector Variables ======================================================//
	//===============================================================================================================================//

	#region Inspector Variables

	[SerializeField] Image[] playerSlots;

	#endregion

	//===============================================================================================================================//
	//======================================================= Private Fields ========================================================//
	//===============================================================================================================================//

	#region Private Fields


	#endregion

	//===============================================================================================================================//
	//===================================================== Public Properties =======================================================//
	//===============================================================================================================================//

	#region Public Properties


	#endregion

	//===============================================================================================================================//
	//==================================================== Unity Event Functions ====================================================//
	//===============================================================================================================================//

	#region Unity Event Functions

	void Awake ()
	{

	}

	void Start ()
	{

	}

	void Update ()
	{
		
	}

	#endregion

	//===============================================================================================================================//
	//=========================================================== Public Methods ====================================================//
	//===============================================================================================================================//

	#region Public Methods

	public void Activate (int id)
	{
		playerSlots [id - 1].color = GameManager.GetPlayerColor (id);
	}

	public void Deactivate (int id)
	{
		playerSlots [id - 1].color = GameManager.GetPlayerColor (-1);
	}

	#endregion

	//===============================================================================================================================//
	//========================================================== Private  Methods ===================================================//
	//===============================================================================================================================//

	#region Private Methods

		

	#endregion

	//===============================================================================================================================//
	//============================================================= Coroutines ======================================================//
	//===============================================================================================================================//

	#region Coroutines

		

	#endregion

		
	//===============================================================================================================================//
	//====================================================== Debugging & Testing ====================================================//
	//===============================================================================================================================//

	#region Debugging & Testing



	#endregion

}
