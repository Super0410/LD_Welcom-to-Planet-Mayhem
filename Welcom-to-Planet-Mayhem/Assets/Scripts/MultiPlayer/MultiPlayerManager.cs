﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerManager : MonoBehaviour
{
	
	#region Public Members

	public event System.Action<int> OnPlayerJoinEvent;

	#endregion

	#region Private Members

	int playerCount = 0;
	int playerAliveCount = 0;

	#endregion

	#region Unity Methods

	void Start ()
	{
	}

	#endregion

	#region Public Methods

	public void OnPlayerJoin ()
	{
		if (playerCount > 3)
			return;

		playerCount++;
		playerAliveCount++;
		if (OnPlayerJoinEvent != null)
			OnPlayerJoinEvent (playerCount);
	}

	public void OnPlayerDie ()
	{
		playerAliveCount--;
		if (playerAliveCount <= 0) {
			FindObjectOfType<GameManager> ().OnGameOver ();
		}
	}

	#endregion
}
