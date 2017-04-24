
public static class Common
{
	public enum GameState
	{
		Loading,
		PlayerLobby,
		Menu,
		Playing,
		Paused,
		GameOver
	}

	public enum PlayerStatus
	{
		None,
		Stunned,
		Shielded,
		Slowmo,
		Berserk,
	}

	public enum PlayerStates
	{
		//			Spawning,
		Alive,
		Dying,
		Dead,
	}

	public static GameState State;

	public const int NumPlayers = 4;
}
