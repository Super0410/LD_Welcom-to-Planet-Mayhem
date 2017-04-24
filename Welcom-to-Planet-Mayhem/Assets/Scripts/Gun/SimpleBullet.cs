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


[RequireComponent (typeof(Rigidbody), typeof(Collider))]
public class SimpleBullet : MonoBehaviour
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

	[SerializeField] string collisionFX;
	[SerializeField] float damage;
	[SerializeField] float speed;
	[SerializeField] float bulletWeight;
	[SerializeField] bool useGravity;

	#endregion

	//===============================================================================================================================//
	//===================================================== Public Properties =======================================================//
	//===============================================================================================================================//

	#region Public Properties

	public string VFX {
		get{ return collisionFX; }
	}

	#endregion

	//===============================================================================================================================//
	//======================================================= Private Fields ========================================================//
	//===============================================================================================================================//

	#region Private Fields

	Rigidbody myRig;
	float distanceToPlanet;

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
		myRig.mass = bulletWeight;
	}

	void OnEnable ()
	{
		distanceToPlanet = Vector3.Distance (Planet.Instance.transform.position, transform.position);
	}

	void FixedUpdate ()
	{
		transform.position += transform.forward * speed * Time.deltaTime;
		var up = (transform.position - Planet.Instance.transform.position).normalized;
		var right = -transform.right;
		var forward = Vector3.Cross (up, right);

		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (forward, up), 10f * Time.deltaTime);

		if (useGravity) {
			distanceToPlanet += Planet.Gravity * Time.deltaTime * myRig.mass;
		} 

		transform.position = Vector3.Lerp (transform.position, (transform.position - Planet.Instance.transform.position).normalized * distanceToPlanet, Time.deltaTime);
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

		ObjectPooler.ReturnInstance (name, gameObject);
		myRig.velocity = Vector3.zero;
	}

	#endregion
}
