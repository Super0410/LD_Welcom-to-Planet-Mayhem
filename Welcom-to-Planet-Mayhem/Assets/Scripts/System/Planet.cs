﻿//=======================================================================================================================================//
// Product:    	Welcome to Planet Mayhem																								 //
// Developer : 	Pericles Barros																											 //
// Company:    	GameDevTeam																												 //
// Date: 	   	2017/04/22																						 						 //
//=======================================================================================================================================//


#region Imports

using UnityEngine;
using System.Collections.Generic;

#endregion

public class Planet : Singleton<Planet>
{
	//===============================================================================================================================//
	//===================================================== Inspector Variables =====================================================//
	//===============================================================================================================================//

	#region InspectorVariables

	[SerializeField] float gravity = -9.8f;
	[SerializeField] float turnSpeed = 50f;

	static SpawnPoint[] playerSpawnPoints;
	static SpawnPoint[] enemySpawnPoints;
	static SpawnPoint[] objectSpawnPoints;
	static FisherYatesRandom fyRandom = new FisherYatesRandom ();

	#endregion

	//===============================================================================================================================//
	//=========================================================== Public Methods ====================================================//
	//===============================================================================================================================//

	#region Public Properties

	public static float Gravity {
		get{ return Instance.gravity; }
	}

	public static Vector3 RandomPlayerSpawnPoint {
		get {
			if (fyRandom == null)
				fyRandom = new FisherYatesRandom ();

			if (playerSpawnPoints == null)
				Instance.Start ();

			return playerSpawnPoints [fyRandom.Next (playerSpawnPoints.Length)].transform.position;
		}
	}

	public static Vector3 RandomEnemySpawnPoint {
		get {
			if (fyRandom == null)
				fyRandom = new FisherYatesRandom ();

			if (enemySpawnPoints == null)
				Instance.Start ();

			return enemySpawnPoints [fyRandom.Next (enemySpawnPoints.Length)].transform.position;
		}
	}

	public static Vector3 RandomObjectSpawnPoint {
		get {
			if (fyRandom == null)
				fyRandom = new FisherYatesRandom ();

			if (objectSpawnPoints == null)
				Instance.Start ();

			return objectSpawnPoints [fyRandom.Next (objectSpawnPoints.Length)].transform.position;
		}
	}

	#endregion

	//===============================================================================================================================//
	//============================================================ Unity Methods ====================================================//
	//===============================================================================================================================//

	#region Unity Methods

	void Start ()
	{
		SpawnPoint[] allPointArr = Instance.transform.GetComponentsInChildren<SpawnPoint> ();

		List<SpawnPoint> playerPointList = new List<SpawnPoint> ();
		List<SpawnPoint> enemyPointList = new List<SpawnPoint> ();
		List<SpawnPoint> objectPointList = new List<SpawnPoint> ();

		for (int i = 0; i < allPointArr.Length; i++) {
			switch (allPointArr [i].spawnType) {
			case SpawnType.Player:
				playerPointList.Add (allPointArr [i]);
				break;
			case SpawnType.Enemy:
				enemyPointList.Add (allPointArr [i]);
				break;
			case SpawnType.Object:
				objectPointList.Add (allPointArr [i]);
				break;
			}
		}

		playerSpawnPoints = playerPointList.ToArray ();
		enemySpawnPoints = enemyPointList.ToArray ();
		objectSpawnPoints = objectPointList.ToArray ();
	}

	#endregion

	//===============================================================================================================================//
	//=========================================================== Public Methods ====================================================//
	//===============================================================================================================================//

	#region PublicMethods

	public static void Attract (Rigidbody body)
	{
		if (Instance == null)
			return;

		Vector3 targetDirection = (body.position - Instance.transform.position).normalized;
		Quaternion targetRotation = Quaternion.FromToRotation (body.transform.up, targetDirection) * body.rotation;

		body.rotation = Quaternion.Slerp (body.rotation, targetRotation, instance.turnSpeed * Time.deltaTime);
		body.AddForce (targetDirection * Instance.gravity, ForceMode.Acceleration);
	}

	public static void Load ()
	{
		Instance.transform.position = Vector3.zero;
		Instance.transform.rotation = Quaternion.identity;
	}

	#endregion
}
