using UnityEngine;

public class GunController : MonoBehaviour
{
	public Transform weaponHold;

	Gun equippedGun;

	void Start ()
	{
		if (weaponHold == null) {
			weaponHold = GetComponentInChildren<WeaponHolder> ().transform;
		}
	}

	public void EquipGun (string gunID)
	{
		if (string.IsNullOrEmpty (gunID) || weaponHold == null)
			return;
		
		var gunToEquip = ObjectPooler.FetchInstance (gunID);

		if (gunToEquip == null)
			return;

		if (equippedGun != null) {
			ObjectPooler.ReturnInstance (equippedGun.GetType ().Name, equippedGun.gameObject);
		}

		gunToEquip.transform.position = weaponHold.position;
		gunToEquip.transform.rotation = weaponHold.rotation;
		gunToEquip.transform.SetParent (weaponHold);
		gunToEquip.SetActive (true);
		equippedGun = gunToEquip.GetComponent<Gun> ();
		equippedGun.Init ();
	}

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