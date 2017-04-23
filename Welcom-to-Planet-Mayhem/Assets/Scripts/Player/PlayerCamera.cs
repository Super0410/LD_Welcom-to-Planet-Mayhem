//=======================================================================================================================================//
// Product:    	Welcome to Planet Mayhem																								 //
// Developer : 	Pericles Barros																											 //
// Company:    	GameDevTeam																												 //
// Date: 	   	2017/04/22																						 						 //
//=======================================================================================================================================//


#region Imports

using UnityEngine;
using System.Collections;

#endregion

namespace WelcomeToPlanetMayhem
{
	public class PlayerCamera : MonoBehaviour
	{
		//===============================================================================================================================//
		//==================================================== Inspector Variables ======================================================//
		//===============================================================================================================================//

		#region Inspector Variables

		[Header ("Camera Parameters")]
		[SerializeField] float zoomSpeed;
		[SerializeField] Vector3 offset;
		[SerializeField] Transform target;

		#endregion

		//===============================================================================================================================//
		//======================================================= Private Fields ========================================================//
		//===============================================================================================================================//

		#region Private Fields

		Camera cam;
		float zoomInput;

		#endregion

		//===============================================================================================================================//
		//==================================================== Unity Event Functions ====================================================//
		//===============================================================================================================================//

		#region Unity Event Functions

		void Start ()
		{			
			cam = GetComponentInChildren<Camera> ();
		}

		void Update ()
		{
			//Zoom in/out
			cam.fieldOfView += zoomInput * zoomSpeed;
			cam.fieldOfView = Mathf.Clamp (cam.fieldOfView, 35f, 90f);
		}

		void LateUpdate ()
		{				
			//Follow target
			transform.position = target.TransformPoint (offset);
			transform.rotation = Quaternion.LookRotation ((target.position - transform.position).normalized, target.up);
		}

		#endregion

		//===============================================================================================================================//
		//=========================================================== Public Methods ====================================================//
		//===============================================================================================================================//

		#region Public Methods

		public void OnZoom (float input)
		{
			zoomInput = input;
		}

		#endregion
	}
}