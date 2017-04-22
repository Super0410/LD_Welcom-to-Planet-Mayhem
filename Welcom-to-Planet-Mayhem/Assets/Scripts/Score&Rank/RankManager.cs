using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RankManager : MonoBehaviour
{
	#region Private Members

	public List<ScoreKeeperForPlayer> scoreKeeperList = new List<ScoreKeeperForPlayer> ();

	#endregion

	#region Public Methods

	public void AddScoreListener (ScoreKeeperForPlayer scoreKeeper)
	{
		scoreKeeperList.Add (scoreKeeper);
	}

	public void DoRank ()
	{
		scoreKeeperList = scoreKeeperList.OrderBy (a => a.score).ToList ();

		for (int i = 0; i < scoreKeeperList.Count; i++) {
			scoreKeeperList [i].OnUpdateRank (i);
		}
	}

	#endregion
}
