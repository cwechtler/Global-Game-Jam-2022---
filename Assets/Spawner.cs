using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] GameObject[] enemyPrefabs;

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
			GameObject prefabToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
			GameObject enemy = Instantiate(prefabToSpawn, transform.position, Quaternion.identity) as GameObject;
			enemy.transform.SetParent(this.transform);
			yield return new WaitForSeconds(Random.Range(2,5));
		}
	}
}
