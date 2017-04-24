using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnim : MonoBehaviour {

	Animator anim;

	void Start ()
	{
		anim = GetComponent<Animator>();
	}
	
	void Update ()
	{

		if (Input.GetKey(KeyCode.Z))
		{
			anim.SetBool("isShooting", true);
		}

		if (Input.GetKeyUp(KeyCode.Z))
		{
			anim.SetBool("isShooting", false);
		}

		if (Input.GetKeyUp(KeyCode.X))
		{
			anim.SetTrigger("isKilled");
		}

	}
		
}
