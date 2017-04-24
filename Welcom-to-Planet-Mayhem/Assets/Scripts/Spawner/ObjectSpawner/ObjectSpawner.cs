using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : Singleton<ObjectSpawner>
{
	
	#region Public Members

	public ObjectProperties[] objectArr;

	#endregion

	#region Public Methods

	public void Spawn ()
	{
		for (int arrIndex = 0; arrIndex < objectArr.Length; arrIndex++) {
			for (int objectCount = 0; objectCount < objectArr [arrIndex].number; objectCount++) {
				
				Vector3 spawnPos = Planet.RandomObjectSpawnPoint;
				Vector3 dir2Plane = (Planet.Instance.transform.position - spawnPos).normalized;
				Quaternion spawnRotation = Quaternion.LookRotation (dir2Plane);
				spawnRotation.eulerAngles = new Vector3 (spawnRotation.eulerAngles.x - 90, spawnRotation.eulerAngles.y, spawnRotation.eulerAngles.z);

				Instantiate (objectArr [arrIndex].objectPrefab, spawnPos, spawnRotation);
			}
		}
	}

	#endregion

	#region Private Methods

	#endregion

	[System.Serializable]
	public class ObjectProperties
	{
		public int number;
		public GameObject objectPrefab;
	}

	void Start ()
	{
		Spawn ();
	}
}
