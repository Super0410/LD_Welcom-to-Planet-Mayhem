using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _FORTEST_Movement : MonoBehaviour
{
	
	#region Public Members

	public float speed = 5f;

	#endregion

	#region Private Members

	#endregion

	#region Unity Methods

	void Start ()
	{
		
	}

	void Update ()
	{
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");
		Vector3 inputDir = new Vector3 (horizontal, 0, vertical);
			
		transform.Translate (inputDir * Time.deltaTime * speed);
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion
}
