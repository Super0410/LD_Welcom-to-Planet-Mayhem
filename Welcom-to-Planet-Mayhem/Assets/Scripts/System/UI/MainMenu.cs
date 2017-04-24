//=======================================================================================================================================//
// Product:    	Welcome to Planet Mayhem																								 //
// Developer : 	Pericles Barros																											 //
// Company:    	GameDevTeam																												 //
// Date: 	   	2017/04/22																						 						 //
//=======================================================================================================================================//


#region Imports

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

#endregion

public class MainMenu : MonoBehaviour
{
	//===============================================================================================================================//
	//======================================================== Pending Tasks ========================================================//
	//===============================================================================================================================//

	#region Pending Tasks


	#endregion

	//===============================================================================================================================//
	//====================================================== Internal Classes =======================================================//
	//===============================================================================================================================//

	#region Internal Classes

	enum MenuPosition
	{
		Title,
		Lobby,
		Transition
	}

	#endregion

	//===============================================================================================================================//
	//==================================================== Inspector Variables ======================================================//
	//===============================================================================================================================//

	#region Inspector Variables

	[Header ("Title Parameters")]
	[SerializeField] Transform title;
	[SerializeField] float titlePosY = 2.5f;
	[SerializeField] float menuTitlePosY = 6f;
	[SerializeField] float titleMenuScale = 0.25f;
	[SerializeField] float titleScale = 0.5f;
	[SerializeField] Text startText;
	[SerializeField] float blinkSpeed = 1f;

	[Header ("Lobby Parameters")]
	[SerializeField] Lobby lobby;
	[SerializeField] float lobbyTimeout = 30f;
	[SerializeField] Text timerText;


	[SerializeField] float transitionSpeed = 0.5f;


	#endregion

	//===============================================================================================================================//
	//======================================================= Private Fields ========================================================//
	//===============================================================================================================================//

	#region Private Fields


	MenuPosition currentPosition;
	MenuPosition targetPosition;

	int fadeDir = 1;
	float blinkTargetAlpha = 0f;
	bool[] players;
	int numPlayers;
	float timer;

	#endregion


	//===============================================================================================================================//
	//==================================================== Unity Event Functions ====================================================//
	//===============================================================================================================================//

	#region Unity Event Functions

	void Start ()
	{
		if (lobby == null) {
			lobby = GetComponentInChildren<Lobby> ();
		}

		if (title == null) {
			title = transform.Find ("Title");
		}

		if (startText == null) {
			startText = transform.Find ("Text_Start").GetComponent<Text> ();
		}

		if (timerText == null) {
			timerText = transform.Find ("Text_Timer").GetComponent<Text> ();
		}

		lobby.gameObject.SetActive (false);
		timerText.enabled = false;

		players = new bool[Common.NumPlayers];
		numPlayers = 0;

		currentPosition = MenuPosition.Title;
	}


	void FixedUpdate ()
	{		
		
		switch (currentPosition) {
		case MenuPosition.Title:
			BlinkText ();

			if (Input.GetKeyDown (InputMapping.ButtonID (InputMapping.StartBtn, 1))) {				
				currentPosition = MenuPosition.Transition;
				StopAllCoroutines ();
				StartCoroutine (AnimateTitle (true));
			}						
			break;
		case MenuPosition.Lobby:
			numPlayers = 0;
			for (int i = 1; i <= Common.NumPlayers; i++) {				
				if (Input.GetKeyDown (InputMapping.ButtonID (InputMapping.ConfirmBtn, i))) {					
					players [i - 1] = true;
					lobby.Activate (i);
				}	

				if (players [i - 1]) {
					numPlayers++;
				}
			}

			if (numPlayers > 0) {
				timer -= Time.deltaTime;
				if (timer <= 0f && numPlayers > 0 || Input.GetKeyDown (InputMapping.ButtonID (InputMapping.StartBtn, 1)))
					GameManager.OnGameStart (players);
			} else {
				timer = lobbyTimeout;
			}

			if (Input.GetKeyDown (InputMapping.ButtonID (InputMapping.CancelBtn, 1))) {			
				for (int i = 0; i < Common.NumPlayers; i++) {
					players [i] = false;
					lobby.Deactivate (i + 1);
				}

				numPlayers = 0;
				StopAllCoroutines ();
				StartCoroutine (AnimateTitle (false));
				return;
			}

			SetTimerText ();
			break;
		case MenuPosition.Transition:
			return;
		}
	}


	#endregion

	//===============================================================================================================================//
	//=========================================================== Public Methods ====================================================//
	//===============================================================================================================================//

	#region Public Methods



	#endregion

	//===============================================================================================================================//
	//========================================================== Private  Methods ===================================================//
	//===============================================================================================================================//

	#region Private Methods


	void BlinkText ()
	{		
		blinkTargetAlpha += fadeDir * blinkSpeed * Time.deltaTime;
		blinkTargetAlpha = Mathf.Clamp (blinkTargetAlpha, 0, 1);

		startText.color = new Color (startText.color.r, startText.color.g, startText.color.b, blinkTargetAlpha);
		if ((fadeDir == -1 && blinkTargetAlpha == 0f) || (fadeDir == 1 && blinkTargetAlpha == 1f)) {
			fadeDir *= -1;
		}
	}

	void SetTimerText ()
	{
		int mins = Mathf.RoundToInt (timer / 60f);
		int secs = Mathf.RoundToInt (timer % 60f);
		timerText.text = mins + ":" + secs;
	}

	#endregion

	//===============================================================================================================================//
	//============================================================= Coroutines ======================================================//
	//===============================================================================================================================//

	#region Coroutines

	IEnumerator AnimateTitle (bool moveUp)
	{
		var targetPos = title.position;
		var targetScale = title.localScale;
		var lobbyScale = moveUp ? Vector3.one : Vector3.zero;

		if (moveUp) {
			targetPos.y = menuTitlePosY;
			targetScale = Vector3.one * titleMenuScale;
		} else {
			targetPos.y = titlePosY;
			targetScale = Vector3.one * titleScale;
		}

		timerText.enabled = moveUp;
		startText.enabled = !moveUp;
		lobby.gameObject.SetActive (moveUp);

		while (Vector3.Distance (title.position, targetPos) > 0.01f) {
			title.position = Vector3.Lerp (title.position, targetPos, transitionSpeed * Time.deltaTime);
			title.localScale = Vector3.Lerp (title.localScale, targetScale, transitionSpeed * Time.deltaTime);
			lobby.transform.localScale = Vector3.Lerp (lobby.transform.localScale, lobbyScale, transitionSpeed * Time.deltaTime);
			yield return null;
		}

		currentPosition = moveUp ? MenuPosition.Lobby : MenuPosition.Title;
	}





	#endregion

		
	//===============================================================================================================================//
	//====================================================== Debugging & Testing ====================================================//
	//===============================================================================================================================//

	#region Debugging & Testing



	#endregion

}
