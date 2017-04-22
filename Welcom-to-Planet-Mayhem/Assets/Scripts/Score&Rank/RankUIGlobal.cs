using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUIGlobal : MonoBehaviour
{
	[SerializeField] Transform userScoreParent;
	
	public GameObject userScorePrefab;
	//	const string userScorePrefabUrl = "UI/UserScoreUIElement";

	public void UpdateGlobalRankUI (ScoreKeeperForPlayer[] updateTarget)
	{
		if (userScoreParent.childCount > 0) {
			for (int i = 0; i < userScoreParent.childCount; i++) {
				Destroy (userScoreParent.GetChild (i).gameObject);
			}
		}
	
		for (int i = 0; i < updateTarget.Length; i++) {
			Transform oneUserScore = Instantiate (userScorePrefab).transform;
			oneUserScore.SetParent (userScoreParent, false);
			oneUserScore.FindChild ("UserName").GetComponent<Text> ().text = updateTarget [i].playerIndex.ToString ("D2");
			oneUserScore.FindChild ("Score").GetComponent<Text> ().text = updateTarget [i].score.ToString ();
			oneUserScore.FindChild ("Rank").GetComponent<Text> ().text = (i + 1).ToString ();
		}
	}
}
