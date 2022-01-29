using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTimer : MonoBehaviour
{
	[SerializeField] private float timeToSpawn;
	[SerializeField] private GameObject tear;


	void Start()
	{
		StartCoroutine(SpawnAfterTime(timeToSpawn));
	}

	private IEnumerator SpawnAfterTime(float time) {
		yield return new WaitForSeconds(time);
		tear.gameObject.SetActive(true);
	}
}
