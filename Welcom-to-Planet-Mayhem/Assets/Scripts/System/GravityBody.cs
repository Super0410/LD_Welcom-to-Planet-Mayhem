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
	//====================================================== Public Properties ======================================================//
	//===============================================================================================================================//

	#region Public Properties

	public bool isStatic;

	#endregion

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
		myRig.collisionDetectionMode = CollisionDetectionMode.Continuous;
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate ()
	{
		Planet.Attract (myRig);
	}

	void OnCollisionEnter (Collision col)
	{
		if (!isStatic)
			return;
		
		if ((col.gameObject.layer << 1) == LayerMask.NameToLayer ("Planet")) {
			transform.SetParent (col.transform);
			myRig.isKinematic = true;
		}
	}

	#endregion

}
