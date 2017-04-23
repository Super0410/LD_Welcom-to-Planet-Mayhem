using UnityEngine;
using System.Collections;

namespace Utils.Libraries
{
	public static class PhysicsTools
	{
		//===============================================================================================================================//
		//========================================================= Pending Tasks =======================================================//
		//===============================================================================================================================//

		#region PendingTasks



		#endregion

		//===============================================================================================================================//
		//=========================================================== Public Methods ====================================================//
		//===============================================================================================================================//

		#region PublicMethods

		public static bool IsGrounded (Transform transform, float groundCheckDistance)
		{
			return Physics.Raycast (transform.position + transform.up * 0.1f, -transform.up, groundCheckDistance);
		}

		public static bool IsGrounded (Transform transform, float groundCheckDistance, LayerMask groundLayer)
		{
			return Physics.Raycast (transform.position + transform.up * 0.1f, -transform.up, groundCheckDistance, groundLayer);
		}

		public static float KineticEnergy (Rigidbody rigidbody)
		{
			return rigidbody.mass * Mathf.Pow (rigidbody.velocity.y, 2.0f) * 0.5f;
		}

		public static float PotentialEnergy (Rigidbody rigidbody, float gravity, float height)
		{
			return rigidbody.mass * gravity * height;
		}

		#endregion

		//===============================================================================================================================//
		//========================================================= Private Functions ===================================================//
		//===============================================================================================================================//

		#region PrivateFunctions



		#endregion

	}
}