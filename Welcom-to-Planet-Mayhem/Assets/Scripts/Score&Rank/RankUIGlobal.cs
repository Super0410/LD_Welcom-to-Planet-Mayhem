using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using GameSystems.Patterns;

public class RankUIGlobal : Singleton<RankUIGlobal>
{
	[SerializeField] Transform userScoreParent;
	
	public GameObject userScorePrefab;
	//	const string userScorePrefabUrl = "UI/UserScoreUIElement";

	public static void UpdateGlobalRankUI (ScoreKeeperForPlayer[] updateTarget)
	{
		if (Instance.userScoreParent.childCount > 0) {
			for (int i = 0; i < Instance.userScoreParent.childCount; i++) {
				Destroy (Instance.userScoreParent.GetChild (i).gameObject);
			}
		}
	
		for (int i = 0; i < updateTarget.Length; i++) {
			Transform oneUserScore = Instantiate (Instance.userScorePrefab).transform;
			oneUserScore.SetParent (Instance.userScoreParent, false);
			oneUserScore.FindChild ("UserName").GetComponent<Text> ().text = updateTarget [i].playerIndex.ToString ("D2");
			oneUserScore.FindChild ("Score").GetComponent<Text> ().text = updateTarget [i].score.ToString ();
			oneUserScore.FindChild ("Rank").GetComponent<Text> ().text = (i + 1).ToString ();
		}
	}
}
