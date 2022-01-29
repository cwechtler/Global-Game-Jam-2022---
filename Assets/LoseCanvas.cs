using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoseCanvas : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI shadowText, airText, fireText, waterText, total ;

	void Start()
	{
		shadowText.text = GameController.instance.Shadow.ToString();
		airText.text = GameController.instance.Air.ToString();
		fireText.text = GameController.instance.Fire.ToString();
		waterText.text = GameController.instance.Water.ToString();
		total.text = GameController.instance.EnemiesKilled.ToString();
	}
}
