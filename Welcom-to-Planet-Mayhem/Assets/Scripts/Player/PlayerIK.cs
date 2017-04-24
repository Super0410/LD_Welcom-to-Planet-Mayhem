using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIK : MonoBehaviour
{
	
	#region Public Members

	public Transform leftHandPoint;
	public Transform rightHandPoint;
	public Transform spine1;

	public Transform gunHolder;
	public Gun gun;

	#endregion

	#region Private Members

	Animator anim;
	GunController gunController;
	Camera m_Camera;

	Vector3 farawayPoint;

	#endregion

	#region Unity Methods

	void Start ()
	{
		anim = GetComponent<Animator> ();
		gunController = GetComponent<GunController> ();
		m_Camera = GetComponentInChildren<Camera> ();

		ObjectPooler.CreatePool ("GUN_01", gun.gameObject, 1);

		gunController.EquipGun ("GUN_01");
	}

	Quaternion a;

	void Update ()
	{
		Vector2 screenCenter = new Vector2 (Screen.width / 2, Screen.height / 2);
		Ray screenCenterRay = m_Camera.ScreenPointToRay (screenCenter);
		farawayPoint = screenCenterRay.GetPoint (99);

		gunHolder.LookAt (farawayPoint);
//		gunController.LookAtPoint (farawayPoint);
	}

	void OnAnimatorIK (int layerIndex)
	{
		if (layerIndex == 0) {
		}
		if (layerIndex == 1) {
			anim.SetLookAtWeight (1, 1, 1, 1, 1);
			anim.SetLookAtPosition (farawayPoint);
		}
		if (layerIndex == 2) {
			anim.SetIKPositionWeight (AvatarIKGoal.LeftHand, 1);
			anim.SetIKRotationWeight (AvatarIKGoal.LeftHand, 1);
			anim.SetIKPositionWeight (AvatarIKGoal.RightHand, 1);
			anim.SetIKRotationWeight (AvatarIKGoal.RightHand, 1);

			anim.SetIKPosition (AvatarIKGoal.LeftHand, leftHandPoint.position);
			anim.SetIKRotation (AvatarIKGoal.LeftHand, leftHandPoint.rotation);

			anim.SetIKPosition (AvatarIKGoal.RightHand, rightHandPoint.position);
			anim.SetIKRotation (AvatarIKGoal.RightHand, rightHandPoint.rotation);
		}
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion
}
