using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] GameObject[] enemyPrefab;

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(Fire());
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private IEnumerator Fire()
	{
		while (true) {
			GameObject prefabToSpawn = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
			GameObject enemy = Instantiate(prefabToSpawn, transform.position, Quaternion.identity) as GameObject;
			enemy.transform.SetParent(this.transform);
			yield return new WaitForSeconds(Random.Range(1,3));
		}
	}
}
