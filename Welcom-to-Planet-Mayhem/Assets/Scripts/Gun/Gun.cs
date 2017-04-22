using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public enum FireMode
	{
		Single,
		Burst,
		Auto
	}

	public FireMode fireMode;
	public int burstCount = 3;
	public Transform[] muzzlePointArr;
	public float msBetweenShots = 100;
	public float muzzleVelocity = 35;
	public int shotsPerMag;
	public float reloadTime = 0.3f;

	[Header ("Recoil")]
	public Vector2 kickMinMax = new Vector2 (0.05f, 0.2f);
	public Vector2 recoilAngleMinMax = new Vector2 (3, 5);
	public float recoilMoveSettleTime = 0.1f;
	public float recoilRotationSettleTime = 0.1f;

	[Header ("Effects")]
	public GameObject bulletPrefab;
	public Transform shellEjectionPoint;
	public ShellEjection shellPrefab;
	public GunFlash gunFlash;
	public AudioClip shotAudio;
	public AudioClip reloadAudio;

	float nextShotTime;
	bool isTriggerUp = true;
	int burstRemain;
	int shotsRemain;
	bool isReloading;

	Vector3 recoilSmoothDampVelocity;
	float recoilAngle;
	float recoilAngelSmoothDampVelocity;

	void Start ()
	{
		burstRemain = burstCount;
		shotsRemain = shotsPerMag;
	}

	public void OnTriggerHold ()
	{
		shoot ();
		isTriggerUp = false;
	}

	public void OnTriggerRelease ()
	{
		isTriggerUp = true;
		burstRemain = burstCount;
	}

	void LateUpdate ()
	{
		transform.localPosition = Vector3.SmoothDamp (transform.localPosition, Vector3.zero, ref recoilSmoothDampVelocity, recoilMoveSettleTime);
		recoilAngle = Mathf.SmoothDamp (recoilAngle, 0, ref recoilAngelSmoothDampVelocity, recoilRotationSettleTime);

		if (!isReloading) {
			transform.localEulerAngles = new Vector3 (-recoilAngle, 0, 0);
		}

		if (shotsRemain <= 0 && !isReloading) {
			Reload ();
		}
	}

	void shoot ()
	{
		if (!isReloading && Time.time > nextShotTime && shotsRemain > 0) {

			if (fireMode == FireMode.Single) {
				if (!isTriggerUp)
					return;
			} else if (fireMode == FireMode.Burst) {
				if (burstRemain <= 0)
					return;
				burstRemain--;
			}

			nextShotTime = Time.time + msBetweenShots / 1000;

			for (int i = 0; i < muzzlePointArr.Length; i++) {
				if (shotsRemain <= 0) {
					break;
				}
				shotsRemain--;

				Instantiate (bulletPrefab, muzzlePointArr [i].position, muzzlePointArr [i].rotation);
			}

			Instantiate (shellPrefab.gameObject, shellEjectionPoint.position, shellEjectionPoint.rotation);
			gunFlash.Active ();

			transform.localPosition -= Vector3.forward * Random.Range (kickMinMax.x, kickMinMax.y);
			recoilAngle += Random.Range (recoilAngleMinMax.x, recoilAngleMinMax.y);
			recoilAngle = Mathf.Clamp (recoilAngle, 0, 50);

			//TODO
			//AudioManager
		}
	}

	public float GunHeight {
		get{ return muzzlePointArr [0].position.y; }
	}

	public void LookAtPoint (Vector3 lookPoint)
	{
		if (!isReloading)
			transform.LookAt (new Vector3 (lookPoint.x, transform.position.y, lookPoint.z));
	}

	public void Reload ()
	{
		if (!isReloading && shotsRemain != shotsPerMag) {
			StartCoroutine (doReload ());

			//TODO
			//AudioManager
		}
	}

	IEnumerator doReload ()
	{
		isReloading = true;
		yield return new WaitForSeconds (0.2f);

		float percent = 0;
		float reloadSpeed = 1 / reloadTime;

		while (percent < 1) {
			percent += Time.deltaTime * reloadSpeed;
			yield return null;
		}

		isReloading = false;
		shotsRemain = shotsPerMag;
		burstRemain = burstCount;
	}
}