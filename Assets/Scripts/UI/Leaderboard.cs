using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{

	[SerializeField] GameObject scorePrefab;
	[SerializeField] GameObject content;

	void Start()
	{
		for (int i = 0; i < GameController.instance.HighScores.Count; i++) {
			GameObject scoreRow = Instantiate(scorePrefab, transform.position, Quaternion.identity) as GameObject;
			scoreRow.transform.SetParent(content.transform);
			PlayerScorePrefabController prefabController = scoreRow.GetComponent<PlayerScorePrefabController>();

			prefabController.PlayerRank.text = (i + 1).ToString();
			prefabController.PlayerScore.text = GameController.instance.HighScores[i].Item2.ToString();
			prefabController.PlayerName.text = GameController.instance.HighScores[i].Item1;

			if (i == 0) {
				prefabController.Image1.SetActive(true);
			}
		}
	}
}
