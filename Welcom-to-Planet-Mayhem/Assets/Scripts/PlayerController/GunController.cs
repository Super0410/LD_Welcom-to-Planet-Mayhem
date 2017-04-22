using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
	public Transform weaponHold;
	//	public Gun[] allGunArr;

	private Gun equippedGun;

	public void EquipGun (Gun gun2Equip)
	{
		if (equippedGun != null) {
			Destroy (equippedGun.gameObject);
		}
		equippedGun = Instantiate (gun2Equip, weaponHold.position, weaponHold.rotation) as Gun;
		equippedGun.transform.parent = weaponHold;
	}

	//	public void EquipGun (int weaponIndex)
	//	{
	//		if (allGunArr [weaponIndex] != null)
	//			EquipGun (allGunArr [weaponIndex]);
	//	}

	public void OnTriggerHold ()
	{
		if (equippedGun != null) {
			equippedGun.OnTriggerHold ();
		}
	}

	public void OnTriggerRelease ()
	{
		if (equippedGun != null) {
			equippedGun.OnTriggerRelease ();
		}
	}

	public float GunHeight {
		get {
			if (equippedGun != null)
				return equippedGun.GunHeight;
			else
				return 0;
		}
	}

	public void LookAtPoint (Vector3 lookPoint)
	{
		if (equippedGun != null) {
			equippedGun.LookAtPoint (lookPoint);
		}
	}

	public void Reload ()
	{
		if (equippedGun != null) {
			equippedGun.Reload ();
		}
	}
}