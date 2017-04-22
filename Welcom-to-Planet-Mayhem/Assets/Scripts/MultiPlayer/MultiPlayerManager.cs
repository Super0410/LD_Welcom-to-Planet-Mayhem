using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPlayerManager : MonoBehaviour
{
	
	#region Public Members

	public event System.Action<int> OnPlayerJoinEvent;

	public Button btn_PlayerAdder;

	#endregion

	#region Private Members

	int playerCount = 0;

	#endregion

	#region Unity Methods

	void Start ()
	{
		btn_PlayerAdder.onClick.AddListener (delegate {
			OnPlayerJoin ();
		});
	}


	#endregion

	#region Public Methods

	public void OnPlayerJoin ()
	{
		if (playerCount > 4)
			return;

		playerCount++;
		if (OnPlayerJoinEvent != null)
			OnPlayerJoinEvent (playerCount);
	}

	#endregion
}
