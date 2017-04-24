using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TEST_FakeEnemy : MonoBehaviour
{
	
	#region Public Members

	#endregion

	#region Private Members

	Rigidbody rb;

	#endregion

	#region Unity Methods

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
//		Planet.Attract(
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion
}
