using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
	private skillElementType skillElementType;

	void Start()
	{
		skillElementType = GetComponentInParent<VacumeHole>().SkillElementType;

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player")) {
			skillElementType type = collision.GetComponent<Enemy>().SkillElementTypeToDestroy;
			if (skillElementType == type) {
				Destroy(collision.gameObject);
			}
		}
	}
}
