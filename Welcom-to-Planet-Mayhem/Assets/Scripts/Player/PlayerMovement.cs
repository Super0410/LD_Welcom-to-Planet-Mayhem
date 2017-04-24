//=======================================================================================================================================//
// Product:    	Welcome to Planet Mayhem																								 //
// Developer : 	Pericles Barros																											 //
// Company:    	GameDevTeam																												 //
// Date: 	   	2017/04/22																						 						 //
//=======================================================================================================================================//


#region Imports

using UnityEngine;
using Utils.Libraries;

#endregion


[RequireComponent (typeof(GravityBody))]
public class PlayerMovement : MonoBehaviour
{
	//===============================================================================================================================//
	//===================================================== Inspector Variables =====================================================//
	//===============================================================================================================================//

	#region InspectorVariables

	[Header ("Movement Parameters")]
	[SerializeField] float moveSpeed = 5f;
	[SerializeField] float moveSmooth = 0.5f;
	[SerializeField] float turnSpeed = 5f;
	[SerializeField] float jumpForce = 100f;
	[SerializeField] float groundCheckDistance = 0.11f;

	#endregion

	//===============================================================================================================================//
	//====================================================== Public Properties ======================================================//
	//===============================================================================================================================//

	#region Public Properties

	#endregion

	//===============================================================================================================================//
	//======================================================= Private Members =======================================================//
	//===============================================================================================================================//

	#region PrivateMembers

	Rigidbody myRig;
	CapsuleCollider capsule;
	Vector3 moveAmount, moveVelocity;

	bool grounded;

	float turnInput;
	float moveInput;
	bool jumpInput;

	#endregion

	//===============================================================================================================================//
	//==================================================== Unity Event Functions ====================================================//
	//===============================================================================================================================//

	#region UnityEventFunctions

	// Use this for initialization
	void Start ()
	{
		capsule = GetComponent<CapsuleCollider> ();
		myRig = GetComponent<Rigidbody> ();
		myRig.freezeRotation = true;
		myRig.useGravity = false;
	}

	void Update ()
	{		
		//Turn 
		transform.Rotate (Vector3.up * turnInput * turnSpeed, Space.Self);

		//Calculate movement
		var targetMoveAmount = Vector3.forward * moveInput * moveSpeed;
		moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref moveVelocity, moveSmooth);

		//Jump
		if (jumpInput) {
			if (grounded) {
				myRig.AddForce (transform.up * jumpForce, ForceMode.Impulse);
			}		
		}
	}

	void FixedUpdate ()
	{
		var localMove = transform.TransformDirection (moveAmount) * Time.fixedDeltaTime;
		myRig.MovePosition (myRig.position + localMove);

		grounded = PhysicsTools.IsGrounded (transform, groundCheckDistance + capsule.height / 2);
	}

	#endregion


	//===============================================================================================================================//
	//========================================================= Public Methods ======================================================//
	//===============================================================================================================================//

	#region Public Methods

	public void OnJump (bool state)
	{
		jumpInput = state;
	}

	public void OnTurn (float input)
	{
		turnInput = input;
	}

	public void OnMove (float input)
	{
		moveInput = input;
	}

	#endregion
}
