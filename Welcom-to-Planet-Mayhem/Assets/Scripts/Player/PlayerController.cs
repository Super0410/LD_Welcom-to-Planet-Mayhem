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
	public class PlayerController : MonoBehaviour
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

		[SerializeField] PlayerMovement movementController;
		[SerializeField] PlayerCamera cameraController;
		[SerializeField] GunController gunController;
		[SerializeField] PlayerState stateController;
		[SerializeField] PlayerInventory inventoryController;

		#endregion

		//===============================================================================================================================//
		//======================================================= Private Fields ========================================================//
		//===============================================================================================================================//

		#region Private Fields

		string turnInput;
		string moveInput;
		string jumpInput;
		string zoomInput;
		string shootInput;
		string switchWeaponRightInput;
		string switchWeaponLeftInput;
		string startInput;
		string cancelInput;
		string confirmInput;
		string useToolInput;
		string reloadInput;

		#endregion

		//===============================================================================================================================//
		//===================================================== Public Properties =======================================================//
		//===============================================================================================================================//

		#region Public Properties

		public int PlayerID {
			get;
			set;
		}

		#endregion

		//===============================================================================================================================//
		//==================================================== Unity Event Functions ====================================================//
		//===============================================================================================================================//

		#region Unity Event Functions

		void Start ()
		{
			turnInput = Common.TurnInput + PlayerID;
			moveInput = Common.MoveInput + PlayerID;
			jumpInput = Common.JumpInput + PlayerID;
			zoomInput = Common.ZoomInput + PlayerID;
			shootInput = Common.ShootInput + PlayerID;

			switchWeaponRightInput = Common.SwitchRightInput + PlayerID;
			switchWeaponLeftInput = Common.SwitchLeftInput + PlayerID;
			startInput = Common.StartInput + PlayerID;
			cancelInput = Common.CancelInput + PlayerID;
			confirmInput = Common.ConfirmInput + PlayerID;
			useToolInput = Common.UseToolInput + PlayerID;
			reloadInput = Common.ReloadInput + PlayerID;

			movementController = GetComponentInChildren<PlayerMovement> ();
			cameraController = GetComponentInChildren<PlayerCamera> ();
			gunController = GetComponentInChildren<GunController> ();
			stateController = GetComponentInChildren<PlayerState> ();
			inventoryController = GetComponentInChildren<PlayerInventory> ();
		}

		void Update ()
		{
			//TODO: Convert to use player state instead

			switch (Common.State) {
			case Common.GameState.Playing:
				if (movementController != null) {
					movementController.OnMove (Input.GetAxis (moveInput));
					movementController.OnTurn (Input.GetAxis (turnInput));
					movementController.OnJump (Input.GetButtonDown (jumpInput));
				}
				if (cameraController != null) {
					cameraController.OnZoom (Input.GetAxis (zoomInput));
				}
				var increment = 0;
				if (gunController != null) {
					if (Input.GetButtonDown (shootInput)) {
						gunController.OnTriggerHold ();
					} else if (Input.GetButtonUp (shootInput)) {
						gunController.OnTriggerRelease ();
					}

					if (Input.GetButtonDown (reloadInput)) {
						gunController.Reload ();
					}

					if (Input.GetButtonDown (switchWeaponRightInput)) {
						increment = 1;
					} else if (Input.GetButtonDown (switchWeaponLeftInput)) {
						increment = -1;
					}

					if (inventoryController != null) {
						if (increment != 0) {
							var nextGun = inventoryController.GetNextWeapon (increment);
							gunController.EquipGun (nextGun);
						}
					}
				}

				if (inventoryController != null) {
					if (Input.GetButtonDown (useToolInput)) {
						inventoryController.UseTool ();
					}
				}

				break;
			}
		}

		#endregion

		//===============================================================================================================================//
		//=========================================================== Public Methods ====================================================//
		//===============================================================================================================================//

		#region Public Methods

		public void Init ()
		{

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

		void OnValidate ()
		{
			PlayerID = Mathf.Clamp (PlayerID, 1, 4);
		}


		#endregion

	}
}