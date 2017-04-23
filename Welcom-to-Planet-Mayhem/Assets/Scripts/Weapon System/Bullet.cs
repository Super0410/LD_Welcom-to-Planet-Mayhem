//=======================================================================================================================================//
// Product:    	Welcome to Planet Mayhem																								 //
// Developer : 	Pericles Barros																											 //
// Company:    	GameDevTeam																												 //
// Date: 	   	2017/04/22																						 						 //
//=======================================================================================================================================//

#region Imports

using UnityEngine;
using System.Collections;
using GameSystems.ObjectManagement;

#endregion

namespace WelcomeToPlanetMayhem
{
	[RequireComponent (typeof(Rigidbody))]
	public class Bullet : MonoBehaviour
	{
		//===============================================================================================================================//
		//======================================================== Pending Tasks ========================================================//
		//===============================================================================================================================//

		#region Pending Tasks

		//TODO:

		#endregion

		//===============================================================================================================================//
		//==================================================== Inspector Variables ======================================================//
		//===============================================================================================================================//

		#region Inspector Variables

		[SerializeField] string targetWeapon;
		[SerializeField] string collisionFX;

		#endregion

		//===============================================================================================================================//
		//===================================================== Public Properties =======================================================//
		//===============================================================================================================================//

		#region Public Properties

		public string TargetWeapon {
			get{ return targetWeapon; }
		}

		#endregion

		//===============================================================================================================================//
		//======================================================= Private Fields ========================================================//
		//===============================================================================================================================//

		#region Private Fields

		Rigidbody myRig;

		float damage;
		bool hasBeenFired;
		bool useGravity;

		float distanceToPlanet;
		float speed;

		#endregion

		//===============================================================================================================================//
		//==================================================== Unity Event Functions ====================================================//
		//===============================================================================================================================//

		#region Unity Event Functions

		void Start ()
		{
			myRig = GetComponent<Rigidbody> ();
			myRig.useGravity = false;
			myRig.freezeRotation = true;
		}

		void FixedUpdate ()
		{
			if (!hasBeenFired)
				return;
			
			transform.position += transform.forward * speed * Time.deltaTime;
			var up = (Planet.Instance.transform.position - transform.position).normalized;
			var right = transform.right;
			var forward = Vector3.Cross (right, up);

			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (forward, up), 10f * Time.deltaTime);

			if (useGravity) {
				distanceToPlanet -= Planet.Gravity * Time.deltaTime * myRig.mass;
			} else {				
				transform.position = Vector3.Lerp (transform.position, (transform.position - Planet.Instance.transform.position).normalized * distanceToPlanet, Time.deltaTime);
			}
		}

		void OnCollisionEnter (Collision collision)
		{
			var target = collision.collider.GetComponent<IDamageable> ();
			if (target != null) {
				target.TakeDamage (damage);
			}

			var vfx = ObjectPooler.FetchInstance (collisionFX);
			if (vfx != null) {
				vfx.transform.position = collision.contacts [0].point;
				vfx.GetComponent<VFX> ().Play ();
			}
			

			hasBeenFired = false;
			speed = 0f;
			distanceToPlanet = 0f;

			ObjectPooler.ReturnInstance (GetType ().Name, gameObject);
		}

		#endregion

		//===============================================================================================================================//
		//=========================================================== Public Methods ====================================================//
		//===============================================================================================================================//

		#region Public Methods

		public void Shoot (float speed, float damage, bool useGravity = false)
		{
			distanceToPlanet = Vector3.Distance (Planet.Instance.transform.position, transform.position);
			this.speed = speed;
			this.damage = damage;
			this.useGravity = useGravity;
			hasBeenFired = true;
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
}