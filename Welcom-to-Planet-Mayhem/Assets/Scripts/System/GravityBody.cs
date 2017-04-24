//=======================================================================================================================================//
// Product:    	Welcome to Planet Mayhem																								 //
// Developer : 	Pericles Barros																											 //
// Company:    	GameDevTeam																												 //
// Date: 	   	2017/04/22																						 						 //
//=======================================================================================================================================//

#region Imports

using UnityEngine;

#endregion

[RequireComponent (typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
	//===============================================================================================================================//
	//======================================================= Private Members =======================================================//
	//===============================================================================================================================//

	#region PrivateMembers

	protected Rigidbody myRig;

	#endregion


	//===============================================================================================================================//
	//==================================================== Unity Event Functions ====================================================//
	//===============================================================================================================================//

	#region UnityEventFunctions

	// Use this for initialization
	void Awake ()
	{			
		myRig = GetComponent<Rigidbody> ();
		myRig.freezeRotation = true;
		myRig.useGravity = false;
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate ()
	{
		Planet.Attract (myRig);
	}

	#endregion

}
