using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	
	#region Public Members

	public string openAnimatorStr = "IsOpen";

	public Animator anim_GameOverPanel;


	#endregion

	#region Private Members

	#endregion

	#region Unity Methods

	void Start ()
	{
		
	}

	void Update ()
	{
		
	}

	#endregion

	#region Public Methods

	public void OpenGameOverPanel ()
	{
		anim_GameOverPanel.SetBool (openAnimatorStr, true);
	}

	#endregion

	#region Private Methods

	#endregion
}
