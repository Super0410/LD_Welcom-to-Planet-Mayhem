using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	//	public enum CameraMode
	//	{
	//				Follow,
	//		Aim
	//	}

	#region Public Members

	public Transform aimPoint;
	public Transform target;

	[SerializeField] float yawSensitivity = 2;
	[SerializeField] float pitchSensitivity = 2;
	[SerializeField] Vector2 aimMinMaxX = new Vector2 (-15, 15);
	[SerializeField] Vector2 aimMinMaxY = new Vector2 (-50, 15);

	#endregion

	#region Private Members

	//	CameraMode cameraMode;
	Vector3 prePosition;
	Quaternion preRotation;

	float aimYaw;
	float aimPitch;

	#endregion

	#region Unity Methods

	//	void Start ()
	//	{
	//		cameraMode = CameraMode.Aim;
	//	}

	void Update ()
	{
		aimYaw = Mathf.Clamp (aimYaw, aimMinMaxX.x, aimMinMaxX.y);
		aimPitch = Mathf.Clamp (aimPitch, aimMinMaxY.x, aimMinMaxY.y);
	}

	void LateUpdate ()
	{
		aimPoint.transform.localEulerAngles = new Vector3 (-aimPitch, aimYaw, 0);

//		if (cameraMode == CameraMode.Follow) {
//			//Follow target
//			transform.position = target.position;
//			transform.rotation = target.rotation;
//		} else {
		transform.position = aimPoint.position;
		transform.rotation = aimPoint.rotation;
//		}
	}

	#endregion

	#region Public Methods

	public void OnMoveCamera (float pitch, float yaw)
	{
		aimYaw += yaw * yawSensitivity;
		aimPitch += pitch * pitchSensitivity;
	}

	//	public void SwitchCameraMode ()
	//	{
	//		if (cameraMode == CameraMode.Aim) {
	//			cameraMode = CameraMode.Follow;
	//		} else if (cameraMode == CameraMode.Follow) {
	//			cameraMode = CameraMode.Aim;
	//		}
	//	}

	#endregion

	#region Private Methods

	#endregion
}
