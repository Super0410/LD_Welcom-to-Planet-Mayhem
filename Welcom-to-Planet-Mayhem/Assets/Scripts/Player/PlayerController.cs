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

public class PlayerController : MonoBehaviour, IDamageable, IChangeStatus
{
	//===============================================================================================================================//
	//==================================================== Inspector Variables ======================================================//
	//===============================================================================================================================//

	#region Inspector Variables

	[Header ("Component References")]
	[SerializeField] PlayerMovement movementController;
	[SerializeField] PlayerCamera cameraController;
	[SerializeField] GunController gunController;
	[SerializeField] PlayerUI uiController;

	[Header ("Gameplay Parameters")]
	[Range (100f, 1000f)][SerializeField] float maxHP;
	[SerializeField] string starterWeapon;

	#endregion

	//===============================================================================================================================//
	//======================================================= Private Fields ========================================================//
	//===============================================================================================================================//

	#region Private Fields

	string turnAxis;
	string moveAxis;
	string yawAxis;
	string pitchAxis;

	KeyCode jumpBtn;
	KeyCode shootBtn;
	KeyCode switchWeaponRightBtn;
	KeyCode switchWeaponLeftBtn;
	KeyCode startBtn;
	KeyCode cancelBtn;
	KeyCode confirmBtn;
	KeyCode useToolBtn;
	KeyCode reloadBtn;

	PlayerScore score;
	PlayerInventory inventory;

	Animator anim;

	float HP;
	Common.PlayerStates state;
	Common.PlayerStatus status;

	#endregion

	//===============================================================================================================================//
	//===================================================== Public Properties =======================================================//
	//===============================================================================================================================//

	#region Public Properties

	public int PlayerID {
		get;
		set;
	}

	public Common.PlayerStates State {
		get{ return state; }
	}

	public Common.PlayerStatus Status {
		get { return status; }
	}

	#endregion

	//===============================================================================================================================//
	//==================================================== Unity Event Functions ====================================================//
	//===============================================================================================================================//

	#region Unity Event Functions

	void Start ()
	{
		GameManager.Load ();
		PlayerID = 1;
		Init ();
	}

	void Update ()
	{
		switch (state) {
		case Common.PlayerStates.Alive:
			if (Input.GetKeyDown (startBtn)) {
				GameManager.Pause (PlayerID);
				return;
			}

			if (movementController != null) {
				movementController.OnMove (Input.GetAxis (moveAxis));
				movementController.OnTurn (Input.GetAxis (turnAxis));
				movementController.OnJump (Input.GetKeyDown (jumpBtn));
			}
			if (cameraController != null) {
				cameraController.OnMoveCamera (Input.GetAxis (pitchAxis), Input.GetAxis (yawAxis));
			}

			var increment = 0;
			if (gunController != null) {
				if (Input.GetKey (shootBtn)) {
					gunController.OnTriggerHold ();
				} else if (Input.GetKeyUp (shootBtn)) {
					gunController.OnTriggerRelease ();
				}

				if (Input.GetKeyDown (reloadBtn)) {
					gunController.Reload ();
				}

				if (Input.GetKeyDown (switchWeaponRightBtn)) {
					increment = 1;
				} else if (Input.GetKeyDown (switchWeaponLeftBtn)) {
					increment = -1;
				}

				if (inventory != null) {
					if (increment != 0) {
						var nextGun = inventory.GetNextWeapon (increment);
						gunController.EquipGun (nextGun);
					}
				}
			}

			if (inventory != null) {
				if (Input.GetKeyDown (useToolBtn)) {
					inventory.UseTool ();
				}
			}
			break;
		case Common.PlayerStates.Dying:
			if (anim != null)
				anim.SetBool ("Dead", true);
			uiController.GameOver ();
			break;
		}
	}

	#endregion

	//===============================================================================================================================//
	//=========================================================== Public Methods ====================================================//
	//===============================================================================================================================//

	#region Public Methods

	public void TakeDamage (float damage)
	{
		print (damage);
		HP -= damage;
		HP = Mathf.Clamp (HP, 0, maxHP);

		if (HP <= 0) {
			state = Common.PlayerStates.Dying;
			uiController.ShowRespawn ();
			return;
		}

		uiController.UpdateHealthBar (HP);
	}

	public void ChangeStatus (Common.PlayerStatus newStatus, float duration)
	{
		StartCoroutine (OnStatusChange (newStatus, duration));
	}

	#endregion

	//===============================================================================================================================//
	//========================================================== Private  Methods ===================================================//
	//===============================================================================================================================//

	#region Private Methods

	[ContextMenu ("Init")]
	public void Init ()
	{			
		turnAxis = InputMapping.TurnInput + PlayerID;
		moveAxis = InputMapping.MoveInput + PlayerID;
		yawAxis = InputMapping.YawInput + PlayerID;
		pitchAxis = InputMapping.PitchInput + PlayerID;

		shootBtn = InputMapping.ButtonID (InputMapping.ShootBtn, PlayerID);
		jumpBtn = InputMapping.ButtonID (InputMapping.JumpBtn, PlayerID);
		switchWeaponRightBtn = InputMapping.ButtonID (InputMapping.SwitchRightBtn, PlayerID);
		switchWeaponLeftBtn = InputMapping.ButtonID (InputMapping.SwitchLeftBtn, PlayerID);
		startBtn = InputMapping.ButtonID (InputMapping.StartBtn, PlayerID);
		cancelBtn = InputMapping.ButtonID (InputMapping.CancelBtn, PlayerID);
		confirmBtn = InputMapping.ButtonID (InputMapping.ConfirmBtn, PlayerID);
		useToolBtn = InputMapping.ButtonID (InputMapping.UseToolBtn, PlayerID);
		reloadBtn = InputMapping.ButtonID (InputMapping.ReloadBtn, PlayerID);

		movementController = GetComponentInChildren<PlayerMovement> ();
		cameraController = GetComponentInChildren<PlayerCamera> ();
		gunController = GetComponentInChildren<GunController> ();
		uiController = GetComponentInChildren<PlayerUI> ();

		inventory = new PlayerInventory (starterWeapon);
		score = new PlayerScore (PlayerID);

		inventory.AddWeapon ("Gun_Auto");
		inventory.AddWeapon ("Gun_Single");
		inventory.AddWeapon ("Gun_Burst");

		RankManager.AddScoreListener (score);

		gunController.EquipGun (starterWeapon);

		anim = GetComponentInChildren<Animator> ();

		HP = maxHP;
		uiController.UpdateHealthBar (HP);
	}

	public void Activate ()
	{
		gameObject.SetActive (true);
	}

	#endregion

	//===============================================================================================================================//
	//============================================================= Coroutines ======================================================//
	//===============================================================================================================================//

	#region Coroutines

	IEnumerator OnStatusChange (Common.PlayerStatus newStatus, float duration)
	{			
		if (duration < 0.1f)
			duration = 0.1f;

		var originalStatus = status;
		status = newStatus;

		uiController.SetStatus (status.ToString ());

		yield return new WaitForSeconds (duration);
		status = originalStatus;

		uiController.SetStatus (status.ToString ());
	}

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
