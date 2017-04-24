
using UnityEngine;

public class GameUI : Singleton<GameUI>
{
	enum MenuState
	{
		Main,
		Options,
		Ranking
	}

	#region Public Members

	[SerializeField] GameObject mainMenu;
	[SerializeField] GameObject playerLobby;
	[SerializeField] string openAnimatorStr = "IsOpen";
	[SerializeField] Animator anim_GameOverPanel;

	#endregion

	#region Private Members

	#endregion

	#region Unity Methods

	void Start ()
	{
		if (anim_GameOverPanel == null)
			anim_GameOverPanel = transform.Find ("GameOverPanel").GetComponent<Animator> ();
		
		anim_GameOverPanel.gameObject.SetActive (false);
	}

	void Update ()
	{
		
	}

	#endregion

	#region Public Methods

	public static void ShowLoadingScreen ()
	{
		//TODO: switch to game screen
		//TODO: overlay
	}

	public static void OpenGameOverPanel ()
	{
		Instance.anim_GameOverPanel.gameObject.SetActive (true);
		Instance.anim_GameOverPanel.SetBool (Instance.openAnimatorStr, true);
	}

	#endregion

	#region Private Methods

	#endregion
}
