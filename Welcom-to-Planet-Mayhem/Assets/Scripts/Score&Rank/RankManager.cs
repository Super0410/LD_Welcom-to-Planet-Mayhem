using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class RankManager
{
	#region Private Members

	public static List<PlayerScore> scoreKeeperList = new List<PlayerScore> ();

	#endregion

	#region Public Methods

	public static void AddScoreListener (PlayerScore scoreKeeper)
	{
		scoreKeeperList.Add (scoreKeeper);
	}

	public static void DoRank ()
	{
		scoreKeeperList = scoreKeeperList.OrderBy (a => a.score).ToList ();

		for (int i = 0; i < scoreKeeperList.Count; i++) {
			scoreKeeperList [i].OnUpdateRank (i);
		}
	}

	#endregion
}
