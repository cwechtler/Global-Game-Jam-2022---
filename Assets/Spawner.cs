using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[Tooltip("Random between 2 seconds and this number")]
	[Range(1, 10)] [SerializeField] private int maxWaitToSpawn = 5;
	[SerializeField] private bool random = true;
	[ConditionalHide("random", true, true)]
	[SerializeField] private GameObject enemyPrefab;

	[ConditionalHide("random", true)]
	[SerializeField] private GameObject[] enemyPrefabs;


	void Start()
	{
		StartCoroutine(SpawnEnemies());
	}

	void Update()
	{
		
	}

	private IEnumerator SpawnEnemies()
	{
		while (true) {
			GameObject prefabToSpawn;
			if (random) {
				prefabToSpawn = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
			}
			else {
				prefabToSpawn = enemyPrefab;
			}

			GameObject enemy = Instantiate(prefabToSpawn, transform.position, Quaternion.identity) as GameObject;
			enemy.transform.SetParent(this.transform);
			yield return new WaitForSeconds(UnityEngine.Random.Range(2, maxWaitToSpawn));
		}
	}
}
