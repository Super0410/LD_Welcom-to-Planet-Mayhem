using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(GunController))]
public class _FORTEST_PlayerController : MonoBehaviour
{
	public Gun[] allGunsArr;

	GunController gunController;

	void Start ()
	{
		gunController = GetComponent<GunController> ();
		gunController.EquipGun (allGunsArr [0]);
	}
		
}
