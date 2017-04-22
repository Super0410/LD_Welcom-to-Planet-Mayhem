using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeperForPlayer : MonoBehaviour
{
	
	#region Public Members

	public int playerIndex;
	public int score;
	public int rank;
	public int combleCount;

	public int basicScorePerKill = 5;
	public float awardPowForComble = 1.2f;

	#endregion

	#region Private Members

	ScoreUIForPlayer scoreUI;
	RankManager rankManager;
	float timeBetweenNextKill = 1.5f;
	float lastTimeKilled;

	#endregion

	#region Unity Methods

	void Start ()
	{
		scoreUI = GetComponent<ScoreUIForPlayer> ();
		rankManager = FindObjectOfType<RankManager> ();

		score = Random.Range (0, 100);
		rankManager.DoRank ();
		updateScore (playerIndex);

//		Enemy.OnDeathStatic += updateScore;
//		FindObjectOfType<Player> ().OnDeath += onPlayerDeath;
	}

	#endregion

	#region Public Methods

	public void OnUpdateRank (int targetRank)
	{
		rank = targetRank;
		scoreUI.UpdateRankText (targetRank);
	}

	#endregion

	#region Private Methods

	void updateScore (int playerKillEnemyNum)
	{
		if (playerIndex != playerKillEnemyNum)
			return;
		
		if (Time.time < lastTimeKilled + timeBetweenNextKill) {
			combleCount++;
		} else
			combleCount = 0;
		
		lastTimeKilled = Time.time;

		score += basicScorePerKill + (int)Mathf.Pow (awardPowForComble, combleCount);

		rankManager.DoRank ();

		scoreUI.UpdateScoreText (score);
	}

	void onPlayerDeath ()
	{
//		Enemy.OnDeathStatic -= updateScore;
	}

	#endregion
}
