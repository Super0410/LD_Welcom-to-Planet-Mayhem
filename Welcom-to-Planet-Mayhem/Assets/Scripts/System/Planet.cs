//=======================================================================================================================================//
// Product:    	Welcome to Planet Mayhem																								 //
// Developer : 	Pericles Barros																											 //
// Company:    	GameDevTeam																												 //
// Date: 	   	2017/04/22																						 						 //
//=======================================================================================================================================//


#region Imports

using UnityEngine;
using GameSystems.Patterns;
using System.Collections;

#endregion

namespace WelcomeToPlanetMayhem
{
	public class Planet : Singleton<Planet>
	{
		//===============================================================================================================================//
		//===================================================== Inspector Variables =====================================================//
		//===============================================================================================================================//

		#region InspectorVariables

		[SerializeField] float gravity = -9.8f;
		[SerializeField] float turnSpeed = 50f;

		#endregion

		//===============================================================================================================================//
		//=========================================================== Public Methods ====================================================//
		//===============================================================================================================================//

		#region Public Properties

		public static float Gravity {
			get{ return Instance.gravity; }
		}

		#endregion

		//===============================================================================================================================//
		//=========================================================== Public Methods ====================================================//
		//===============================================================================================================================//

		#region PublicMethods

		public static void Attract (Rigidbody body)
		{
			if (Instance == null)
				return;
			
			Vector3 targetDirection = (body.position - Instance.transform.position).normalized;
			Quaternion targetRotation = Quaternion.FromToRotation (body.transform.up, targetDirection) * body.rotation;

			body.rotation = Quaternion.Slerp (body.rotation, targetRotation, instance.turnSpeed * Time.deltaTime);
			body.AddForce (targetDirection * Instance.gravity, ForceMode.Acceleration);
		}

		#endregion
	}
}