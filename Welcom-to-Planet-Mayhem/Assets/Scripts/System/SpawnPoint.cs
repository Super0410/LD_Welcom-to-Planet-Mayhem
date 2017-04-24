﻿//=======================================================================================================================================//
// Product:    	Welcome to Planet Mayhem																								 //
// Developer : 	Pericles Barros																											 //
// Company:    	GameDevTeam																												 //
// Date: 	   	2017/04/22																						 						 //
//=======================================================================================================================================//
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	//===============================================================================================================================//
	//====================================================== Debugging & Testing ====================================================//
	//===============================================================================================================================//

	public SpawnType spawnType;

	#region Debugging & Testing

	void OnDrawGizmosSelected ()
	{
		switch (spawnType) {
		case SpawnType.Player:
			Gizmos.color = Color.green;
			break;
		case SpawnType.Enemy:
			Gizmos.color = Color.red;
			break;
		case SpawnType.Object:
			Gizmos.color = Color.yellow;
			break;
		}

		Gizmos.DrawSphere (transform.position, 0.5f);
	}

	#endregion

}
