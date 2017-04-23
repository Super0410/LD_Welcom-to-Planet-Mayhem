using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public enum CameraMod
	{
		Follow,
		Aim
	}

	#region Public Members

	public Transform aimPoint;
	public Transform target;

	public float sensitivityX = 2;
	public float sensitivityY = 2;
	public float aimRotationX;
	public float aimRotationY;
	public Vector2 aimMinMaxX = new Vector2 (-35, 35);
	public Vector2 aimMinMaxY = new Vector2 (-30, 30);

	#endregion

	#region Private Members

	CameraMod cameraMod;
	Vector3 prePosition;
	Quaternion preRotation;

	#endregion

	#region Unity Methods

	void Start ()
	{
		cameraMod = CameraMod.Follow;
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (1)) {
			cameraMod = CameraMod.Aim;
		} else if (Input.GetMouseButtonUp (1)) {
			cameraMod = CameraMod.Follow;
		}
		if (Input.GetMouseButton (1)) {
			aimRotationX += Input.GetAxis ("Mouse X") * sensitivityX;
			aimRotationX = Mathf.Clamp (aimRotationX, aimMinMaxX.x, aimMinMaxX.y);

			aimRotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
			aimRotationY = Mathf.Clamp (aimRotationY, aimMinMaxY.x, aimMinMaxY.y);
		}

	}

	void LateUpdate ()
	{			

		aimPoint.transform.localEulerAngles = new Vector3 (-aimRotationY, aimRotationX, 0);

		if (cameraMod == CameraMod.Follow) {
			//Follow target
			transform.position = target.position;
			transform.rotation = target.rotation;
		} else {
			transform.position = aimPoint.position;
			transform.rotation = aimPoint.rotation;
		}
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion
}
