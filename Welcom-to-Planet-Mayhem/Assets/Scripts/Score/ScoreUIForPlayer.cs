using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIForPlayer : MonoBehaviour
{
	
	#region Public Members

	public Text text_Score;
	public Text text_Rank;

	public string[] rankString;

	#endregion

	#region Public Methods

	public void UpdateScoreText (int score)
	{
		text_Score.text = score.ToString ("D6");
	}

	public void UpdateRankText (int rank)
	{
		text_Rank.text = rankString [rank];
	}

	#endregion

}
