
using UnityEngine;

public class PlayerScore
{
	#region Public Members

	[HideInInspector] public int playerIndex;
	[HideInInspector] public int score;
	[HideInInspector] public int rank;
	[HideInInspector] public int combleCount;

	[HideInInspector] public int basicScorePerKill = 5;
	[HideInInspector] public float awardPowForComble = 1.2f;

	#endregion

	#region Private Members


	float timeBetweenNextKill = 1.5f;
	float lastTimeKilled;

	#endregion

	#region Unity Methods

	public PlayerScore (int playerID)
	{	
		playerIndex = playerID;
		score = 0;

		RankManager.DoRank ();
		UpdateScore (playerIndex);
	}

	#endregion

	#region Public Methods

	public void OnUpdateRank (int targetRank)
	{
		rank = targetRank;
	}

	#endregion

	#region Private Methods

	public void UpdateScore (int killedByEnemyID)
	{
		if (playerIndex == killedByEnemyID)
			return;
		
		if (Time.time < lastTimeKilled + timeBetweenNextKill) {
			combleCount++;
		} else
			combleCount = 0;
		
		lastTimeKilled = Time.time;

		score += basicScorePerKill + (int)Mathf.Pow (awardPowForComble, combleCount);

		RankManager.DoRank ();
	}

	#endregion
}
