using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
	public int ExperiencePointsWorth { get; set; }

	private PlayerController player;
	private bool collected = false;

	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && !collected)
		{
			collected = true;
			//Kill Coin
			Destroy(transform.gameObject);
			// Give Exp
			player.ExperiencePoints += ExperiencePointsWorth;
			print("added by: " + collision.name);
		}
	}
}
