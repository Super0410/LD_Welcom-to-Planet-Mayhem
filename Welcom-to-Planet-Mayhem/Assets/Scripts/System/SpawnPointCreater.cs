using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointCreater : MonoBehaviour
{
	public SpawnType spawnType;

	public int numAxes = 15;
	public int pointsPerAxis = 15;
	public float axisFov = 180;
	public float domeFov = 360;
	public float radius = 15;

	[HideInInspector] public Vector3[] points;

	[ContextMenu ("Generate")]
	public void GeneratePoints ()
	{
		points = new Vector3[numAxes * pointsPerAxis];
		int idx = 0;
		float stepAngle = axisFov / (pointsPerAxis - 1);
		float axisAngleStep = domeFov / (numAxes - 1);
		for (int i = 0; i < pointsPerAxis; i++) {
			for (int axis = 0; axis < numAxes; axis++) {				
				float angle = transform.eulerAngles.y - axisFov / 2f + stepAngle * i;
				var direction = Quaternion.AngleAxis (transform.eulerAngles.x + axisAngleStep * axis, -transform.right) * DirectionFromAngle (transform, angle);
				var center = transform.TransformPoint (direction * radius);
				points [idx++] = center;
			}
		}

		CreatePoints ();
	}

	//	[ContextMenu ("Create")]
	public void CreatePoints ()
	{
		if (transform.childCount != 0) {
			DestroyImmediate (transform.GetChild (0).gameObject);
		}

		string spawnTypeStr = spawnType.ToString ();

		Transform spawnPointHolder = new GameObject (spawnTypeStr + "SpawnPointHolder").transform;
		spawnPointHolder.parent = transform;

		for (int i = numAxes - 1; i < points.Length - numAxes; i++) {
			GameObject newGOBJ = new GameObject (spawnTypeStr + " Spawn Point " + i);
			SpawnPoint newSP = newGOBJ.AddComponent<SpawnPoint> ();
			newSP.spawnType = spawnType;
			newGOBJ.transform.position = points [i];
			newGOBJ.transform.parent = spawnPointHolder;
		}

		points = null;
	}

	/*
	void OnDrawGizmosSelected ()
	{
		if (points != null) {
			Gizmos.color = Color.red;
			for (int i = 0; i < points.Length; i++) {
				Gizmos.DrawSphere (points [i], 0.5f);
			}
		}
	}
	*/

	public Vector3 DirectionFromAngle (Transform transform, float angle, bool isGlobal = true)
	{
		if (!isGlobal) {
			angle += transform.eulerAngles.y;
		}

		return new Vector3 (Mathf.Sin (angle * Mathf.Deg2Rad), 0f, Mathf.Cos (angle * Mathf.Deg2Rad));
	}
}
